<template>
	<div class="table-utils">
		<table-utils-sort />
		<table-utils-filter />

		<a-tooltip
			hide-on-click-outside
			triggers="click"
			placement="bottom-end"
			theme="table-util"
		>
			<template #default>
				<a-button :icon="{ tag: DotsIcon }" class="table-utils__more" />
			</template>

			<template #tooltip>
				<div class="table-utils__more-outer">
					<div class="table-utils__more-wrapper">
						<a-button
							class="table-utils__export"
							:icon="{ right: { tag: ChevronRightIcon } }"
							content="exports"
						/>
					</div>
				</div>
			</template>
		</a-tooltip>
	</div>
</template>

<script setup lang="ts">
import { ChevronRightIcon, DotsIcon } from 'vue-tabler-icons';

import TableUtilsFilter from './TableUtilsFilter.vue';
import TableUtilsSort from './TableUtilsSort.vue';
</script>

<style lang="scss">
.table-utils {
	@apply flex items-center gap-6;

	&__more {
		@include apply-hover-bubble($duration: '0.1s');
		@apply rounded-full bg-transparent wh-8;

		&-outer {
			@apply flex min-w-32 flex-col py-1;
		}
		&-wrapper {
			button {
				@apply justify-between rounded-none py-1 pl-3 pr-2 font-title text-sm capitalize tracking-wide text-main-senary wh-full hover:bg-main-quaternary;
				.button-icon {
					@apply w-4 stroke-[2.4px];
				}
			}
		}
	}

	&__action {
		@apply rounded-md border-transparent py-1 px-2 capitalize transition duration-150 hover:bg-main-quaternary/50;

		&-outer {
			@apply flex min-w-32 flex-col py-1;
		}

		&-wrapper {
			button {
				@apply justify-start rounded-none py-1 px-3 font-title text-sm capitalize tracking-wide text-main-senary wh-full hover:bg-main-quaternary;
				.button-icon {
					@apply mr-2 w-4 stroke-[2.4px];
				}
			}
		}

		> .button-icon {
			@apply mr-2 stroke-[2px] wh-5;
		}
	}
}

.v-popper--theme-table-util {
	.v-popper__inner {
		@apply overflow-hidden rounded-md border border-main-quaternary/50 bg-main-tertiary;
	}
	.v-popper__arrow-outer,
	.v-popper__arrow-inner {
		@apply border-transparent;
	}
}
</style>
