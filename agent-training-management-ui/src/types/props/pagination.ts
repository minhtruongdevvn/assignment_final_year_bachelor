import type { VuePropsWrapper } from '@/utils';
import type { PropType } from 'vue';

export type PaginationButtonProps = {
	totalPages: number;
	currentPage: number;
	maxVisibleButtons?: number;
};
export type PaginationMinimalProps = {
	totalPages: number;
	currentPage: number;
	totalItems: number;
	pageSize: number;
};

export const paginationButtonProps: VuePropsWrapper<PaginationButtonProps> = {
	totalPages: { type: Number, required: true },
	currentPage: { type: Number, required: true },
	maxVisibleButtons: { type: Number, default: 5 },
};
export const paginationMinimalProps: VuePropsWrapper<PaginationMinimalProps> = {
	totalPages: { type: Number, required: true },
	currentPage: { type: Number, required: true },
	totalItems: { type: Number, required: true },
	pageSize: { type: Number, required: true },
};
