<template>
	<a-tooltip
		hide-on-click-outside
		:distance="10"
		:disabled="!uiStore.isSidebarFolded"
		triggers="click"
		theme="category-link-popup"
		placement="bottom-start"
		@hide="subNavShown = false"
		@show="subNavShown = true"
	>
		<a-button
			:class="[
				'nav-link-button',
				{ 'router-link-active': isRouteActive || hasSubAndActive },
			]"
			:content="{
				tag: 'h3',
				content: name,
				classes: 'nav-link-content',
				disabled: uiStore.isSidebarFolded,
			}"
			:icon="{
				left: { tag: tag, classes: 'nav-link-icon' },
				right: {
					tag: ChevronDownIcon,
					disabled: uiStore.isSidebarFolded,
					classes: [
						'nav-link-content__dropdown',
						{ 'rotate-180': subNavShown },
					],
				},
			}"
			@click="toggleDropdown"
		/>

		<template #tooltip="{ hide }">
			<div class="nav-link__popup">
				<a-button
					v-for="(child, index) in children"
					:key="index"
					tag="router-link"
					:to="child.to"
					:aria-label="child.name"
					:content="{ tag: 'h3', content: child.name }"
					:title="'go to' + child.name"
					class="button nav-link__popup-item"
					@click="hide"
				/>
			</div>
		</template>
	</a-tooltip>

	<div
		v-if="!uiStore.isSidebarFolded"
		ref="dropdownEl"
		class="nav-link__dropdown"
	>
		<div class="nav-link__dropdown-wrapper">
			<a-button
				v-for="(child, index) in children"
				:key="index"
				tag="router-link"
				:to="child.to"
				:aria-label="child.name"
				:content="{ tag: 'h3', content: child.name }"
				:title="'go to' + child.name"
				class="nav-link__dropdown-item"
			/>
		</div>
	</div>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue';
import { useRoute } from 'vue-router';
import { ChevronDownIcon } from 'vue-tabler-icons';

import { useUIStore, useUserStore } from '@/stores';
import { categoryNavLinkProps } from '@/types';

const props = defineProps(categoryNavLinkProps);

const route = useRoute();
const uiStore = useUIStore();
const userStore = useUserStore();

const dropdownEl = ref<HTMLElement>();
const subNavShown = ref(false);
const isRouteActive = computed(() =>
	route.name?.toString().includes(props.name)
);
const hasSubAndActive = computed(() => {
	const activeNavLink = userStore.userSidebarSchema?.find(
		(uss) => uss.name == props.name
	);
	const navLinkHasChildren = activeNavLink && 'children' in activeNavLink;
	const hasChildren =
		navLinkHasChildren &&
		activeNavLink.children.some((arc) => arc.to == route.path);

	return hasChildren;
});

const toggleDropdown = () => {
	if (uiStore.isSidebarFolded) return;

	const dropdown = Object.throwIfNull(dropdownEl.value);

	subNavShown.value = !subNavShown.value;
	dropdown.style.height = !dropdown.clientHeight
		? dropdown.children[0]?.clientHeight + 'px'
		: '0';
};
</script>

<style lang="scss">
.nav-link {
	&-content {
		@apply flex w-full justify-between tracking-wide;

		&__dropdown {
			@apply mr-1 flex-shrink-0 transition-transform duration-300;
		}
	}
}

/* prettier-ignore */
@each $base in ('dropdown', 'popup') {
	.nav-link__#{$base} {
		@apply z-10 flex w-auto cursor-default flex-col items-start pr-5 pl-9;

		@if $base == 'popup' {
			@apply rounded-xl border border-main-quaternary bg-main-primary/90 py-5 backdrop-blur backdrop-filter;
			&-item { @apply w-32; }
		} @else {
			@apply h-0 overflow-hidden transition-[height] duration-300;
			&-wrapper { @apply w-full pt-5; }
		}

		&-item {
			@apply relative ml-3 h-11 flex-shrink-0 cursor-pointer select-none justify-start rounded-xl px-4 font-bold capitalize text-main-quinary transition duration-100 hover:bg-main-secondary/60 hover:text-main-senary;
			&:after { @apply absolute bottom-1/2 -left-[22px] mt-2 h-3 w-[22px] rounded-bl-xl border-b-2 border-l-2 border-main-quaternary content-['']; }
			&:not(:first-child):before { @apply absolute -top-[48px] -left-[22px] h-[133%] border-l-2 border-main-quaternary content-['']; }
			&:not(:last-child) { @apply mb-1; }
		}
	}
}

/* prettier-ignore */
.v-popper--theme-category-link-popup {
	@apply p-0;
	.v-popper__arrow-outer,
	.v-popper__arrow-inner { @apply border-transparent;}
	.v-popper__inner       { @apply border-transparent bg-transparent;}
}
</style>
