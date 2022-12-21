import type { AxiosInstance } from 'axios';

import { useAxios } from '@/apis';
import type { Sieve, SieveResponse } from '@/types';
import { axiosHelpers } from '@/utils';

const pathSuffix = '/api/management/';
const atmUrl = import.meta.env.VITE_ATM_URL + pathSuffix;
const apiBP = axiosHelpers.apiBoilerplates;

export abstract class APIBase {
	protected authAxiosClient: AxiosInstance;

	constructor(endpoint: string) {
		this.authAxiosClient = useAxios({
			url: atmUrl + endpoint,
			options: { auth: true },
		});
	}
}

export abstract class AtmAPIsBase<TUpsert, TResponse> extends APIBase {
	get(args?: Partial<Sieve>): Promise<SieveResponse<TResponse>> {
		const processedSieve = apiBP.processSieve(args);
		const params = { ...processedSieve };

		return this.authAxiosClient.get('', { params });
	}
	getWithPath(
		path: string,
		args?: Partial<Sieve>
	): Promise<SieveResponse<TResponse>> {
		const processedSieve = apiBP.processSieve(args);
		const params = { ...processedSieve };

		return this.authAxiosClient.get(`/${path}`, { params });
	}
	getById(id: string | number): Promise<TResponse> {
		const path = `/${id}`;
		return this.authAxiosClient.get(path);
	}
	add(data: TUpsert): Promise<TUpsert> {
		return this.authAxiosClient.post('', data);
	}
	update(id: string | number, data: TUpsert): Promise<TResponse> {
		const path = `/${id}`;
		return this.authAxiosClient.put(path, data);
	}
	delete(id: string | number): Promise<TResponse> {
		const path = `/${id}`;
		return this.authAxiosClient.delete(path);
	}
}
