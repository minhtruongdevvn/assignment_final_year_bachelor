import type { EntityBase } from '@/commons';

import type { LecturerResponse } from './lecturer';
import type { SkillResponse } from './skill';

export interface ClassUpsert {
	name?: string;
	placement?: string;
	start_date?: string | Date;
	end_date?: string | Date;
	available?: boolean;
	enable_automation?: boolean;
	max_learner?: number;
	skill_id?: number | string;
	description?: string;
}

export interface ClassResponse
	extends Omit<ClassUpsert, 'skill_id'>,
		EntityBase {
	skill: SkillResponse;
	lecturers: LecturerResponse[];
}
