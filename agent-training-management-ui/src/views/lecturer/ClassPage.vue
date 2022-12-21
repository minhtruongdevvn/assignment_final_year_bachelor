<template>
	<data-table
		row-index
		title="class"
		class="class-table"
		:reload="reloaded"
		:response="sieveResponse"
		:schema="tableSchema"
		:modals="{
			entityName: 'class',
			dud: {
				moreActions: true,
				formkit: {
					schema: formSchema,
					data: formData,
					initialValue: formInitialValue,
				},
			},
		}"
		@shown:modal-dud="handleDudModalShown"
		@update:sieve="getClassSieve"
		@submit:create="handleCreate"
		@submit:delete="handleDelete"
		@submit:update="handleUpdate"
	>
		<template #more-actions="{ hideTooltip }">
			<div class="dud-more-actions">
				<a-button
					content="manage students"
					@click="showModal(hideTooltip)"
				/>
			</div>
		</template>

		<template #custom-modals>
			<a-modal
				v-model="addModalShown"
				close-button
				esc-to-close
				container-class="dud-more-actions-container"
				title="Manage Student In Class"
			>
				<div class="dud-more-actions-add">
					<data-table
						row-index
						title="students"
						:response="sieveStudentResponse"
						:schema="tableStudentSchema"
						:reload="addActionReloaded"
						@update:sieve="getAddActionSieve"
						@click:row="handleRowClickToAdd"
					/>
				</div>
			</a-modal>

			<a-modal
				v-model="updateStudentModalShown"
				close-button
				esc-to-close
				container-class="dud-more-actions__update-student-container"
				title="Update Student's Score"
			>
				<a-form-kit
					id="form-student-update"
					:schema="[
						{ $formkit: 'text', name: 'score', label: 'score' },
					]"
					@submit="handleSubmitUpdateStudent"
				/>
			</a-modal>
		</template>
	</data-table>
</template>

<script setup lang="ts">
import type { FormKitSchemaNode } from '@formkit/core';
import { reactive, ref } from 'vue';

import { classAPIs, skillAPIs, studentAPIs } from '@/apis';
import { defaultValues } from '@/constants';
import FilterOperation from '@/constants/filterOperations';
import { useUserStore } from '@/stores';
import type {
	ClassUpsert,
	Sieve,
	SkillReportUpdateVerifiedRequest,
	TablePropsSchema,
} from '@/types';
import { getValidDate } from '@/utils';

const apis = classAPIs();
const userStore = useUserStore();

const fakeSieve = ref(defaultValues.getSieve());
const sieveStudentResponse = ref(defaultValues.getSieveResponse());
const sieveResponse = ref(defaultValues.getSieveResponse());
const classId = ref<string>();
const formData = reactive({ skills: {} });
const formInitialValue = ref<ClassUpsert>({});
const reloaded = ref(false);
const addModalShown = ref(false);
const updateStudentModalShown = ref(false);
const addActionReloaded = ref(false);
const studentId = ref<string>();

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
const getAddActionSieve = async (sieve: Sieve) => {
	if (!classId.value) return;

	fakeSieve.value = sieve;
	sieveStudentResponse.value = await apis.getStudentsByClass(
		classId.value,
		sieve
	);
};

const showModal = async (hideTooltip: () => void) => {
	addModalShown.value = true;

	hideTooltip();
	await getAddActionSieve(fakeSieve.value);
};

const reload = () => (reloaded.value = true);

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

const handleCreate = async (data: unknown) => {
	await apis.add(data as ClassUpsert);
	reload();
};
const handleUpdate = async (data: unknown) => {
	const id = classId.value;

	if (id) {
		await apis.update(id, data as ClassUpsert);
		reload();
	}
};
const handleDelete = async () => {
	const id = classId.value;

	if (id) {
		await apis.delete(id);
		reload();
	}
};
const handleSubmitUpdateStudent = async (data: unknown) => {
	const d = data as SkillReportUpdateVerifiedRequest;

	const updateSkillReport = { score: d.score, class_id: classId.value };

	await studentAPIs().verifyStudentSkillReport(
		studentId.value!,
		updateSkillReport
	);

	updateStudentModalShown.value = false;
};

const handleRowClickToAdd = async (row: unknown) => {
	if (!Array.isArray(row)) return;

	const entityId = row.find((t) => t.key == 'id')?.value as string;

	studentId.value = entityId;
	updateStudentModalShown.value = true;
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
const tableStudentSchema: TablePropsSchema = [
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
