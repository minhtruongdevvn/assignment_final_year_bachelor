<template>
	<data-table
		v-model:reload="reloaded"
		row-index
		title="lecturer"
		:response="sieveResponse"
		:schema="tableSchema"
		:modals="{
			entityName: 'lecturer',
			creation: { formkit: { schema: baseFormSchema } },
			dud: {
				deletable: true,
				editable: true,
				formkit: {
					schema: dudFormSchema,
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

import { lecturerAPIs } from '@/apis';
import { defaultValues } from '@/constants';
import type {
	LecturerInsert,
	LecturerResponse,
	LecturerUpdate,
	Sieve,
	TablePropsSchema,
} from '@/types';
import { getValidDate, Kt } from '@/utils';

const api = lecturerAPIs();

const sieveResponse = ref(defaultValues.getSieveResponse());
const lecturerId = ref<string>();
const formInitialValue = ref({});
const reloaded = ref(false);

const getSieve = async (sieve: Sieve) => {
	sieveResponse.value = await api.get(sieve);
};

const handleDudModalShown = async (row: unknown) => {
	if (!Array.isArray(row)) return;

	const id = row.find((t) => t.key == 'id')?.value as string;
	const lecturer = await api.getById(id);

	if (lecturerId.value != lecturer.id) {
		lecturerId.value = lecturer.id?.toString();
	}

	if (lecturer.birth_date) {
		lecturer.birth_date = getValidDate(lecturer.birth_date, 'fr-CA');
	}

	formInitialValue.value = lecturer;
};

const reload = () => {
	reloaded.value = true;
};

const handleCreate = async (data: unknown) => {
	await api.add(data as LecturerInsert);
	reload();
};
const handleUpdate = async (data: unknown) => {
	new Kt(lecturerId.value)?.also(async (id) => {
		const request = new Kt(data as LecturerResponse).let<LecturerUpdate>(
			(data) => ({
				email: data.email,
				password: data.password,
				family_name: data.family_name,
				given_name: data.given_name,
				birth_date: data.birth_date,
			})
		).value;

		if (request) {
			await api.update(id, request);
		}
	});
	reload();
};
const handleDelete = async () => {
	new Kt(lecturerId.value)?.also(async (id) => {
		await api.delete(id);
	});
	reload();
};

const tableSchema: TablePropsSchema = [
	{ key: 'id', hidden: true },
	{ key: 'email', sortable: true, filterable: true },
	{ key: 'given_name', name: 'given name', sortable: true, filterable: true },
	{
		key: 'family_name',
		name: 'family name',
		sortable: true,
		filterable: true,
	},
	{ key: 'code', sortable: true, filterable: true },
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
const dudFormSchema: FormKitSchemaNode[] = (() => {
	const newSchema = [...baseFormSchema];

	newSchema.splice(1, 2);

	return newSchema;
})();
</script>
