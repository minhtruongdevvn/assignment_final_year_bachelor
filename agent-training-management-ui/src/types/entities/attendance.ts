import type { EntityBase } from '@/commons';

import type { ClassResponse, StudentResponse } from '.';

export type AttendanceRequest = {
	student_id?: string | number;
	schedule_id?: string | number;
	attend_date?: string | Date;
};
export type AttendancesRequest = {
	from?: Date;
	to?: Date;
	id?: string | number;
};
export type AttendanceResponse = GenericObject & {
	schedule_id?: string;
	is_attended?: boolean;
	attend_date?: Date;
	absence_reasons?: string;
	slot?: SlotResponse;
	class?: ClassResponse;
	student?: StudentResponse;
};
export type LecturerAttendanceResponse = Omit<
	AttendanceResponse,
	'student' | 'absence_reasons'
>;

export type TimetableRequest = AttendancesRequest;
export type TimetableResponse = {
	is_checked_in?: boolean;
	check_in_date?: Date;
	slot?: SlotResponse;
};

export type SlotResponse = {
	day_of_week?: string;
	start_at?: string;
	end_at?: string;
};

export type ScheduleResponse = EntityBase & {
	class: ClassResponse;
	slot: SlotResponse;
};
