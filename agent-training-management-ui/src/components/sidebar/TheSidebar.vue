<template>
	<div
		class="sidebar"
		:class="{
			'z-30 translate-x-0':
				uiStore.breakpoints.isSmaller('sm') && !uiStore.isSidebarFolded,
		}"
	>
		<div class="sidebar-wrapper">
			<div class="sidebar-container">
				<div class="sidebar__logo">
					<a-button
						tag="a"
						href="/"
						:content="{
							content: 'AITO',
							disabled: uiStore.isSidebarFolded,
						}"
					>
						<template #icon-left>
							<img src="/src/assets/logo.svg" alt="app logo" />
						</template>
					</a-button>
				</div>

				<div class="sidebar__nav">
					<nav-link
						v-for="(link, index) in schema"
						:key="index"
						:="link"
					/>
				</div>

				<div class="sidebar__utility">
					<div
						class="sidebar__utility--buttons"
						:class="[
							uiStore.isSidebarFolded
								? 'flex-col gap-6'
								: 'flex-row-reverse gap-3',
						]"
					>
						<a-button
							class="sidebar__utility__toggle--theme"
							title="theme toggle button"
							:icon="{ tag: uiStore.isDark ? SunIcon : MoonIcon }"
							@click="uiStore.toggleDark"
						/>

						<a-button
							class="sidebar__utility__toggle--sidebar"
							title="sidebar toggle button"
							:icon="{
								tag: uiStore.isSidebarFolded
									? LayoutSidebarLeftExpandIcon
									: LayoutSidebarLeftCollapseIcon,
							}"
							@click="uiStore.toggleSidebar"
						/>
					</div>

					<the-user-profile />
				</div>
			</div>
		</div>
	</div>
	<div
		v-if="!uiStore.isSidebarFolded && smallerThanLg"
		class="absolute inset-0 z-10 bg-black/60"
		@click="uiStore.foldSidebar"
	/>
</template>

<script setup lang="ts">
import {
	LayoutSidebarLeftCollapseIcon,
	LayoutSidebarLeftExpandIcon,
	MoonIcon,
	SunIcon,
} from 'vue-tabler-icons';

import { useCssVarsStore, useUIStore } from '@/stores';
import type { SidebarNavSchema } from '@/types';

import NavLink from './navLink/NavLink.vue';

defineProps<{ schema: SidebarNavSchema }>();

const uiStore = useUIStore();
const cssVarsStore = useCssVarsStore();

const smallerThanLg = uiStore.breakpoints.smaller('lg');
</script>

<style lang="scss">
.sidebar {
	@apply float-left sm:<lg:w-auto <sm:absolute <sm:-translate-x-[100px];

	&,
	&-wrapper {
		@apply h-full w-[v-bind(cssVarsStore.sidebar.width)] min-w-[100px] transition-[transform,width] duration-300 ease-in-out;
	}

	&-container {
		@apply mr-0 flex h-full flex-col justify-between;
	}

	&-wrapper {
		@include apply-scrollbar(#{&}, y);
		@apply fixed top-0 z-30 bg-main-primary sm:<lg:absolute;
	}

	&__nav {
		@apply flex flex-auto flex-col gap-6;
	}

	&__utility {
		@apply mt-3 flex flex-col gap-6 py-9;

		&--buttons {
			@apply flex w-full justify-end px-[25px];
		}

		&__toggle--theme,
		&__toggle--sidebar {
			@apply flex-shrink-0 cursor-pointer rounded-2xl border-none bg-transparent text-main-quinary transition wh-[50px] hover:bg-main-secondary/60 hover:text-main-senary;

			.button-icon {
				@apply wh-[26px];
			}
		}
	}

	&__logo {
		@apply mx-[25px] mb-5 flex h-[120px] flex-shrink-0 items-center justify-between;

		.button {
			@apply box-border flex w-full items-center justify-start overflow-hidden truncate text-clip rounded-none text-justify;

			&-content {
				@apply ml-8 flex h-full select-none items-center overflow-hidden truncate text-clip text-4xl font-black uppercase tracking-[10px] text-black dark:text-white;
			}
		}
		img {
			@apply flex-shrink-0 select-none wh-[50px];
		}
	}
}
</style>
