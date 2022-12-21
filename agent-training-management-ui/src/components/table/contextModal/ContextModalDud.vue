<template>
	<a-modal
		esc-to-close
		:model-value="shown"
		:title="getDefaultTitle('detail', props.title, true)"
		class="dud-modal"
		@update:model-value="hideDudModal"
	>
		<template #header-extend>
			<div class="dud-modal__header">
				<a-button
					v-if="editable && isEditMode"
					class="context-modal-action-btn btn--icon dud-modal__action--reset btn--cancel"
					:icon="{ tag: RotateIcon }"
					content="reset form"
					@click="resetForm"
				/>

				<a-button
					class="context-modal-action-btn btn--icon dud-modal__action--toggle-edit-mode btn--cancel"
					:icon="{ tag: iconEditToggleButton }"
					:content="isEditMode ? 'preview' : 'edit mode'"
					:disabled="!editable"
					@click="toggleMode"
				/>

				<a-tooltip
					v-if="moreActions"
					hide-on-click-outside
					triggers="click"
					placement="bottom-end"
					theme="dud-modal-more-action"
				>
					<template #default>
						<a-button
							class="dud-modal__action--more"
							:icon="{ tag: DotsIcon }"
						/>
					</template>

					<template #tooltip="{ hide }">
						<slot name="more-actions" :hide-tooltip="hide" />
					</template>
				</a-tooltip>
			</div>
		</template>

		<div v-show="isEditMode">
			<a-form-kit
				:id="updateFormId"
				:model-value="formModelValue"
				:schema="updateFormSchema.schema"
				:data="updateFormSchema.data"
				@submit="handleEditSubmit"
				@update:model-value="updateFormKitModelValue"
			/>
		</div>

		<div v-show="!isEditMode">
			<a-form-kit
				:id="detailFormId"
				:model-value="formModelValue"
				:schema="detailFormSchema.schema"
				:data="detailFormSchema.data"
				@update:model-value="updateFormKitModelValue"
			/>
		</div>

		<template #footer="{ close }">
			<div class="dud-modal__footer">
				<a-button
					class="context-modal-action-btn btn--icon dud-modal__action--delete"
					content="delete"
					:icon="{ tag: TrashXIcon }"
					:disabled="!deletable"
					@click="showDeletionModal"
				/>

				<a-button
					class="context-modal-action-btn btn--cancel dud-modal__action--cancel"
					content="cancel"
					@click="close"
				/>

				<a-button
					v-if="isEditMode"
					class="context-modal-action-btn btn--icon dud-modal__action--update"
					type="submit"
					:icon="{ tag: EditIcon }"
					content="udpate"
					:disabled="!isFormChanged"
					:form="updateFormId"
				/>
			</div>
		</template>
	</a-modal>

	<a-modal
		v-if="deletable"
		v-model="deletionShown"
		esc-to-close
		container-class=""
		:title="getDefaultTitle('delete', props.title)"
		class="dud-modal__delete-confirmation"
	>
		<p>Are you sure to delete this {{ props.entityName }} ?</p>

		<template #footer="{ close }">
			<div class="dud-modal__delete-confirmation__footer">
				<a-button
					class="context-modal-action-btn btn--cancel dud-modal__action--cancel"
					content="cancel"
					@click="close"
				/>
				<a-button
					class="context-modal-action-btn btn--icon dud-modal__delete-confirmation__action--delete"
					content="delete"
					:icon="{ tag: TrashXIcon }"
					:disabled="!deletable"
					@click="handleDeleteSubmit(close)"
				/>
			</div>
		</template>
	</a-modal>
</template>

<script setup lang="ts">
import { type FormKitNode, getNode, reset } from '@formkit/core';
import { computedEager } from '@vueuse/shared';
import { computed, onMounted, ref, watch } from 'vue';
import {
	DotsIcon,
	EditIcon,
	EditOffIcon,
	FileDescriptionIcon,
	RotateIcon,
	TrashXIcon,
} from 'vue-tabler-icons';

import { formkitVault } from '@/constants';
import { dudActionModalProps } from '@/types';

const emits = defineEmits<{
	(e: 'submit:delete'): void;
	(e: 'submit:update', data: unknown): void;
	(e: 'update:shown', t: boolean): void;
	(e: 'update:formkit-model-value', d: unknown): void;
	(e: 'reset'): void;
}>();
const props = defineProps(dudActionModalProps);

