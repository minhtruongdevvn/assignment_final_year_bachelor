import { AtmAPIsBase } from '@/commons';
import type { SkillResponse, SkillUpsert } from '@/types';

class SkillAPIs extends AtmAPIsBase<SkillUpsert, SkillResponse> {
	constructor() {
		super('skills');
	}

	getAll(): Promise<SkillResponse[]> {
		return this.authAxiosClient.get('/all');
	}
}

let api: SkillAPIs;

export const skillAPIs = () => (api ??= new SkillAPIs());
