import type { PropType } from 'vue';

type DefaultFactory<T> = (props: GenericObject) => T | null | undefined;

type OptionalRequired<T> = T extends undefined
	? { required?: false }
	: { required: true };

type PropOptionsWorkaround<T = never, D = T> = OptionalRequired<T> & {
	type?: PropType<T> | true | null;
	default?: D | DefaultFactory<D> | null | object;
	validator?(value: unknown): boolean;
};

// convert interface properties to property options
export type VuePropsWrapper<P = GenericObject> = {
	[Key in keyof P]-?: PropOptionsWorkaround<P[Key]>;
};
