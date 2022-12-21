import { AtmAPIsBase } from '@/commons';
import type { Sieve, SieveResponse, SlotResponse } from '@/types';
import { axiosHelpers } from '@/utils';

const apiBP = axiosHelpers.apiBoilerplates;

class SlotAPIs extends AtmAPIsBase<unknown, SlotResponse> {
	constructor() {
		super('slots');
	}

	get(args?: Partial<Sieve>): Promise<SieveResponse<SlotResponse>> {
		const processedSieve = apiBP.processSieve(args);
		const params = { ...processedSieve };

		return this.authAxiosClient.get('', { params });
	}
	getById(id: string | number): Promise<SlotResponse> {
		const path = `/${id}`;
		return this.authAxiosClient.get(path);
	}

	getUnassignedSlot(
		classId: number | string,
		args?: Partial<Sieve>
	): Promise<SieveResponse<SlotResponse>> {
		return this.getWithPath(`classes/unassigned/${classId}`, args);
	}

	getAssignedSlot(
		classId: number | string,
		args?: Partial<Sieve>
	): Promise<SieveResponse<SlotResponse>> {
		return this.getWithPath(`classes/assigned/${classId}`, args);
	}
}

let api: SlotAPIs;

export const slotAPIs = () => (api ??= new SlotAPIs());
