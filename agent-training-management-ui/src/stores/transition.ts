import { ref } from 'vue';

import { defineStoreBase } from '@/commons';

export const useTransitionStore = defineStoreBase({
	id: 'transition',
	storeSetup() {
		const loading = ref(false);
		const setLoading = (value: boolean) => (loading.value = value);
		return { loading, setLoading };
	},
});
