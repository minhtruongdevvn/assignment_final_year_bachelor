import { AtmAPIsBase } from '@/commons';
import type {
	AttendanceRequest,
	AttendanceResponse,
	AttendancesRequest,
	Sieve,
	SieveResponse,
	SkillReportUpdateVerifiedRequest,
	StudentInsert,
	StudentResponse,
	StudentUpdate,
} from '@/types';
import { axiosHelpers } from '@/utils';

const apiBp = axiosHelpers.apiBoilerplates;

class StudentAPIs extends AtmAPIsBase<StudentInsert, StudentResponse> {
	constructor() {
		super('students');
	}

	getAttendances(
		args: AttendancesRequest
	): Promise<SieveResponse<AttendanceResponse>> {
		return apiBp.getAttendnances(this.authAxiosClient, args);
	}

	update(id: string | number, data: StudentUpdate): Promise<StudentResponse> {
		const path = `/${id}`;
		return this.authAxiosClient.put(path, data);
	}

	getAttendanceByDate(args: AttendanceRequest): Promise<AttendanceResponse> {
		const studId = args.student_id;
		const attend = args.attend_date;
		const schedId = args.schedule_id;
		const path = `${studId}/attendances/${attend}/${schedId}`;

		return this.authAxiosClient.get(path);
	}

	getUnassignedStudent(
		classId: number | string,
		args?: Partial<Sieve>
	): Promise<SieveResponse<StudentResponse>> {
		return this.getWithPath(`classes/unassigned/${classId}`, args);
	}

	getAssignedStudent(
		classId: number | string,
		args?: Partial<Sieve>
	): Promise<SieveResponse<StudentResponse>> {
		return this.getWithPath(`classes/assigned/${classId}`, args);
	}

	addStudentToClass(classId: number | string, subjectId: number | string) {
		return this.authAxiosClient.post(`/${classId}/students`, [subjectId]);
	}

	verifyStudentAAMInfoByExternal(
		studentId: string | number,
		data: StudentInsert
	) {
		return this.authAxiosClient.put(`/${studentId}/verified`, data);
	}

	verifyStudentSkillReport(
		studentId: string | number,
		data: SkillReportUpdateVerifiedRequest
	) {
		return this.authAxiosClient.put(`/${studentId}/reports`, data);
	}
}

let api: StudentAPIs;

export const studentAPIs = () => (api ??= new StudentAPIs());
