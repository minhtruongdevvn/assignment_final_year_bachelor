export type Sieve = {
	filters: string[];
	sorts: string[];
	page: number;
	page_size: number;
};

export type SieveResponse<TEntity> = {
	current_page: number;
	page_size: number;
	total_items: number;
	total_pages: number;
	data: TEntity[];
};
