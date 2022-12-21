import type { EntityBase } from '@/commons';

import type { CategoryResponse } from './category';

export interface SkillUpsert {
	name?: string;
	description?: string;
}

export interface SkillResponse extends SkillUpsert, EntityBase {
	category?: CategoryResponse;
}

export interface SkillReportUpdateVerifiedRequest {
	score?: number;
	class_id?: number | string;
}
