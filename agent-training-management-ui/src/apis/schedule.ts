import { APIBase } from '@/commons';
import type { SieveResponse } from '@/types';
import type {
	AttendanceResponse,
	AttendancesRequest,
} from '@/types/entities/attendance';

class ScheduleAPIs extends APIBase {
	constructor() {
		super('schedules');
	}

	getAttendances(
		args: AttendancesRequest
	): Promise<SieveResponse<AttendanceResponse>> {
		const path = args.id + '/attendances';
		const params = {
			sieve: { page_size: 50 },
			from: args.from?.toISOString(),
			to: args.to?.toISOString(),
		};

		return this.authAxiosClient.get(path, { params });
	}
}

let api: ScheduleAPIs;

export const scheduleAPIs = () => (api ??= new ScheduleAPIs());
