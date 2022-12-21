<template>
	<div class="a-table" :class="{ 'w-auto': !widthFull }">
		<div class="a-table-container">
			<div class="a-table__header">
				<div class="a-table__header-container">
					<h1 class="a-table__header__title">{{ title }}</h1>
					<table-utils class="a-table__header__utils" />
				</div>
			</div>

			<div class="a-table__body">
				<table class="a-table__body-container">
					<thead class="a-table__body__header">
						<tr tabindex="0">
							<th
								v-if="rowIndex"
								class="a-table__body__header--index"
							>
								#
							</th>
							<th
								v-for="(header, index) in schema"
								:key="index"
								class="a-table__body__header--key"
								:class="{ hidden: header.hidden }"
							>
								<span>
									{{ header.name ?? header.key }}
								</span>
							</th>
						</tr>
					</thead>

					<tbody class="w-full">
						<tr
							v-for="(tuple, i) in rowData"
							:key="i"
							class="a-table__body__row"
							@click="
								modals
									? showModals.dud(tuple.fields)
									: handleItemClick(tuple.fields)
							"
						>
							<td
								v-if="rowIndex"
								class="a-table__body__row--index"
							>
								<a-button :content="tuple.index" />
							</td>
							<row-cell
								v-for="(field, j) in tuple.fields"
								:key="j"
								:type="field.type"
								:value="field.value"
								:options="field.options"
								:class="{ hidden: field.hidden }"
								class="a-table__body__row--key"
							/>
						</tr>
					</tbody>
				</table>
			</div>

			<div v-if="response" class="a-table__footer">
				<a-button
					v-if="modals?.creation"
					class="a-table__footer__create-entity--button"
					:icon="{ tag: SquarePlusIcon }"
					:content="`New ${title}`"
					@click="showModals.creation"
				/>

				<a-pagination-minimal
					class="a-table__footer__pagination"
					:current-page="response.current_page"
					:total-pages="response.total_pages"
					:page-size="sieve.page_size"
					:total-items="response.total_items"
					@page-update="handlePagination.pageUpdate"
					@page-size-update="handlePagination.pageSizeUpdate"
				/>
			</div>
		</div>

		<div v-if="modals" class="a-table__modals">
			<context-modal-creation
				v-if="modals.creation"
				v-model:shown="shown.creation"
				:entity-name="modals.entityName"
				:formkit="{
					schema: modals.creation.formkit.schema,
					data: modals.creation.formkit.data,
				}"
				@submit:create="handleModalSubmits.create"
			/>

			<context-modal-dud
				v-if="modals.dud"
				v-model:shown="shown.dud"
				:editable="modals.dud.editable"
				:deletable="modals.dud.deletable"
				:more-actions="modals.dud.moreActions"
				:entity-name="modals.entityName"
				:formkit="modals.dud.formkit"
				:update-formkit="modals.dud.updateFormkit"
				@submit:delete="handleModalSubmits.delete"
				@submit:update="handleModalSubmits.update"
			>
				<template #more-actions="{ hideTooltip }">
					<slot name="more-actions" :hide-tooltip="hideTooltip" />
				</template>
			</context-modal-dud>

			<slot name="custom-modals" />
		</div>
	</div>
</template>

<script setup lang="ts">
import { computed, provide, reactive, ref, watch, watchSyncEffect } from 'vue';
import { SquarePlusIcon } from 'vue-tabler-icons';

import { defaultValues, injectionKeys } from '@/constants';
import { type Sieve, tableProps } from '@/types';

const emits = defineEmits<{
	(e: 'update:sieve', value: Sieve): void;
	(e: 'update:reload', value: boolean): void;
	(e: 'shown:modalCreation'): void;
	(e: 'shown:modalDud', row: unknown): void;
	(e: 'click:row', row: unknown): void;
	(e: 'submit:create', data: unknown): void;
	(e: 'submit:update', data: unknown): void;
	(e: 'submit:delete'): void;
}>();
const props = defineProps(tableProps);

const sieve = ref(defaultValues.getSieve());
const shown = reactive({ creation: false, dud: false });

const rowData = computed(() => {
	let startIndex = sieve.value.page_size * (sieve.value.page - 1) + 1;

	const mappedData = props.response?.data?.map((row) => {
		const index = startIndex++;
		const fields = props.schema?.map(({ key, options, hidden, type }) => {
			const value = row[key];
			return { value, options, hidden, type, key };
		});
		return { index, fields };
	});

	return mappedData;
});

