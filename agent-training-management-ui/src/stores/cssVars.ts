import { computed } from 'vue';

import { defineStoreBase } from '@/commons';

import { useUIStore } from '.';

export const useCssVarsStore = defineStoreBase({
	id: 'cssvars',
	storeSetup() {
		const store = useUIStore();

		const navbar = { height: '70px' };

		const sidebar = computed(() => ({
			width: !store.isSidebarFolded ? '360px' : '100px',
		}));

		const contentWidth = computed(
			() => `calc(100% - ${sidebar.value.width})`
		);

		return {
			navbar,
			sidebar,
			contentWidth,
		};
	},
});
