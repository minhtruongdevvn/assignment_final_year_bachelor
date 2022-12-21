<template>
	<div class="nav-link">
		<a-tooltip
			auto-hide
			placement="left"
			:content="name"
			:disabled="!store.isSidebarFolded"
			:distance="10"
		>
			<nav-link-category
				v-if="children != null"
				:children="children"
				:name="name"
				:tag="tag"
			/>

			<a-button
				v-else
				:tag="RouterLink"
				class="nav-link-button"
				:to="to"
				:aria-label="name"
				:icon="{ tag: tag, classes: 'nav-link-icon' }"
				:content="{
					content: name,
					classes: 'nav-link-content',
					disabled: store.isSidebarFolded,
				}"
			/>
		</a-tooltip>
	</div>
</template>

<script setup lang="ts">
import { RouterLink } from 'vue-router';

import { useUIStore } from '@/stores';
import { navLinkProps } from '@/types';

import NavLinkCategory from './NavLinkCategory.vue';

defineProps(navLinkProps);

const store = useUIStore();
</script>

<style lang="scss">
.nav-link {
	@apply relative mx-[25px] w-auto cursor-pointer decoration-clone transition;

	&-button {
		@apply h-[50px] w-full justify-start rounded-2xl px-[12px] text-main-quinary transition duration-100;

		&:not(.nav-link .router-link-active) {
			@apply hover:bg-main-secondary/60 hover:text-main-senary;
		}
	}

	&-content {
		@apply ml-5 select-none overflow-hidden truncate text-ellipsis font-bold capitalize tracking-wide;
	}

	&-icon {
		@apply flex-shrink-0 wh-[26px];
	}

	& .router-link-active,
	&__popup .router-link-active {
		@apply bg-accent-primary/10 font-extrabold text-accent-primary;
	}
}
</style>
