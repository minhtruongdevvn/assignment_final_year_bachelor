<template>
	<div class="pgn-wrapper">
		<div ref="searchEl" class="pgn-srch">
			<form-kit
				id="pgnSearchForm"
				:actions="false"
				type="form"
				form-class="$reset pgn-srch-button"
				@submit="handleSearchPage"
			>
				<div class="pgn-srch-button-icon" @click="triggerSearch">
					<search-icon />
				</div>
				<div ref="searchInputEl">
					<form-kit
						type="number"
						placeholder="to"
						input-class="$reset pgn-srch-input"
						outer-class="$reset pgn-srch-input-outer"
						inner-class="$reset pgn-srch-input-inner"
						label-class="$reset pgn-srch-input-label"
						wrapper-class="$reset pgn-srch-input-wrapper"
					/>
				</div>
			</form-kit>
		</div>

		<div class="pgn-content">
			<a-button
				v-if="!isFirstPage"
				class="pgn-button pgn-button__first"
				:icon="{ tag: ChevronsLeftIcon }"
				@click="goFirst"
			/>
			<a-button
				v-if="!isFirstPage"
				class="pgn-button pgn-button__prev"
				:icon="{ tag: ChevronLeftIcon }"
				@click="goPrev"
			/>
			<a-button
				v-for="(page, index) in pagesToShow"
				:key="index"
				:content="page.index"
				class="pgn-button pgn-button__goto"
				:class="{ 'pgn-button-active': isActive(page.index) }"
				:disabled="page.disabled"
				@click="goTo(page.index)"
			/>
			<a-button
				v-if="!isLastPage"
				class="pgn-button pgn-button__next"
				:icon="{ tag: ChevronLeftIcon }"
				@click="goNext"
			/>
			<a-button
				v-if="!isLastPage"
				class="pgn-button pgn-button__last"
				:icon="{ tag: ChevronsLeftIcon }"
				@click="goLast"
			/>
		</div>
	</div>
</template>

<script setup lang="ts">
import { reset } from '@formkit/core';
import { onClickOutside } from '@vueuse/core';
import { computed, ref, watch } from 'vue';
import {
	ChevronLeftIcon,
	ChevronsLeftIcon,
	SearchIcon,
} from 'vue-tabler-icons';

import { useUIStore } from '@/stores';
import { paginationButtonProps } from '@/types';

const emits = defineEmits<{ (e: 'pageUpdate', value: number): void }>();
const props = defineProps(paginationButtonProps);

const store = useUIStore();

const searchEl = ref<HTMLElement>();
const searchInputEl = ref<HTMLElement>();
const searchShown = ref(false);
const smallerThanLg = store.breakpoints.smaller('lg');
const smallerThanSm = store.breakpoints.smaller('sm');

const isFirstPage = computed(() => props.currentPage === 1);
const isLastPage = computed(() => props.currentPage === props.totalPages);
const pagesToShow = computed(() => {
	const curr = props.currentPage;
	const total = props.totalPages;
	const numSm = !smallerThanSm.value ? 3 : 1;
	const maxNum = !smallerThanLg.value ? props.maxVisibleButtons! : numSm;
	const symRange = Math.round(maxNum / 2);

	// default when NOT near symmetry range
	let start = curr - symRange + 1;

	// when in LEFT symmetry range OR total pages < max visible pages
	if (curr < symRange || total <= maxNum) start = 1;
	// when in RIGHT symmetry range
	else if (total - curr < symRange - 1) start = total - maxNum + 1;

	// get last choosable, available index
	const end = Math.min(start + maxNum - 1, total);

	// render index range with currentPage indication
	return Array.applyRange(start, end).map((pageIndex) => ({
		disabled: pageIndex === props.currentPage,
		index: pageIndex,
	}));
});

const isActive = (page: number) => props.currentPage === page;
const goTo = (page: number) => emits('pageUpdate', page);
const goNext = () => emits('pageUpdate', props.currentPage + 1);
const goPrev = () => emits('pageUpdate', props.currentPage - 1);
const goLast = () => emits('pageUpdate', props.totalPages);
const goFirst = () => emits('pageUpdate', 1);

const triggerSearch = () => {
	if (searchShown.value == false) return (searchShown.value = true);
};
const handleSearchPage = (d: GenericObject) => {
	const page = Object.values(d)[0] as number;

	goTo(page);
	searchShown.value = false;
	reset('pgnSearchForm');
};

onClickOutside(searchEl, () => (searchShown.value = false));
watch(
	searchShown,
	() => {
		const searchInput = Object.throwIfNull(searchInputEl.value);
		const input = searchInput.getElementsByClassName(
			'pgn-srch-input'
		)[0] as HTMLElement;

		if (!input.clientWidth) {
			input.style.width = '40px';
			input.style.marginRight = '20px';
			input.style.translate = '0';
		} else {
			input.style.width = input.style.marginRight = '0';
			input.style.translate = '-10px';
		}
	},
	{ flush: 'post' }
);
</script>

<style lang="scss" scoped>
.pgn {
	&-wrapper {
		@apply flex gap-4;
	}
	&-content {
		@apply flex gap-[6px] text-[14.4px];
	}
	&-srch {
		&-button {
			@apply flex cursor-auto select-none items-center rounded-full text-center transition wh-auto focus-within:bg-accent-primary/10 hover:bg-accent-primary/10;

			&:focus-within {
				.pgn-srch-button-icon .button-icon {
					@apply pointer-events-none text-accent-primary;
				}
			}
			&-icon {
				@apply flex cursor-pointer items-center justify-center bg-transparent wh-10;
				.button-icon {
					@apply stroke-2 transition wh-4;
				}
			}
		}
		&-input {
			@apply mr-0 w-0 border-b border-main-quaternary bg-transparent font-reading text-sm transition-[translate,width,margin] duration-300;
			&-inner {
				@apply flex items-center justify-center text-center;
			}
		}
	}
	&-button {
		@apply h-10 cursor-pointer select-none rounded-full bg-transparent px-[15.5px] text-main-senary transition hover:bg-main-tertiary;
		.button-icon {
			@apply stroke-2 wh-4;
		}
		&-active {
			@apply pointer-events-none bg-accent-primary/10 font-extrabold text-accent-primary;
		}
		&-disabled {
			@apply pointer-events-none text-main-quaternary;
		}
		&__goto span {
			@apply flex items-center text-center font-code font-normal;
		}
	}
}

// @mixin apply-pgn-act($suffix, $classes, $is-extends: false) {
// 	$prefix: '.pgn-button__';
// 	@include apply-classes(
// 		append-affix(str-split($suffix), $prefix, false),
// 		$classes,
// 		$is-extends
// 	);
// }
// @include apply-pgn-act('first,prev,next,last', 'px-3 py-4');
// @include apply-pgn-act('next,last', 'rotate-180');
</style>