const fkAttrs = formkitVault.dataAttrs;
const formInitialValue = ref(props.formkit.initialValue);

const detailFormkitNode = ref<FormKitNode>();
const deletionShown = ref(false);
const isEditMode = ref(false);
const isFormChanged = ref(false);
const formModelValue = ref(formInitialValue.value);

const detailFormSchema = computed(() => ({ ...props.formkit }));
const updateFormSchema = computed(() => ({
	...(props.updateFormkit ?? props.formkit),
}));
const detailFormId = computedEager(() => {
	return props.formkit.id ?? `${props.entityName}-detail-action-form`;
});
const updateFormId = computedEager(() => {
	return props.formkit.id ?? `${props.entityName}-update-action-form`;
});
const iconEditToggleButton = computed(() => {
	const previewModeIcon = FileDescriptionIcon;
	const editModeIcon = EditIcon;
	const uneditableIcon = EditOffIcon;

	const editableIcon = isEditMode.value ? previewModeIcon : editModeIcon;
	return props.editable ? editableIcon : uneditableIcon;
});

const hideDudModal = () => {
	isEditMode.value = false;
	emits('update:shown', false);
};
const showDeletionModal = () => (deletionShown.value = !deletionShown.value);
const toggleMode = () => {
	props.editable && (isEditMode.value = !isEditMode.value);
};
const updateFormKitModelValue = (newVal: unknown) => {
	formModelValue.value = newVal as GenericObject;
	emits('update:formkit-model-value', newVal);
};
const handleDeleteSubmit = (close: () => void) => {
	close();
	hideDudModal();
	emits('submit:delete');
};
const handleEditSubmit = (data: unknown) => {
	if (!isFormChanged.value) return;

	formInitialValue.value = data as GenericObject;
	emits('submit:update', data);
	toggleMode();
	isFormChanged.value = false;
};
const resetForm = () => {
	if (!detailFormkitNode.value || !props.editable) return;

	const initialValue = JSON.parse(JSON.stringify(formInitialValue.value));

	formModelValue.value = initialValue;
	emits('reset');
	reset(detailFormId.value, initialValue);
	reset(updateFormId.value, initialValue);
};
const lockFormInputs = () => {
	detailFormkitNode.value?.children.forEach((c) => {
		if (!c.context) return;

		c.context.disabled = true;
		c.context.attrs[fkAttrs.names.lock] = true;
	});
};

function getDefaultTitle(action: string, title?: string, isSuffix = false) {
	return title ?? isSuffix
		? `${props.entityName} ${action}`
		: `${action} ${props.entityName}`;
}

watch(
	() => props.formkit.initialValue,
	(v) => {
		formModelValue.value = { ...v };
		formInitialValue.value = { ...v };
	},
	{ immediate: true }
);
watch(isEditMode, () => {
	if (!props.editable) return;

	lockFormInputs();
});
watch(
	formModelValue,
	(formValue) => {
		const initialValue = formInitialValue.value;

		if (!formValue || !initialValue) return;

		const initObject = Object.entries(initialValue).toString();
		const resultObj = Object.entries(formValue).toString();

		isFormChanged.value = resultObj !== initObject;
	},
	{ deep: true }
);
onMounted(() => {
	detailFormkitNode.value = getNode(detailFormId.value);

	lockFormInputs();
});
</script>

<style lang="scss">
.dud-modal {
	&__header {
		@apply flex justify-between gap-8;
	}

	&__footer {
		@include apply-scrollbar(#{&}, x, $auto-hide: false);
		@apply flex w-full justify-end gap-8;
	}

	&__action {
		&--update {
			@apply bg-accent-warning/20 text-gray-200 hover:enabled:bg-accent-warning/40;
		}
		&--delete {
			@apply ml-0 mr-auto bg-accent-error/30 text-rose-200 hover:bg-accent-error/50;
		}
		&--more {
			@include apply-hover-bubble($duration: '0.1s');
			@apply mr-3 flex items-center justify-center rounded-full bg-transparent text-main-senary wh-10;
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
		@include apply-component-theme('.formkit-themes--', 'table-action');
	}
}

.v-popper--theme-dud-modal-more-action {
	.v-popper__inner {
		@apply overflow-hidden rounded-secondary border border-main-quaternary/50 bg-main-primary py-1;
	}
	.v-popper__arrow-outer,
	.v-popper__arrow-inner {
		@apply border-transparent;
	}
}
</style>
