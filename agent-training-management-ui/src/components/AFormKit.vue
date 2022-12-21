<template>
	<form-kit
		v-if="type == 'form'"
		type="form"
		:actions="actions"
		:model-value="modelValue"
		:form-class="$attrs.class"
		@submit="handleSubmit"
		@submit-invalid="handleSubmitInvalid"
	>
		<form-kit-schema :schema="schema" :data="data" :library="library" />
	</form-kit>

	<form-kit
		v-else
		type="group"
		:model-value="modelValue"
		:form-class="$attrs.class"
	>
		<template #default="{ state: { valid } }">
			<form-kit-schema :schema="schema" :data="data" :library="library" />

			<ul v-if="$attrs.errors || !valid" class="formkit-messages">
				<li
					v-for="(error, index) in $attrs.errors"
					:key="index"
					class="formkit-message"
					data-message-type="error"
				>
					{{ error }}
				</li>
			</ul>
		</template>
	</form-kit>
</template>

<script setup lang="ts">
import type { FormKitSchemaCondition, FormKitSchemaNode } from '@formkit/core';
import { type PropType, ref } from 'vue';

const emits = defineEmits<{
	(e: 'submit', data: unknown): void;
	(e: 'submitInvalid', event: unknown): void;
}>();

defineProps({
	type: { type: String as PropType<'form' | 'schema'>, default: 'form' },
	data: Object,
	library: Object,
	modelValue: { type: [String, Object, Array] },
	schema: {
		type: Object as PropType<FormKitSchemaCondition | FormKitSchemaNode[]>,
		required: true,
	},
	actions: Boolean,
});

const submitted = ref(false);

const handleSubmitInvalid = (event: unknown) => emits('submitInvalid', event);
const handleSubmit = async (data: unknown) => {
	emits('submit', data);
	submitted.value = true;
};
</script>
