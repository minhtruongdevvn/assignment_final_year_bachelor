<template>
	<data-table
		v-model:reload="reloaded"
		row-index
		title="class"
		class="class-table"
		:response="sieveClassResponse"
		:schema="tableClassSchema"
		:modals="{
			entityName: 'class',
			creation: {
				formkit: { schema: tableClassFormSchema, data: formData },
			},
			dud: {
				deletable: true,
				editable: true,
				moreActions: true,
				formkit: {
					schema: tableClassFormSchema,
					data: formData,
					initialValue: formInitialValue,
				},
			},
		}"
		@shown:modal-creation="handleCreationModalShown"
		@shown:modal-dud="handleDudModalShown"
		@update:sieve="getClassSieve"
		@submit:create="handleCreate"
		@submit:delete="handleDelete"
		@submit:update="handleUpdate"
	>
		<template #more-actions="{ hideTooltip }">
			<div class="dud-more-actions">
				<a-button
					content="add lecturers"
					@click="showModal(hideTooltip, 'lecturer')"
				/>
				<a-button
					content="add students"
					@click="showModal(hideTooltip, 'student')"
				/>
				<a-button
					content="add slots"
					@click="showModal(hideTooltip, 'slot')"
				/>
				<a-button
					content="remove slots"
					@click="showModalRemove(hideTooltip, 'slot')"
				/>
				<a-button
					content="remove lecturers"
					@click="showModalRemove(hideTooltip, 'lecturer')"
				/>
			</div>
		</template>

		<template #custom-modals>
			<a-modal
				v-model="addModalShown"
				close-button
				esc-to-close
				container-class="dud-more-actions-container"
				:title="
					(isRemove ? 'remove' : 'add') +
					` ${addActionModes}s ` +
					(isRemove ? 'from' : 'to') +
					' class'
				"
			>
				<div class="dud-more-actions-add">
					<data-table
						v-if="!isRemove"
						v-model:reload="addActionReloaded"
						row-index
						:title="addActionModes ?? ''"
						:response="addActionTableInfo.sieveRes"
						:schema="addActionTableInfo.tableSchema"
						@update:sieve="getAddActionSieve"
						@click:row="handleRowClickToAdd"
					/>

					<data-table
						v-if="isRemove"
						v-model:reload="addActionReloaded"
						row-index
						:title="addActionModes ?? ''"
						:response="addActionTableInfo.sieveRes"
						:schema="addActionTableInfo.tableSchema"
						@update:sieve="getRemoveActionSieve"
						@click:row="handleRowClickToRemove"
					/>
				</div>
			</a-modal>
		</template>
	</data-table>
</template>

<script setup lang="ts">
import type { FormKitSchemaNode } from '@formkit/core';
import { computed, reactive, ref } from 'vue';

import {
	classAPIs,
	lecturerAPIs,
	skillAPIs,
	slotAPIs,
	studentAPIs,
} from '@/apis';
import { defaultValues } from '@/constants';
import FilterOperation from '@/constants/filterOperations';
import type {
	ClassUpsert,
	LecturerResponse,
	Sieve,
	SieveResponse,
	SlotResponse,
	StudentResponse,
	TablePropsSchema,
} from '@/types';
import { getValidDate, Kt } from '@/utils';

type AddActionModes = 'lecturer' | 'student' | 'slot';

const classApi = classAPIs();
const lectApi = lecturerAPIs();
const studApi = studentAPIs();
const slotApi = slotAPIs();
/* const filterToAddOptions = (() => {
	const { contain } = FilterOperation;
	const identityConfigs = [
		{ name: 'code', display: 'Find by code', operator: contain },
		{ name: 'email', display: 'Find by email', operator: contain },
		{
			name: 'family_name',
			display: 'Find by family name',
			operator: contain,
		},
		{
			name: 'given_name',
			display: 'Find by given name',
			operator: contain,
		},
	];
	const slotConfigs = [
		{
			name: 'day_of_week',
			display: 'Find by day of week',
			operator: contain,
		},
		{ name: 'start_at', display: 'Find by start time', operator: contain },
		{ name: 'end_at', display: 'Find by end time', operator: contain },
	];

	const identityFormkit = Object.fromEntries(
		identityConfigs.map((c) => [c.name, c.display])
	);
	const identityApi = identityConfigs.map((c) => ({
		name: c.name,
		operator: c.operator,
	}));
	const slotFormkit = Object.fromEntries(
		slotConfigs.map((c) => [c.name, c.display])
	);
	const slotApi = slotConfigs.map((c) => ({
		name: c.name,
		operator: c.operator,
	}));

	return {
		identity: { formkit: identityFormkit, api: identityApi },
		slot: { formkit: slotFormkit, api: slotApi },
	};
})(); */

