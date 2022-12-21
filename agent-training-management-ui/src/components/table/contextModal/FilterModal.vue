<template>
	<a-modal
		esc-to-close
		:model-value="props.shown"
		title="Filter"
		class="dud-modal"
		@update:model-value="handleClose"
	>
		<template #header-extend>
			<div class="dud-modal__header">
				<a-button
					class="context-modal-action-btn btn--icon dud-modal__action--reset btn--cancel"
					:icon="{ tag: RotateIcon }"
					content=" Reset Filter"
					@click="resetFilter"
				/>
			</div>
		</template>

		<div
			v-for="(prop, index) in schemas?.filter((ps) => ps.filterable)"
			:key="index"
		>
			<form-kit
				type="text"
				:label="prop.name ?? prop.key"
				:value="fields.find((e) => e.key == prop.key)?.value"
				@change="(event : Event) => (handleInput(prop.key, (event.target as HTMLInputElement).value))"
			/>
		</div>

		<template #footer="{ close }">
			<div class="dud-modal__footer">
				<a-button
					class="context-modal-action-btn btn--cancel dud-modal__action--cancel"
					content="cancel"
					@click="close"
				/>
				<a-button
					class="context-modal-action-btn btn--icon dud-modal__action--update"
					type="submit"
					:icon="{ tag: CheckIcon }"
					content="Confirm"
					@click="onFilter"
				/>
			</div>
		</template>
	</a-modal>
</template>

<script setup lang="ts">
import { type PropType, onMounted, ref } from 'vue';
import { CheckIcon, EditIcon, RotateIcon } from 'vue-tabler-icons';

import FilterOperation from '@/constants/filterOperations';
import type { Sieve, TablePropsSchema } from '@/types';

class FieldData {
	key: string;
	value: string;
	operation: string;
	constructor(key: string, value: string, operation?: string) {
		this.key = key;
		this.value = value;
		this.operation = operation ?? FilterOperation.contain;
	}

	update(value: string) {
		this.value = value;
	}
}

const emits = defineEmits<{
	(e: 'onFilter', data: string[]): void;
	(e: 'close'): void;
}>();
const props = defineProps({
	shown: { type: Boolean, required: true },
	schemas: { type: Object as PropType<TablePropsSchema>, required: true },
	sieve: { type: Object as PropType<Sieve>, required: true },
});

const fields = ref<FieldData[]>([]);

onMounted(() => {
	const initfields: FieldData[] = [];

	props.schemas
		.filter((ps) => ps.filterable)
		.forEach((schema) => {
			const filterSchema = props.sieve.filters.find(
				(e) =>
					e.split(
						schema.filterOperation ?? FilterOperation.contain
					)[0] == schema.key
			);

			if (filterSchema) {
				const value = filterSchema.split(
					schema.filterOperation ?? FilterOperation.contain
				)[1];

				initfields.push(
					new FieldData(schema.key, value, schema.filterOperation)
				);
			} else {
				initfields.push(
					new FieldData(schema.key, '', schema.filterOperation)
				);
			}
		});

	fields.value = initfields;
});

const handleInput = (field: string, value: string): void => {
	const index = fields.value.findIndex((e) => e.key == field);

	fields.value[index].update(value);
};

const handleClose = () => emits('close');
const onFilter = () =>
	emits(
		'onFilter',
		fields.value
			.filter((e) => e.value != '')
			.map((e) => `${e.key}${e.operation}${e.value}`)
	);

const resetFilter = () => emits('onFilter', []);
</script>

<style lang="scss">
.dud-modal {
	&__header {
		@apply flex justify-between gap-8;
	}
	&__footer {
		@apply flex justify-end gap-8;
	}

	&__action-- {
		&update {
			@apply bg-accent-warning/20 text-gray-200 hover:enabled:bg-accent-warning/40;
		}
		&delete {
			@apply ml-0 mr-auto bg-accent-error/30 text-rose-200 hover:bg-accent-error/50;
		}
	}

	&__delete-confirmation {
		.a-modal-container {
			@apply w-96;
		}

		&__footer {
			@apply flex justify-end gap-8;
		}

		&__action--delete {
			@extend.dud-modal__action--delete;
			@apply m-0;
		}
	}

	.a-modal__body {
		@apply flex flex-col gap-4;
	}

	.context-modal-action-btn {
		@apply h-10 justify-start rounded-secondary py-2 px-6 text-sm capitalize transition-colors duration-100 disabled:brightness-50;
	}

	.btn-- {
		&cancel {
			@apply text-main-senary hover:enabled:bg-main-quaternary/40 disabled:brightness-50;
		}
		&icon {
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
</style>
