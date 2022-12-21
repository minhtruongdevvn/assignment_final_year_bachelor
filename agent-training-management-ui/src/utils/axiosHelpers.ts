import type {
	AxiosError,
	AxiosInstance,
	AxiosRequestConfig,
	AxiosResponse,
} from 'axios';

import { authAPIs } from '@/apis';
import type {
	AttendancesRequest,
	AuthDetail,
	LecturerAttendanceResponse,
	Sieve,
	SieveResponse,
} from '@/types';

const defaultEject = (error: AxiosError) => Promise.reject(error);
const defaultResolve = (response: AxiosResponse) => response.data;

const resolveTokenExpired = async (error: AxiosError) => {
	if (error.response?.status === 401) await authAPIs().signinSilent();
};

const validate = class {
	static sieve(params: unknown) {
		if (params && typeof params == 'object' && 'sorts' in params) {
			const sieve = params as Sieve;

			if (sieve.page < 1) sieve.page = 1;

			params = { ...sieve };
		}

		return this;
	}
	static auth(
		config: AxiosRequestConfig,
		storageKey: string,
		disabled?: boolean
	) {
		if (disabled) return this;

		const user = storage.session.get<AuthDetail>(storageKey);
		const token = `Bearer ${user?.access_token}`;

		config.headers ??= {};
		config.headers['Authorization'] = token;

		return this;
	}
};

const apiBoilerplates = {
	getAttendnances: <T = SieveResponse<LecturerAttendanceResponse>>(
		axiosInstance: AxiosInstance,
		args: AttendancesRequest,
		customPath: Nullable<string> = undefined
	): Promise<T> => {
		const path = customPath ?? args.id + '/attendances';
		const sieve = { page_size: 50 };
		const params = {
			...sieve,
			from: args.from?.toISOString(),
			to: args.to?.toISOString(),
		};

		return axiosInstance.get(path, { params });
	},

	processSieve: (sieve?: Partial<Sieve>) => {
		if (!sieve) return undefined;

		let sorts = '',
			filters = '';

		if (sieve.sorts?.length) {
			sorts = sieve.sorts.filter((v) => v).join(',');
		}

		if (sieve.filters?.length) {
			filters = sieve.filters.filter((v) => v).join(',');
		}

		return { ...sieve, filters, sorts };
	},
};

export const axiosHelpers = {
	defaultEject,
	defaultResolve,
	resolveTokenExpired,
	apiBoilerplates,
	validate,
};
