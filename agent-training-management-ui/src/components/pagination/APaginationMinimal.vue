<template>
	<div class="pgn">
		<div class="pgn-container">
			<form-kit
				:disabled="!hasNextPage"
				type="aselect"
				title="page size"
				name="page_size"
				outer-class="$reset pgn__sieve-handler__page-size"
				:input-display-affixes="{ right: ' / page' }"
				:options="pageSizeOptions"
				:value="pageSizeOptions[0]"
				@input="handleUpdatePageSize"
			/>

			<span class="pgn__info" :title="pgnPagingInfo">
				{{ pgnItemsInfo }}
			</span>

			<div class="pgn__sieve-handler__page">
				<a-button
					title="prev"
					class="pgn__sieve-handler__page--action"
					:icon="{ tag: ChevronLeftIcon }"
					:disabled="isFirstPage"
					@click="goPrev"
				/>
				<a-button
					title="next"
					class="pgn__sieve-handler__page--action"
					:icon="{ tag: ChevronLeftIcon, classes: 'rotate-180' }"
					:disabled="isLastPage"
					@click="goNext"
				/>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
import { computedEager } from '@vueuse/shared';
import { computed } from 'vue';
import { ChevronLeftIcon } from 'vue-tabler-icons';

import { appConfigs } from '@/constants';
import { paginationMinimalProps } from '@/types';

const emits = defineEmits<{
	(e: 'pageUpdate', value: number): void;
	(e: 'pageSizeUpdate', value: number): void;
}>();
const props = defineProps(paginationMinimalProps);

const pageSizeOptions = appConfigs.table.pagination.pageSizeOptions;
const smallestPageSizeOptions = Math.min(...pageSizeOptions);

const pgnPagingInfo = computed(
	() => `page ${props.currentPage} of ${props.totalPages}`
);
const pgnItemsInfo = computed(() => {
	const start = props.pageSize * props.currentPage - props.pageSize + 1;
	let end = props.pageSize * props.currentPage;

	if (end >= props.totalItems) end = props.totalItems;

	return `${start}-${end} of ${props.totalItems} records`;
});
const hasNextPage = computedEager(() => {
	return props.totalItems > smallestPageSizeOptions;
});
const isFirstPage = computedEager(() => {
	return hasNextPage && props.currentPage == 1;
});
const isLastPage = computedEager(() => {
	return hasNextPage && props.currentPage == props.totalPages;
});

const handleUpdatePageSize = (v: unknown) => emits('pageSizeUpdate', Number(v));
const goNext = () => {
	if (props.currentPage < props.totalPages) {
		emits('pageUpdate', props.currentPage + 1);
	}
};
const goPrev = () => {
	if (props.currentPage != 1) emits('pageUpdate', props.currentPage - 1);
};
</script>

<style lang="scss">
.pgn {
	&-container {
		@apply flex min-h-[40px] min-w-40 justify-end gap-2 text-[14.4px] <md:flex-wrap;
	}

	&__info {
		@apply flex items-center bg-transparent px-1 font-reading text-sm text-main-senary;
	}

	&__sieve-handler {
		&__page-size {
			.a-select {
				@apply h-10 w-[86px];

				&__input {
					@apply cursor-pointer rounded-full border-none bg-transparent px-4;
				}
			}
		}

		&__page {
			@apply flex items-center gap-1 text-center font-normal;

			&--action {
				@apply h-10 cursor-pointer select-none rounded-full px-3 py-4 text-main-senary disabled:cursor-default disabled:text-main-senary/50;
			}

			.button-icon {
				@apply z-10 stroke-2 wh-4;
			}
		}

		&__page--action,
		&__page-size .a-select {
			@include apply-hover-bubble;
		}

		&__page-size {
			@apply disabled:cursor-default;

			input {
				@apply disabled:opacity-60;
			}
		}
	}
}
</style>
