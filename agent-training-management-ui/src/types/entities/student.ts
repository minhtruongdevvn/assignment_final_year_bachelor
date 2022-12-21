import type {
	EntityBase,
	IdentityResponseBase,
	IdentityUpsertBase,
} from '@/commons';

export interface StudentResponse extends IdentityResponseBase, EntityBase {
	sex?: boolean;
	self_discipline?: number;
	height?: number;
	iq?: number;
	age?: number;
	eq?: number;
	stamina?: number;
	strength?: number;
	reaction_time?: number;
	identify_number?: string;
	pic?: string;
}

export type StudentInsert = IdentityUpsertBase & {
	password: string;
	sex?: boolean;
	height?: number;
	iq?: number;
	eq?: number;
	age?: number;
	stamina?: number;
	strength?: number;
	reaction_time?: number;
	identify_number?: string;
};

export type StudentUpdate = {
	sex?: boolean;
	age?: number;
	identify_number?: string;
	email?: string;
	family_name?: string;
	given_name?: string;
	birth_date?: Date | string;
};
