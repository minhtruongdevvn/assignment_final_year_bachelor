import type { ComputedRef, InjectionKey, Ref } from 'vue';

import type { Sieve, TablePropsSchema } from '@/types';

const TABLE: InjectionKey<{
	sieve: Ref<Sieve>;
	propsSchema: ComputedRef<Nullable<TablePropsSchema>>;
	updateSieve: (v: Sieve) => void;
}> = Symbol('TABLE');

export default {
	TABLE,
};
