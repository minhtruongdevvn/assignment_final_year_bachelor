// this file need to be imported at root (main.ts)
import './arrayExtensions';
import './objectExtensions';
import './stringExtensions';
import './storageAdapter';

export const getDeepPropertyExt = <T = unknown>(
	val: GenericObject,
	props: string[]
) => {
	let result: unknown = val[props[0]];

	for (let i = 1; i < props.length; i++) {
		if (typeof result != 'object') return result as T;

		result = (result as Record<string, unknown>)[props[i]];
	}

	return result as T;
};
