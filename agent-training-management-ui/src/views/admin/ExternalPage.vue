<template>
	<data-table
		v-model:reload="reloaded"
		row-index
		title="External Institution"
		class="student-table"
		:response="sieveResponse"
		:schema="tableSchema"
		:modals="{
			entityName: 'external institution',
			creation: { formkit: { schema: baseFormSchema } },
			dud: {
				deletable: true,
				editable: true,
				moreActions: true,
				updateFormkit: {
					schema: updateFormSchema,
					initialValue: formInitialValue,
				},
				formkit: {
					schema: detailFormSchema,
					initialValue: formInitialValue,
				},
			},
		}"
		@shown:modal-creation="handleCreationModalShown"
		@shown:modal-dud="handleDudModalShown"
		@update:sieve="getSieve"
		@submit:create="handleCreate"
		@submit:delete="handleDelete"
		@submit:update="handleUpdate"
	>
		<template #more-actions>
			<div class="dud-more-actions">
				<a-button
					content="Regenerate"
					:icon="{ tag: RefreshIcon }"
					@click="handleRegeneratePassCode()"
				/>
			</div>
		</template>
	</data-table>
	<a-modal
		esc-to-close
		:model-value="showModel"
		title="External credential"
		class="dud-modal"
		@update:model-value="showModel = false"
	>
		<div>
			<b>Code</b>
			<div>{{ createdCode }}</div>
		</div>

		<div>
			<b>Pass code</b>
			<div>{{ createdPassCode }}</div>
		</div>

		<template #footer="{ close }">
			<div class="dud-modal__footer">
				<a-button
					class="context-modal-action-btn btn--icon dud-modal__action--create"
					content="Yes, I have saved the credential"
					@click="close"
				/>
			</div>
		</template>
	</a-modal>
</template>

<script setup lang="ts">
import type { FormKitSchemaNode } from '@formkit/core';
import { reactive, ref } from 'vue';
import { RefreshIcon } from 'vue-tabler-icons';

import { externalAPIs, skillAPIs } from '@/apis';
import { defaultValues } from '@/constants';
import type {
	ExternalResponse,
	ExternalUpsert,
	Sieve,
	TablePropsSchema,
} from '@/types';

const api = externalAPIs();
const showModel = ref(false);
const createdCode = ref('');
const createdPassCode = ref('');
const sieveResponse = ref(defaultValues.getSieveResponse());
const externalId = ref<string>();
const formInitialValue = ref({});
const reloaded = ref(false);
const formData = reactive({ skills: {} });

const getSieve = async (sieve: Sieve) => {
	sieveResponse.value = await api.get(sieve);
};
const getSkills = async () => {
	const skills = await skillAPIs().getAll();

	if (!Object.keys(formData.skills).length) {
		formData.skills = Object.fromEntries(skills.map((c) => [c.id, c.name]));
	}
};

const handleCreationModalShown = async () => {
	await getSkills();
};
const handleDudModalShown = async (row: unknown) => {
	if (!Array.isArray(row)) return;

	await getSkills();

	const id = row.find((t) => t.key == 'id')?.value as string;
	const external = await api.getById(id);

	if (externalId.value != external.id)
		externalId.value = external.id?.toString();

	formInitialValue.value = { ...external };
};

const reload = () => (reloaded.value = true);

const handleCreate = async (data: unknown) => {
	const response = await api.add(data as ExternalUpsert);

	createdPassCode.value = response.pass_code!;
	createdCode.value = response.code!;
	reload();
	showModel.value = true;
};

const handleRegeneratePassCode = async () => {
	const response = await api.regeneratePassCode(externalId.value!);

	createdPassCode.value = response.pass_code!;
	createdCode.value = response.code!;
	showModel.value = true;
};

const handleUpdate = async (data: unknown) => {
	const id = externalId.value;

	if (id) {
		const newData = data as ExternalResponse;
		const request: ExternalUpsert = {
			skill_ids: newData.skill_ids,
		};

		console.log({ data, request });
		await api.update(id, request);
		reload();
	}
};
const handleDelete = async () => {
	const id = externalId.value;

	if (id) {
		await api.delete(id);
		reload();
	}
};

const tableSchema: TablePropsSchema = [
	{ key: 'id', hidden: true },
	{
		key: 'name',
		name: 'name',
		sortable: true,
		filterable: true,
	},
	{ key: 'code', name: 'code', filterable: true },
	{ key: 'skill_names', name: 'skills' },
];

const baseFormSchema: FormKitSchemaNode[] = [
	{
		$formkit: 'text',
		label: 'Skills',
		name: 'skill_ids',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'text',
		label: 'External name',
		name: 'name',
		validation: 'required',
		validationVisibility: 'dirty',
	},
];
const detailFormSchema: FormKitSchemaNode[] = (() => {
	const newSchema = [
		...baseFormSchema,
		{
			$formkit: 'text',
			label: 'Code',
			name: 'code',
			validation: 'required',
			validationVisibility: 'dirty',
		},
	];

	return newSchema;
})();
const updateFormSchema: FormKitSchemaNode[] = (() => {
	const newSchema = [...detailFormSchema];

	newSchema.splice(1);

	return newSchema;
})();
</script>

<style scoped></style>
