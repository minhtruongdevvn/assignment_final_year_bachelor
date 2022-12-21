import type { EntityBase } from '@/commons';

export interface ExternalResponse extends EntityBase {
	name?: string;
	code?: string;
	skill_ids?: string;
	skill_names?: string;
}

export type ExternalUpsert = {
	name?: string;
	skill_ids?: string;
	code?: string;
	pass_code?: string;
};
