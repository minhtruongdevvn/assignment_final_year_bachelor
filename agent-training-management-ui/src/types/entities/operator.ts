import type {
	EntityBase,
	IdentityResponseBase,
	IdentityUpsertBase,
} from '@/commons';

export interface OperatorResponse extends IdentityResponseBase, EntityBase {}

export type OperatorInsert = IdentityUpsertBase & {
	password?: string;
};

export type OperatorUpdate = IdentityUpsertBase;
