import { AtmAPIsBase } from '@/commons';
import type {
	ClassResponse,
	ClassUpsert,
	Sieve,
	SieveResponse,
	TimetableRequest,
	TimetableResponse,
} from '@/types';
import type { StudentResponse } from '@/types/entities/student';
import { axiosHelpers } from '@/utils';

const apiBp = axiosHelpers.apiBoilerplates;

class ClassAPIs extends AtmAPIsBase<ClassUpsert, ClassResponse> {
	constructor() {
		super('classes');
	}

	getTimetable(args: TimetableRequest): Promise<TimetableResponse> {
		const path = args.id + '/timetables';
		return apiBp.getAttendnances(this.authAxiosClient, args, path);
	}

	addLecturerToClass(classId: number | string, subjectId: number | string) {
		return this.authAxiosClient.post(`/${classId}/lecturers`, [subjectId]);
	}

	removeLecturerFromClass(
		classId: number | string,
		subjectId: number | string
	) {
		return this.authAxiosClient.post(`/${classId}/lecturers/delete`, [
			subjectId,
		]);
	}

	getStudentsByClass(
		classId: string,
		args?: Partial<Sieve>
	): Promise<SieveResponse<StudentResponse>> {
		const processedSieve = axiosHelpers.apiBoilerplates.processSieve(args);
		const params = { ...processedSieve };

		return this.authAxiosClient.get(`/${classId}/students`, { params });
	}

	getClassByLecturer(
		lecturerId: number | string,
		args?: Partial<Sieve>
	): Promise<SieveResponse<ClassResponse>> {
		return this.getWithPath(`lecturers/${lecturerId}`, args);
	}

	addStudentToClass(classId: number | string, subjectId: number | string) {
		return this.authAxiosClient.post(`/${classId}/students`, [subjectId]);
	}

	addSlotToClass(classId: number | string, subjectId: number | string) {
		return this.authAxiosClient.post(`/${classId}/slots`, [subjectId]);
	}

	removeSlotFromClass(classId: number | string, subjectId: number | string) {
		return this.authAxiosClient.post(`/${classId}/slots/delete`, [
			subjectId,
		]);
	}
}

let api: ClassAPIs;

export const classAPIs = () => (api ??= new ClassAPIs());
