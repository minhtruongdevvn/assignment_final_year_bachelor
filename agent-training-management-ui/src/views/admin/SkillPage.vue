<template>
	<data-table
		v-model:reload="reloaded"
		row-index
		title="skill"
		class="skill-table"
		:response="sieveResponse"
		:schema="tableSchema"
		:modals="{
			entityName: 'skill',
			creation: { formkit: { schema: formSchema, data: formData } },
			dud: {
				deletable: true,
				editable: true,
				formkit: {
					schema: formSchema,
					data: formData,
					initialValue: formInitialValue,
				},
			},
		}"
		@shown:modal-creation="handleCreationModalShown"
		@shown:modal-dud="handleDudModalShown"
		@update:sieve="getSkillSieve"
		@submit:create="handleCreate"
		@submit:delete="handleDelete"
		@submit:update="handleUpdate"
	/>
</template>

<script setup lang="ts">
import type { FormKitSchemaNode } from '@formkit/core';
import { reactive, ref } from 'vue';

import { categoryAPIs, skillAPIs } from '@/apis';
import { defaultValues } from '@/constants';
import FilterOperation from '@/constants/filterOperations';
import type {
	CategoryResponse,
	Sieve,
	SkillUpsert,
	TablePropsSchema,
} from '@/types';

const apis = skillAPIs();

const sieveResponse = ref(defaultValues.getSieveResponse());
const categories = ref<CategoryResponse[]>();
const skillId = ref<string>();
const formData = reactive({ categories: {} });
const formInitialValue = ref({});
const reloaded = ref(false);

const getSkillSieve = async (sieve: Sieve) => {
	sieveResponse.value = await apis.get(sieve);
};
const getCategories = async () => {
	const res = await categoryAPIs().get();

	categories.value = res.data;

	if (!Object.keys(formData.categories).length) {
		formData.categories = Object.fromEntries(
			categories.value.map((c) => [c.id, c.name])
		);
	}
};

const handleCreationModalShown = async () => {
	await getCategories();
};
const handleDudModalShown = async (row: unknown) => {
	if (!Array.isArray(row)) return;

	await getCategories();

	const id = row.find((t) => t.key == 'id')?.value as string;
	const skill = await apis.getById(id);

	if (!skill?.category?.id) {
		const cateName = row.find((r) => r.key == 'category').value.name;
		const cateId = categories.value?.find((c) => c.name == cateName)?.id;

		cateId && (skill.category = { ...skill.category, id: cateId });
	}

	if (skillId.value != skill.id) skillId.value = skill.id?.toString();

	formInitialValue.value = {
		name: skill.name,
		category_id: skill.category?.id,
		description: skill.description,
	};
};

const reload = () => (reloaded.value = true);

const handleCreate = async (data: unknown) => {
	await apis.add(data as SkillUpsert);
	reload();
};
const handleUpdate = async (data: unknown) => {
	const id = skillId.value;

	if (id) {
		await apis.update(id, data as SkillUpsert);
		reload();
	}
};
const handleDelete = async () => {
	const id = skillId.value;

	if (id) {
		await apis.delete(id);
		reload();
	}
};

const tableSchema: TablePropsSchema = [
	{
		key: 'id',
		hidden: true,
		sortable: true,
		filterable: true,
		filterOperation: FilterOperation.equal,
	},
	{ key: 'name', sortable: true, filterable: true },
	{ key: 'description' },
	{
		type: 'tag',
		name: 'types',
		key: 'category',
		options: { displayProp: 'name' },
	},
];
const formSchema: FormKitSchemaNode[] = [
	{
		$formkit: 'text',
		label: 'name',
		name: 'name',
		validation: 'required|length:3,250',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'aselect',
		type: 'aselect',
		label: 'category',
		name: 'category_id',
		validation: 'required',
		validationVisibility: 'dirty',
		options: '$categories',
	},
	{
		$formkit: 'textarea',
		label: 'description',
		name: 'description',
		validation: '?length:0,200',
		validationVisibility: 'dirty',
	},
];
</script>