const showModals = {
	creation: () => {
		emits('shown:modalCreation');
		shown.creation = true;
	},
	dud: (row: unknown) => {
		emits('shown:modalDud', row);
		shown.dud = true;
	},
};
const handleModalSubmits = {
	create: (data: unknown) => emits('submit:create', data),
	update: (data: unknown) => emits('submit:update', data),
	delete: () => emits('submit:delete'),
};
const handlePagination = {
	pageUpdate: (p: number) => (sieve.value.page = p),
	pageSizeUpdate: (ps: number) => {
		sieve.value.page = defaultValues.getSieve().page;
		sieve.value.page_size = ps;
	},
};
const handleItemClick = (d: unknown) => emits('click:row', d);
const loadData = () => {
	if (props.reload) emits('update:reload', false);

	emits('update:sieve', sieve.value);
};

const propsSchema = computed(() => props.schema);

provide(injectionKeys.TABLE, {
	updateSieve: (newSieve: Sieve) => (sieve.value = newSieve),
	propsSchema,
	sieve,
});
watchSyncEffect(() => loadData());
watch(
	() => props.reload,

	() => {
		return loadData();
	}
);
</script>

<style lang="scss">
/* prettier-ignore */
@mixin _configs {
	%bg      { @apply bg-main-secondary; }
	%bdr     { @apply border-main-quaternary/60; }
	%bg-dim  { @apply bg-main-tertiary; }
	%bdr-dim { @apply border-main-tertiary; }
}
@include _configs;

.a-table {
	@apply flex w-full flex-col items-center justify-center gap-7 pb-[8%] pt-8 sm:px-6;

	&-container {
		@extend%bg;
		@apply w-full rounded-lg;

		&:hover .a-table__body__row {
			@extend%bdr-dim;
		}
	}

	&__header {
		@extend%bdr;
		@include apply-scrollbar(#{&}, x, $auto-hide: false);
		@apply border-b p-4 md:px-12 md:py-7;

		&-container {
			@apply flex items-center justify-between gap-12;
		}
		&__title {
			@apply text-base font-bold capitalize leading-normal text-neutral-800 focus:outline-none dark:text-gray-200 sm:text-lg md:text-xl lg:text-2xl;
		}
	}

	&__body {
		@include apply-scrollbar(#{&});
		@apply max-h-[520px] pb-5 shadow transition-shadow;

		&-container {
			@apply relative w-full whitespace-nowrap;
		}

		&__header {
			@extend%bg;
			@apply sticky inset-0 box-content h-24 w-full font-reading text-sm focus:outline-none;

			&--index {
				@apply pl-10 pr-6 font-semibold capitalize leading-none text-neutral-800 dark:text-gray-200;
			}
			&--key {
				@apply px-12 font-semibold capitalize leading-none text-neutral-800 dark:text-gray-200;

				span {
					@apply flex;
				}
			}
		}

		&__row {
			@extend%bdr-dim;
			@apply h-20 cursor-pointer border-t border-transparent transition ease-in-out hover:bg-main-tertiary;

			&--index {
				@apply w-[1%] whitespace-nowrap pl-10 pr-6 text-center align-middle font-reading text-main-senary/60;
			}
		}
	}

	&__footer {
		@extend%bdr;
		@include apply-scrollbar(#{&}, x, $auto-hide: false);
		@apply flex items-center justify-between border-t p-4 md:px-8;

		&__create-entity {
			&--button {
				@include apply-hover-bubble($rounded: '6px');
				@apply flex h-8 items-center bg-transparent px-2 font-reading text-sm text-main-senary;

				.button-icon {
					@apply mr-2 wh-5;
				}
			}
		}

		&__pagination {
			@apply mr-0 ml-auto;
		}
	}

	&__modals {
		.context-modal-action-btn {
			@apply h-10 justify-start rounded-secondary py-2 px-6 text-sm capitalize transition-colors duration-100 disabled:brightness-50;
		}

		.btn {
			&--cancel {
				@apply text-main-senary hover:enabled:bg-main-quaternary/40 disabled:brightness-50;
			}
			&--icon {
				@apply px-4;

				.button {
					&-icon {
						@apply wh-5;
					}
					&-content {
						@apply mx-2;
					}
				}
			}
		}
	}

	.a-modal {
		@include apply-component-theme('.modal-themes--', 'table-action');
	}
}
</style>