const fakeSieve = ref(defaultValues.getSieve());
const sieveClassResponse = ref(defaultValues.getSieveResponse());
const sieveLecturerResponse = ref(defaultValues.getSieveResponse());
const sieveStudentResponse = ref(defaultValues.getSieveResponse());
const sieveSlotResponse = ref(defaultValues.getSieveResponse());
const classId = ref<string>();
const formInitialValue = ref<ClassUpsert>({});
const formData = reactive({ skills: {} });
const reloaded = ref(false);
const addActionReloaded = ref(false);
const addActionModes = ref<AddActionModes>();
const addModalShown = ref(false);
const isRemove = ref(false);

/* const actionBasedOptions = computedEager(() => {
	return addActionModes.value != 'slot'
		? filterToAddOptions.identity
		: filterToAddOptions.slot;
});
const initialKeySearchEntity = computedEager(
	() => Object.keys(actionBasedOptions.value.formkit)[0]
); */

const getClassSieve = async (sieve: Sieve) => {
	sieveClassResponse.value = await classApi.get(sieve);
};
const getAddActionSieve = async (sieve: Sieve) => {
	if (!classId.value) return;

	fakeSieve.value = sieve;

	switch (addActionModes.value) {
		case 'lecturer':
			sieveLecturerResponse.value = await lectApi.getUnassignedLecturer(
				classId.value,
				sieve
			);
			break;
		case 'student':
			sieveStudentResponse.value = await studApi.getUnassignedStudent(
				classId.value,
				sieve
			);
			break;
		case 'slot':
			sieveSlotResponse.value = await slotApi.getUnassignedSlot(
				classId.value,
				sieve
			);
			break;
	}
};
const getRemoveActionSieve = async (sieve: Sieve) => {
	if (!classId.value) return;

	fakeSieve.value = sieve;

	switch (addActionModes.value) {
		case 'lecturer':
			sieveLecturerResponse.value = await lectApi.getAssignedLecturer(
				classId.value,
				sieve
			);
			break;
		case 'slot':
			sieveSlotResponse.value = await slotApi.getAssignedSlot(
				classId.value,
				sieve
			);
			break;
	}
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
	const classDetail = await classApi.getById(id);

	classId.value = classDetail.id?.toString();

	classDetail.start_date = getValidDate(classDetail.start_date!, 'fr-CA');
	classDetail.end_date = getValidDate(classDetail.end_date, 'fr-CA');
	formInitialValue.value = {
		...classDetail,
		skill_id: classDetail.skill.id,
	};
};

const showModal = async (hideTooltip: () => void, mode: AddActionModes) => {
	isRemove.value = false;
	addModalShown.value = true;
	addActionModes.value = mode;

	hideTooltip();
	await getAddActionSieve(fakeSieve.value);
};
const showModalRemove = async (
	hideTooltip: () => void,
	mode: AddActionModes
) => {
	isRemove.value = true;
	addModalShown.value = true;
	addActionModes.value = mode;

	hideTooltip();
	await getRemoveActionSieve(fakeSieve.value);
};

const reload = () => (reloaded.value = true);
const reloadaddActionReloaded = () => (addActionReloaded.value = true);

const handleCreate = async (data: unknown) => {
	await classApi.add(data as ClassUpsert);
	reload();
};
const handleUpdate = async (data: unknown) => {
	new Kt(classId.value)?.also(async (id) => {
		await classApi.update(id, data as ClassUpsert);
		reload();
	});
};
const handleDelete = async () => {
	new Kt(classId.value)?.also(async (id) => {
		await classApi.delete(id);
		reload();
	});
};
const handleRowClickToAdd = async (row: unknown) => {
	new Kt(classId.value)?.also(async (id) => {
		if (!Array.isArray(row)) return;

		const entityId = row.find((t) => t.key == 'id')?.value as string;

		switch (addActionModes.value) {
			case 'lecturer':
				await classApi.addLecturerToClass(id, entityId);
				break;
			case 'student':
				await classApi.addStudentToClass(id, entityId);
				break;
			case 'slot':
				await classApi.addSlotToClass(id, entityId);
				break;
		}
	});

	addModalShown.value = false;
	reload();
	reloadaddActionReloaded();
};
const handleRowClickToRemove = async (row: unknown) => {
	new Kt(classId.value)?.also(async (id) => {
		if (!Array.isArray(row)) return;

		const entityId = row.find((t) => t.key == 'id')?.value as string;

		switch (addActionModes.value) {
			case 'lecturer':
				await classApi.removeLecturerFromClass(id, entityId);
				break;
			case 'slot':
				await classApi.removeSlotFromClass(id, entityId);
				break;
			default:
				break;
		}
	});

	addModalShown.value = false;
	reload();
	reloadaddActionReloaded();
};

