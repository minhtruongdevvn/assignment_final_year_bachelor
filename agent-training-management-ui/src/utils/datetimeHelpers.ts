export const getValidDate = (
	d: Nullable<Date | string>,
	locale: 'en-US' | 'vi-VN' | 'fr-CA' = 'vi-VN'
) => {
	if (!d) return undefined;

	const date = new Date(d as Date);

	if (date.toDateString() == 'Invalid Date') return 'Invalid Date';

	return date.toLocaleDateString(locale);
};

export const displayDate = (e: Date | string | undefined) => {
	const date = new Date(e as Date);
	return date.toLocaleDateString();
};

export const getDateRangesOfWeek = (week: number, year: number) => {
	const firstDayOfWeek = 3 + (week - 1) * 7;
	const lastDayOfWeek = firstDayOfWeek + 6;

	return {
		from: new Date(year, 0, firstDayOfWeek),
		to: new Date(year, 0, lastDayOfWeek),
	};
};
