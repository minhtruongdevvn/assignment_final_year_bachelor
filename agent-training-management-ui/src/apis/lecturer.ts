import { AtmAPIsBase } from '@/commons';
import type {
	AttendancesRequest,
	LecturerAttendanceResponse,
	LecturerInsert,
	LecturerResponse,
	LecturerUpdate,
	Sieve,
	SieveResponse,
} from '@/types';
import { axiosHelpers } from '@/utils';

const apiBp = axiosHelpers.apiBoilerplates;

class LecturerAPIs extends AtmAPIsBase<LecturerInsert, LecturerResponse> {
	constructor() {
		super('lecturers');
	}

	getAttendances(
		args: AttendancesRequest
	): Promise<SieveResponse<LecturerAttendanceResponse>> {
		return apiBp.getAttendnances(this.authAxiosClient, args);
	}

	getUnassignedLecturer(
		classId: number | string,
		args?: Partial<Sieve>
	): Promise<SieveResponse<LecturerResponse>> {
		return this.getWithPath(`classes/unassigned/${classId}`, args);
	}

	getAssignedLecturer(
		classId: number | string,
		args?: Partial<Sieve>
	): Promise<SieveResponse<LecturerResponse>> {
		return this.getWithPath(`classes/assigned/${classId}`, args);
	}

	update(
		id: string | number,
		data: LecturerUpdate
	): Promise<LecturerResponse> {
		return this.authAxiosClient.put(`/${id}`, data);
	}
}

let api: LecturerAPIs;

export const lecturerAPIs = () => (api ??= new LecturerAPIs());