const tableClassSchema: TablePropsSchema = [
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
const tableLecturerTableSchema: TablePropsSchema = [
	{ key: 'id', hidden: true, sortable: true, filterable: true },
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
const tableStudentTableSchema: TablePropsSchema = [
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
const tableSlotTableSchema: TablePropsSchema = [
	{ key: 'id', hidden: true },
	{
		key: 'day_of_week',
		name: 'day of week',
		sortable: true,
		filterable: true,
	},
	{
		key: 'start_at',
		name: 'start',
		sortable: true,
		filterable: true,
	},
	{
		key: 'end_at',
		name: 'end',
		sortable: true,
		filterable: true,
	},
];

const tableClassFormSchema: FormKitSchemaNode[] = [
	{
		$formkit: 'text',
		label: 'name',
		name: 'name',
		validation: 'required|length:3,250',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'text',
		label: 'placement',
		name: 'placement',
		validation: 'required|length:3,250',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'date',
		label: 'start',
		name: 'start_date',
		validation: 'required',
	},
	{
		$formkit: 'date',
		label: 'end',
		name: 'end_date',
	},
	{
		$formkit: 'aselect',
		label: 'available',
		name: 'available',
		validation: 'required',
		validationVisibility: 'dirty',
		options: [true, false],
	},
	{
		$formkit: 'aselect',
		label: 'automation',
		name: 'enable_automation',
		validation: 'required',
		validationVisibility: 'dirty',
		options: [true, false],
	},
	{
		$formkit: 'number',
		label: 'max leaners',
		name: 'max_learner',
		validation: 'required',
		validationVisibility: 'dirty',
	},
	{
		$formkit: 'aselect',
		label: 'skill',
		name: 'skill_id',
		validation: 'required',
		validationVisibility: 'dirty',
		options: '$skills',
	},
	{
		$formkit: 'textarea',
		label: 'description',
		name: 'description',
		validation: '?length:0,200',
		validationVisibility: 'dirty',
	},
];

const addActionTableInfo = computed(() => {
	let sieveRes: Nullable<
		SieveResponse<LecturerResponse | StudentResponse | SlotResponse>
	> = undefined;
	let tableSchema: Nullable<TablePropsSchema> = undefined;

	switch (addActionModes.value) {
		case 'lecturer':
			sieveRes = sieveLecturerResponse.value;
			tableSchema = tableLecturerTableSchema;
			break;
		case 'student':
			sieveRes = sieveStudentResponse.value;
			tableSchema = tableStudentTableSchema;
			break;
		case 'slot':
			sieveRes = sieveSlotResponse.value;
			tableSchema = tableSlotTableSchema;

			break;
	}

	return { sieveRes, tableSchema };
});
</script>

<style lang="scss">
.dud-more-actions {
	button {
		@apply justify-start rounded-none py-[6px] px-4 font-title text-sm capitalize tracking-wide text-main-senary wh-full hover:bg-main-quaternary/50;

		.button-icon {
			@apply mr-2 w-4 stroke-[2.4px];
		}
	}

	&-container {
		@apply max-w-[unset];

		.a-modal {
			&__body {
				@apply px-8;
			}
		}
	}

	&-add {
		@apply flex flex-row gap-8;

		&--key {
			@apply inline-block;

			.formkit {
				&-input {
					@apply min-h-10 w-full cursor-pointer pr-1;
				}
			}
			.a-select {
				&-container {
					@apply cursor-pointer rounded-secondary hover:bg-main-tertiary;
				}
			}
		}
		&--query {
			@apply mr-4;

			.formkit {
				&-inner {
					@apply flex items-center justify-center rounded-md border border-main-quaternary transition focus-within:border-accent-primary;
				}
				&-input {
					@apply min-h-11 px-4 font-reading text-main-senary placeholder-main-quinary/70;
				}
				&-icon {
					@include apply-hover-bubble($duration: '0.1s');
					@apply flex cursor-pointer items-center justify-center wh-10;
				}
			}
		}

		&__card {
			@apply h-full min-w-[400px] flex-shrink-0;
		}

		&__search {
			&__inner {
				@apply flex gap-4;
			}
		}

		&__result {
			&__header {
				@apply text-2xl capitalize text-main-senary;
			}
		}
	}

	&__footer {
		@apply flex p-1;
	}
}
</style>
