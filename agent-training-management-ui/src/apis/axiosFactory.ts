import axios from 'axios';

import { axiosHelpers as helpers } from '@/utils';

type AxiosFactoryArgs = {
	url: string;
	headers?: GenericObject<string>;
	options?: { auth: boolean };
};

const baseHeaders = { 'Content-Type': 'application/json' };
const idsUrl = import.meta.env.VITE_IDS_URL;
const jwtStorageKey = `oidc.user:${idsUrl}:atm.spa.client`;

export const useAxios = ({ url, headers, options }: AxiosFactoryArgs) => {
	const instance = axios.create({
		headers: { ...baseHeaders, ...headers },
		baseURL: url,
	});

	instance.interceptors.request.use((config) => {
		helpers.validate
			.auth(config, jwtStorageKey, !options?.auth)
			.sieve(config.params);

		return config;
	}, helpers.defaultEject);

	instance.interceptors.response.use(
		helpers.defaultResolve,
		async (error) => {
			if (options?.auth) helpers.resolveTokenExpired(error);

			return Promise.reject(error);
		}
	);

	return instance;
};
