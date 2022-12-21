<template>
	<a-attendance
		:attendances="attendances"
		@update:date-ranges="handleUpdateDateRanges"
		@card-click="handleCardClick"
	/>

	<a-modal
		v-model="attendDetailModalShown"
		esc-to-close
		close-button
		class="attend-detail-modal"
		title="attendance details"
	>
		<table v-if="proccessedDetails" class="attend-detail-modal__content">
			<tbody>
				<!-- skill -->
				<tr
					v-if="proccessedDetails.skill"
					class="attend-detail-modal__cell"
				>
					<th class="attend-detail-modal__cell--label">skill</th>
					<td class="attend-detail-modal__cell--data">
						{{ proccessedDetails.skill.name }}
						<span
							v-if="proccessedDetails.skill.category"
							class="uppercase"
						>
							({{ proccessedDetails.skill.category }})
						</span>
					</td>
				</tr>

				<!-- class name -->
				<tr
					v-if="proccessedDetails.class.name"
					class="attend-detail-modal__cell"
				>
					<th class="attend-detail-modal__cell--label">class</th>
					<td class="attend-detail-modal__cell--data">
						{{ proccessedDetails.class.name }}
					</td>
				</tr>

				<!-- learn times -->
				<tr
					v-if="proccessedDetails.learnTimes.startDate"
					class="attend-detail-modal__cell"
				>
					<th class="attend-detail-modal__cell--label">
						learn times
					</th>
					<td class="attend-detail-modal__cell--data">
						{{ proccessedDetails.learnTimes.startDate }}

						<template v-if="proccessedDetails.learnTimes.endDate">
							<span>to</span>
							{{ proccessedDetails.learnTimes.endDate }}
						</template>
					</td>
				</tr>

				<!-- lecturers -->
				<tr
					v-if="proccessedDetails.lecturers"
					class="attend-detail-modal__cell"
				>
					<th class="attend-detail-modal__cell--label">lecturers</th>
					<td class="attend-detail-modal__cell--data">
						<template
							v-for="(
								lect, lIndex
							) in proccessedDetails.lecturers"
							:key="lIndex"
						>
							<span v-if="lIndex != 0">,</span>
							{{ lect.fullname }}
							<span>({{ lect.email }})</span>
						</template>
					</td>
				</tr>

				<!-- slot -->
				<tr
					v-if="proccessedDetails.slot"
					class="attend-detail-modal__cell"
				>
					<th class="attend-detail-modal__cell--label">times</th>
					<td class="attend-detail-modal__cell--data">
						{{ proccessedDetails.slot.startAt }}
						<span>to</span>
						{{ proccessedDetails.slot.endAt }}

						<span>(slot: {{ proccessedDetails.slot.no }})</span>
					</td>
				</tr>

				<!-- attend date -->
				<tr
					v-if="proccessedDetails.attendDate"
					class="attend-detail-modal__cell"
				>
					<th class="attend-detail-modal__cell--label">
						attend date
					</th>
					<td class="attend-detail-modal__cell--data">
						{{ proccessedDetails.attendDate }}
					</td>
				</tr>

				<!-- place to attend -->
				<tr
					v-if="proccessedDetails.placement"
					class="attend-detail-modal__cell"
				>
					<th class="attend-detail-modal__cell--label">placement</th>
					<td class="attend-detail-modal__cell--data">
						{{ proccessedDetails.placement }}
					</td>
				</tr>

				<!-- attend status -->
				<tr class="attend-detail-modal__cell">
					<th class="attend-detail-modal__cell--label">
						attend status
					</th>
					<td class="attend-detail-modal__cell--data capitalize">
						{{ proccessedDetails.status }}
					</td>
				</tr>

				<!-- absence reason -->
				<tr
					v-if="proccessedDetails.absenceReasons"
					class="attend-detail-modal__cell"
				>
					<th class="attend-detail-modal__cell--label">
						absence reasons
					</th>
					<td class="attend-detail-modal__cell--data">
						{{ proccessedDetails.absenceReasons }}
					</td>
				</tr>
			</tbody>
		</table>
	</a-modal>
