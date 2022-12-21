import type { EntityBase } from '@/commons';

export interface CategoryUpsert {
	name?: string;
	description?: string;
}

export interface CategoryResponse extends CategoryUpsert, EntityBase {}
