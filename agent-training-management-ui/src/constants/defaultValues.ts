import type { Sieve, SieveResponse } from '@/types';

const getSieveResponse = (): SieveResponse<GenericObject> => ({
	current_page: 1,
	page_size: 1,
	total_items: 0,
	total_pages: 1,
	data: [],
});

const getSieve = (): Sieve => ({
	page_size: 5,
	page: 1,
	sorts: [],
	filters: [],
});

export default {
	getSieveResponse,
	getSieve,
} as const;
