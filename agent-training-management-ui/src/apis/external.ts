import { AtmAPIsBase } from '@/commons';
import type { ExternalResponse, ExternalUpsert } from '@/types';

class ExternalAPIs extends AtmAPIsBase<ExternalUpsert, ExternalResponse> {
	constructor() {
		super('externals');
	}

	add(data: ExternalUpsert): Promise<ExternalUpsert> {
		const path = `/${data.name}/${data.skill_ids}`;
		return this.authAxiosClient.post(path);
	}

	update(id: string | number, data: ExternalUpsert): Promise<ExternalUpsert> {
		const path = `/${id}/${data.skill_ids}`;
		return this.authAxiosClient.put(path);
	}

	regeneratePassCode(id: string | number): Promise<ExternalUpsert> {
		const path = `/${id}/regenerate`;
		return this.authAxiosClient.put(path);
	}
}

let api: ExternalAPIs;

export const externalAPIs = () => (api ??= new ExternalAPIs());
