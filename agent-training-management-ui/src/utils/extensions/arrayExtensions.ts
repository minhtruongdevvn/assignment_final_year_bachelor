import { getDeepPropertyExt } from '.';

export {};

Array.applyRange = (start: number, end: number) => {
	return Array.apply(0, Array(end - start + 1)).map((_, i) => i + start);
};

Array.filterCount = <T>(data: Array<T>, expression: (obj: T) => boolean) => {
	return Object.values(data).filter(expression).length;
};

Array.throwIfNull = <T extends unknown[] | []>(value: T, message?: string) => {
	if (!value) throw new Error(message);

	return value as NonNullable<T>;
};

Array.ifNull = <T extends unknown[] | []>(value: T, onError: () => void) => {
	if (!value) onError();

	return value as NonNullable<T>;
};

Array.getDeepProperty = <T = unknown>(
	value: unknown[],
	scheme: string,
	seperate = '.'
) => {
	if (!Array.isArray(value)) throw new Error('Value needs to be an array');

	return value.map((v) => getDeepPropertyExt<T>(v, scheme.split(seperate)));
};
