import { AtmAPIsBase } from '@/commons';
import type { OperatorInsert, OperatorResponse, OperatorUpdate } from '@/types';

class OperatorAPIs extends AtmAPIsBase<OperatorInsert, OperatorResponse> {
	constructor() {
		super('operators');
	}

	update(
		id: string | number,
		data: OperatorUpdate
	): Promise<OperatorResponse> {
		const path = `/${id}`;
		return this.authAxiosClient.put(path, data);
	}
}

let api: OperatorAPIs;

export const operatorAPIs = () => (api ??= new OperatorAPIs());
