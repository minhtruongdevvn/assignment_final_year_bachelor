import {
	breakpointsTailwind,
	useBreakpoints,
	useDark,
	useToggle,
} from '@vueuse/core';
import { ref } from 'vue';

import { defineStoreBase } from '@/commons';

export const useUIStore = defineStoreBase({
	id: 'ui',
	persist: true,
	storeSetup() {
		// states
		const isSidebarFolded = ref(false);
		const isDark = useDark({
			valueDark: 'dark',
			storageKey: 'ui-theme',
		});

		// getters
		const breakpoints = useBreakpoints(breakpointsTailwind);

		// actions
		const toggleDark = () => useToggle(isDark)();
		const foldSidebar = () => (isSidebarFolded.value = true);
		const unfoldSidebar = () => (isSidebarFolded.value = false);
		const toggleSidebar = () => {
			isSidebarFolded.value = !isSidebarFolded.value;
		};

		return {
			isDark,
			isSidebarFolded,
			breakpoints,
			toggleDark,
			foldSidebar,
			unfoldSidebar,
			toggleSidebar,
		};
	},
});
