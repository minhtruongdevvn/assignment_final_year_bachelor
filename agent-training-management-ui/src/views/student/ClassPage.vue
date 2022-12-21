<template>
	<data-table
		row-index
		title="class"
		class="class-table"
		:response="sieveResponse"
		:schema="tableSchema"
		:modals="{
			entityName: 'class',
			dud: {
				formkit: {
					schema: formSchema,
					data: formData,
					initialValue: formInitialValue,
				},
			},
		}"
		@shown:modal-dud="handleDudModalShown"
		@update:sieve="getClassSieve"
	/>
</template>

<script setup lang="ts">
import type { FormKitSchemaNode } from '@formkit/core';
import { reactive, ref } from 'vue';

import { classAPIs, skillAPIs } from '@/apis';
import { defaultValues } from '@/constants';
import { useUserStore } from '@/stores';
import type { ClassUpsert, Sieve, TablePropsSchema } from '@/types';
import { getValidDate } from '@/utils';

const apis = classAPIs();
const userStore = useUserStore();

const sieveResponse = ref(defaultValues.getSieveResponse());
const classId = ref<string>();
const formData = reactive({ skills: {} });
const formInitialValue = ref<ClassUpsert>({});

const getClassSieve = async (sieve: Sieve) => {
	if (!userStore.profile?.sub) return;

	sieveResponse.value = await apis.getClassByLecturer(
		userStore.profile.sub,
		sieve
	);
};

const getSkills = async () => {
	const skills = await skillAPIs().getAll();

	if (!Object.keys(formData.skills).length) {
		formData.skills = Object.fromEntries(skills.map((c) => [c.id, c.name]));
	}
};
const handleDudModalShown = async (row: unknown) => {
	if (!Array.isArray(row)) return;

	await getSkills();

	const id = row.find((t) => t.key == 'id')?.value as string;
	const classDetail = await apis.getById(id);

	classId.value = classDetail.id?.toString();
	classDetail.start_date = getValidDate(classDetail.start_date!, 'fr-CA');
	classDetail.end_date = getValidDate(classDetail.end_date, 'fr-CA');
	formInitialValue.value = {
		...classDetail,
		skill_id: classDetail.skill.id,
	};
};

const tableSchema: TablePropsSchema = [
	{ key: 'id', hidden: true, sortable: true, filterable: true },
	{ name: 'placement', key: 'placement', sortable: true, filterable: true },
	{
		name: 'start',
		key: 'start_date',
		sortable: true,
		filterable: true,
		options: { isDateTypeData: true },
	},
	{
		name: 'end',
		key: 'end_date',
		sortable: true,
		filterable: true,
		options: { isDateTypeData: true },
	},
	{ name: 'available', key: 'available', sortable: true, filterable: true },
	{
		type: 'tag',
		name: 'skill',
		key: 'skill',
		options: { displayProp: 'name' },
		filterable: true,
	},
	{
		name: 'max_learner',
		key: 'max_learner',
		sortable: true,
		filterable: true,
	},
	{
		name: 'enable automation',
		key: 'enable_automation',
		sortable: true,
		filterable: true,
	},
];
const formSchema: FormKitSchemaNode[] = [
	{ $formkit: 'text', label: 'name', name: 'name' },
	{ $formkit: 'text', label: 'placement', name: 'placement' },
	{ $formkit: 'date', label: 'start', name: 'start_date' },
	{ $formkit: 'date', label: 'end', name: 'end_date' },
	{ $formkit: 'text', label: 'available', name: 'available' },
	{ $formkit: 'text', label: 'automation', name: 'enable_automation' },
	{ $formkit: 'number', label: 'max leaners', name: 'max_learner' },
	{
		$formkit: 'aselect',
		label: 'skill',
		name: 'skill_id',
		options: '$skills',
	},
	{ $formkit: 'textarea', label: 'description', name: 'description' },
];
</script>

<style lang="scss">
.dud-more-actions__update-student-container {
	.formkit {
		&-input {
			@apply h-10 w-full items-center justify-center rounded-md border border-main-quaternary transition focus-within:border-accent-primary;
		}
		&-label {
			@apply capitalize;
		}
		&-wrapper {
			@apply flex flex-col gap-3;
		}
	}
}
</style>
