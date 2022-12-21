import type {
	EntityBase,
	IdentityResponseBase,
	IdentityUpsertBase,
} from '@/commons';

export interface LecturerResponse extends IdentityResponseBase, EntityBase {}
export type LecturerInsert = IdentityUpsertBase & { password?: string };
export type LecturerUpdate = IdentityUpsertBase;
