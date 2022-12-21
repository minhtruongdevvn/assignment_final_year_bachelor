<template>
	<a-modal
		:model-value="shown"
		esc-to-close
		:title="validatedProps.title"
		class="creation-modal"
		@update:model-value="hideMainModal"
	>
		<a-form-kit
			:id="validatedProps.id"
			v-model="formDetails"
			:schema="formkit.schema"
			:data="formkit.data"
			@submit="showConfirmation"
		/>

		<template #footer="{ close }">
			<div class="creation-modal__footer">
				<a-button
					class="context-modal-action-btn btn--cancel creation-modal__action--cancel"
					content="cancel"
					@click="close"
				/>
				<a-button
					class="context-modal-action-btn btn--icon creation-modal__action--create"
					content="create"
					type="submit"
					:icon="{ left: { tag: SquarePlusIcon } }"
					:form="validatedProps.id"
				/>
			</div>
		</template>
	</a-modal>

	<a-modal
		v-model="confirmShown"
		esc-to-close
		container-class="creation-modal"
		title="Are you sure?"
	>
		<a-form-kit
			:id="confirmFormkitId"
			v-model="formDetails"
			:data="formkit.data"
			:schema="formkit.schema"
			@submit="handleConfirm"
		/>

		<template #footer="{ close }">
			<div class="creation-modal__footer">
				<a-button
					class="context-modal-action-btn creation-modal__action--cancel"
					content="cancel"
					@click="close"
				/>
				<a-button
					class="context-modal-action-btn creation-modal__action--confirm"
					content="confirm"
					type="submit"
					:form="confirmFormkitId"
					@click="close"
				/>
			</div>
		</template>
	</a-modal>
</template>

<script setup lang="ts">
import { type FormKitNode, getNode, reset } from '@formkit/core';
import { computed, onMounted, ref } from 'vue';
import { SquarePlusIcon } from 'vue-tabler-icons';

import { formkitVault } from '@/constants';
import { createActionModalProps } from '@/types';

const emits = defineEmits<{
	(e: 'submit:create', data: unknown): void;
	(e: 'update:shown', t: boolean): void;
}>();
const props = defineProps(createActionModalProps);

const formkitNode = ref<FormKitNode>();
const confirmShown = ref(false);
const formDetails = ref({});

const validatedProps = computed(() => ({
	...props,
	id: props.formkit.id ?? `${props.entityName}-creation-form`,
	title: props.title ?? `create ${props.entityName}`,
}));

const hideMainModal = () => {
	formDetails.value = {};

	if (formDetails.value) reset(validatedProps.value.id, {});

	emits('update:shown', false);
};
const handleConfirm = () => {
	emits('submit:create', formDetails.value);
	emits('update:shown', false);
};
const showConfirmation = (data: unknown) => {
	formDetails.value = data as GenericObject;
	confirmShown.value = true;
};

const confirmFormkitId = 'createConfirmModal';

onMounted(() => {
	formkitNode.value = getNode(confirmFormkitId);

	const attrs = formkitVault.dataAttrs;

	formkitNode.value?.children.forEach((c) => {
		if (!c.context) return;

		c.context.disabled = true;
		c.context.attrs[attrs.names.lock] = true;
	});
});
</script>

<style lang="scss">
.creation-modal {
	&__footer {
		@apply flex justify-end gap-12;
	}

	&__action {
		&--confirm,
		&--create {
			@apply bg-accent-tertiary/10 text-accent-tertiary hover:bg-accent-tertiary/20;
		}
	}

	.a-modal__body {
		@include apply-component-theme('.formkit-themes--', 'table-action');
	}
}
</style>
