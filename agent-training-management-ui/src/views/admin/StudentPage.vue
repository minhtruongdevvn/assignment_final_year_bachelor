<template>
	<data-table
		ref="dataTableRef"
		v-model:reload="reloaded"
		row-index
		title="student"
		class="student-table"
		:response="sieveResponse"
		:schema="tableSchema"
		:modals="{
			entityName: 'student',
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

import { studentAPIs } from '@/apis';
import { defaultValues } from '@/constants';
import FilterOperation from '@/constants/filterOperations';
import type {
	Sieve,
	StudentInsert,
	StudentResponse,
	StudentUpdate,
	TablePropsSchema,
} from '@/types';

const api = studentAPIs();

const sieveResponse = ref(defaultValues.getSieveResponse());
const studentId = ref<string>();
const reloaded = ref(false);

const formInitialValue = ref({});

const getSieve = async (sieve: Sieve) => {
	sieveResponse.value = await api.get(sieve);
};

const handleDudModalShown = async (row: unknown) => {
	if (!Array.isArray(row)) return;

	const id = row.find((t) => t.key == 'id')?.value as string;
	const student = await api.getById(id);

	if (studentId.value != student.id) studentId.value = student.id?.toString();

	if (student.birth_date) {
		student.birth_date = new Date(student.birth_date)
			.toISOString()
			.slice(0, 10);
	}

	formInitialValue.value = { ...student };
};

const reload = () => (reloaded.value = true);

const handleCreate = async (data: unknown) => {
	await api.add(data as StudentInsert);
	reload();
};
const handleUpdate = async (data: unknown) => {
	const id = studentId.value;

	if (!id) return;

	const newData = data as StudentResponse;
	const request: StudentUpdate = {
		sex: newData.sex,
		age: newData.age,
		identify_number: newData.identify_number,
		email: newData.email,
		family_name: newData.family_name,
		given_name: newData.given_name,
		birth_date: newData.birth_date,
	};

	await api.update(id, request);
	reload();
};
const handleDelete = async () => {
	const id = studentId.value;

	if (!id) return;

	await api.delete(id);
	reload();
};

const tableSchema: TablePropsSchema = [
	{ key: 'id', hidden: true },
	{ name: 'identifier', key: 'identify_number' },
	{
		key: 'family_name',
		name: 'family name',
		sortable: true,
		filterable: true,
	},
	{ key: 'given_name', name: 'given name', sortable: true, filterable: true },
	{ key: 'sex', name: 'is female', sortable: true, filterable: true },
	{
		key: 'stamina',
		sortable: true,
		filterable: true,
		filterOperation: FilterOperation.equal,
	},
	{ key: 'strength', sortable: true, filterable: true },
	{
		key: 'reaction_time',
		name: 'reaction (ms)',
		sortable: true,
		filterable: true,
		filterOperation: FilterOperation.equal,
	},
	{
		key: 'self_discipline',
		name: 'self discipline',
		sortable: true,
		filterable: true,
		filterOperation: FilterOperation.equal,
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
		$el: 'div',
		attrs: { class: 'border-t h-0 border-main-quaternary mb-8' },
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
	{
		$formkit: 'number',
		label: 'height',
		name: 'height',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'number',
		label: 'iq',
		name: 'iq',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'number',
		label: 'eq',
		name: 'eq',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'number',
		label: 'stamina',
		name: 'stamina',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'number',
		label: 'strength',
		name: 'strength',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'number',
		label: 'reaction time',
		name: 'reaction_time',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'aselect',
		label: 'is female',
		name: 'sex',
		options: ['true', 'false'],
	},
	{
		$formkit: 'number',
		label: 'age',
		name: 'age',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'text',
		label: 'identifier',
		name: 'identify_number',
		validation: 'required',
		validationVisibility: 'dirty',
	},
];
const detailFormSchema: FormKitSchemaNode[] = (() => {
	const newSchema = [...baseFormSchema];

	newSchema.splice(1, 3);

	return newSchema;
})();
const updateFormSchema: FormKitSchemaNode[] = (() => {
	const newSchema = [...detailFormSchema];

	newSchema.splice(4, 6);

	return newSchema;
})();
</script>

<style scoped></style>
