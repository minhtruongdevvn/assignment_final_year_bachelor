import { getDeepPropertyExt } from '.';

export {};

Object.getDeepProperty = <T = unknown>(
	value: unknown,
	scheme: string,
	seperate = '.'
) => {
	if (typeof value != 'object' || Array.isArray(value))
		throw new Error('Value needs to be an object');

	return getDeepPropertyExt<T>(
		value as GenericObject,
		scheme.split(seperate)
	);
};

Object.throwIfNull = <T>(value: T, message?: string) => {
	if (!value) throw new Error(message);

	return value as NonNullable<T>;
};

Object.ifNull = <T>(value: T, onError: () => void) => {
	if (!value) onError();

	return value as NonNullable<T>;
};
