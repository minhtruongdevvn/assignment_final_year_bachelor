<template>
	<data-table
		v-model:reload="reloaded"
		row-index
		title="operator"
		class="student-table"
		:response="sieveResponse"
		:schema="tableSchema"
		:modals="{
			entityName: 'operator',
			creation: { formkit: { schema: baseFormSchema } },
			dud: {
				deletable: true,
				editable: true,
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
		@shown:modal-dud="handleDudModalShown"
		@update:sieve="getSieve"
		@submit:create="handleCreate"
		@submit:delete="handleDelete"
		@submit:update="handleUpdate"
	/>
</template>

<script setup lang="ts">
import type { FormKitSchemaNode } from '@formkit/core';
import { ref } from 'vue';

import { operatorAPIs } from '@/apis';
import type DataTableVue from '@/components/table/DataTable.vue';
import { defaultValues } from '@/constants';
import type {
	OperatorInsert,
	OperatorResponse,
	OperatorUpdate,
	Sieve,
	TablePropsSchema,
} from '@/types';

const api = operatorAPIs();

const sieveResponse = ref(defaultValues.getSieveResponse());
const operatorId = ref<string>();
const formInitialValue = ref({});
const reloaded = ref(false);

const getSieve = async (sieve: Sieve) => {
	sieveResponse.value = await api.get(sieve);
};

const handleDudModalShown = async (row: unknown) => {
	if (!Array.isArray(row)) return;

	const id = row.find((t) => t.key == 'id')?.value as string;
	const operator = await api.getById(id);

	console.log(operator.password, 'aa');

	if (operatorId.value != operator.id)
		operatorId.value = operator.id?.toString();

	if (operator.birth_date) {
		operator.birth_date = new Date(operator.birth_date)
			.toISOString()
			.slice(0, 10);
	}

	formInitialValue.value = { ...operator };
	console.log(formInitialValue.value);
};

const reload = () => (reloaded.value = true);

const handleCreate = async (data: unknown) => {
	await api.add(data as OperatorInsert);
	reload();
};
const handleUpdate = async (data: unknown) => {
	const id = operatorId.value;

	if (id) {
		const newData = data as OperatorResponse;
		const request: OperatorUpdate = {
			email: newData.email,
			password: newData.password,
			family_name: newData.family_name,
			given_name: newData.given_name,
			birth_date: newData.birth_date,
		};

		console.log({ data, request });
		await api.update(id, request);
		reload();
	}
};
const handleDelete = async () => {
	const id = operatorId.value;

	if (id) {
		await api.delete(id);
		reload();
	}
};

const tableSchema: TablePropsSchema = [
	{ key: 'id', hidden: true },
	{ key: 'given_name', name: 'given name', sortable: true, filterable: true },
	{
		key: 'family_name',
		name: 'family name',
		sortable: true,
		filterable: true,
	},
	{ key: 'email', name: 'email', sortable: true, filterable: true },
	{ key: 'code', name: 'code', sortable: true, filterable: true },
	{
		key: 'birth_date',
		name: 'birth date',
		sortable: true,
		filterable: true,
		options: { isDateTypeData: true },
	},
];
const baseFormSchema: FormKitSchemaNode[] = [
	{
		$formkit: 'email',
		label: 'email',
		name: 'email',
		validation: 'required|email',
		validationVisibility: 'live',
	},
	{
		$formkit: 'password',
		label: 'password',
		name: 'password',
		validation: 'required|matches:/^[A-Z][a-z0-9A-Z]+$/|length:6,250',
		validationVisibility: 'live',
	},
	{
		$formkit: 'password',
		label: 'confirm password',
		name: 'password_confirm',
		validation: 'required|confirm',
		validationVisibility: 'live',
		validationLabel: 'Password confirmation',
	},
	{
		$formkit: 'text',
		label: 'family name',
		name: 'family_name',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'text',
		label: 'given name',
		name: 'given_name',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'date',
		label: 'birth date',
		name: 'birth_date',
		validation: 'required',
		validationVisibility: 'dirty',
	},
];
const detailFormSchema: FormKitSchemaNode[] = (() => {
	const newSchema = [...baseFormSchema];

	newSchema.splice(1, 2);

	return newSchema;
})();
const updateFormSchema: FormKitSchemaNode[] = (() => {
	const newSchema = [...baseFormSchema];

	newSchema.splice(1, 2);

	return newSchema;
})();
</script>

<style scoped></style>
