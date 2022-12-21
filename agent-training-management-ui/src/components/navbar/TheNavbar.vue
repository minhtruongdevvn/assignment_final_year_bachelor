<template>
	<div class="navbar-wrapper">
		<div class="navbar-content">
			<div class="navbar-left-items">
				<a-button
					v-if="smallerThanSm"
					class="sidebar-toggle-button"
					title="sidebar toggle button"
					:icon="{ tag: Menu2Icon }"
					@click="uiStore.toggleSidebar"
				/>
			</div>

			<div class="navbar-middle-items"></div>

			<div class="navbar-right-items"></div>
		</div>
	</div>
</template>

<script setup lang="ts">
import { Menu2Icon } from 'vue-tabler-icons';

import { useCssVarsStore, useUIStore } from '@/stores';

const uiStore = useUIStore();
const cssVarsStore = useCssVarsStore();

const smallerThanSm = uiStore.breakpoints.smaller('sm');
</script>

<style lang="scss">
.navbar- {
	&wrapper {
		@apply box-border flex h-[v-bind(cssVarsStore.navbar.height)] w-auto items-center border-b border-divider;
	}
	&content {
		@apply grid grid-cols-3 items-center wh-full;
	}
	&right-items {
		@apply select-none justify-self-end;
	}
	&left-items {
		@apply w-full;
	}
}

.sidebar-toggle-button {
	@apply rounded-2xl bg-transparent text-main-quinary transition wh-14 hover:bg-main-tertiary hover:text-gray-200;
}
</style>