</template>

<script setup lang="ts">
import { computedEager } from '@vueuse/shared';
import { ref } from 'vue';

import { studentAPIs } from '@/apis';
import { useUserStore } from '@/stores';
import type {
	AttendanceRequest,
	AttendanceResponse,
	AttendancesRequest,
	SlotResponse,
} from '@/types';
import { getValidDate, Kt } from '@/utils';

const slotNumbers = ['08', 10, 13, 15].map((n) => `${n}:00:00`);
const api = studentAPIs();
const userStore = useUserStore();

const attendances = ref<AttendanceResponse[]>([]);
const attendDetailModalShown = ref(false);
const attendDetails = ref<AttendanceResponse>();
const isEmpty = ref(true);

const getUserId = () => userStore.profile?.sub;
const getAttendanceDetails = async (args: AttendanceRequest) => {
	attendDetails.value = await api.getAttendanceByDate(args);
};
const getAttendances = async (args: AttendancesRequest) => {
	await api
		.getAttendances(args)
		.then((res) => {
			if (!res?.data?.length) isEmpty.value = false;

			attendances.value = res.data;
		})
		.catch(() => (isEmpty.value = false));
};
const handleUpdateDateRanges = async (dr: { from: Date; to: Date }) => {
	await getAttendances({ ...dr, id: getUserId() });
};
const handleCardClick = async (attend?: AttendanceResponse) => {
	new Kt(getUserId())?.let(async (id) => {
		await getAttendanceDetails({
			student_id: id,
			schedule_id: attend?.schedule_id,
			attend_date: attend?.attend_date,
		}).then(() => (attendDetailModalShown.value = true));
	});
};

const proccessedDetails = computedEager(() => {
	const details = attendDetails.value;

	if (!details) return undefined;

	const slot = getSlotTime(details.slot);
	const lecturers = details.class?.lecturers?.map((lect) => ({
		fullname: `${lect.given_name} ${lect.family_name}`,
		email: lect.email,
	}));

	const attendDate = details.attend_date
		? getValidDate(details.attend_date)
		: undefined;

	const skill = {
		name: details.class?.skill.name,
		category: details.class?.skill.category?.name,
	};

	const learnTimes = (() => {
		const start = details.class?.start_date;
		const end = details.class?.end_date;

		return {
			startDate: start ? getValidDate(start) : undefined,
			endDate: end ? getValidDate(end) : undefined,
		};
	})();

	return {
		slot: { ...slot, no: getSlotNumber(details.slot) },
		absenceReasons: details.absence_reasons,
		status: getStatus(details.is_attended),
		class: { name: details.class?.name },
		placement: details.class?.placement,
		learnTimes,
		lecturers,
		attendDate,
		skill,
	};

	function getStatus(isAttended?: boolean) {
		return isAttended == null
			? 'future'
			: isAttended
			? 'attended'
			: 'absences';
	}

	function getSlotNumber(slot?: SlotResponse) {
		if (!slot) return undefined;

		const slotNumber = slot.start_at
			? slotNumbers.indexOf(slot.start_at) + 1
			: 'NaN';

		return slotNumber;
	}

	function getSlotTime(slot?: SlotResponse) {
		if (!slot) return undefined;

		const startAt = slot.start_at?.substring(0, 5);
		const endAt = slot.end_at?.substring(0, 5);

		return { startAt, endAt };
	}
});
</script>

<style lang="scss" scoped>
.attend-detail-modal {
	&__content {
		@apply table-fixed;
	}

	&__cell {
		&--label {
			@apply justify-start py-3 pr-10 text-left align-top font-reading font-normal capitalize leading-8 text-main-senary/80;
		}

		&--data {
			@apply max-w-[500px] break-words py-3 font-reading font-semibold leading-8 tracking-wider text-main-senary;

			span {
				@apply font-normal text-main-senary/80;
			}
		}
	}
}
</style>
