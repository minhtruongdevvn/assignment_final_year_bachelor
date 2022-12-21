import { AtmAPIsBase } from '@/commons';
import type { CategoryResponse, CategoryUpsert } from '@/types';

class CategoryAPIs extends AtmAPIsBase<CategoryUpsert, CategoryResponse> {
	constructor() {
		super('categories');
	}
}

let api: CategoryAPIs;

export const categoryAPIs = () => (api ??= new CategoryAPIs());
