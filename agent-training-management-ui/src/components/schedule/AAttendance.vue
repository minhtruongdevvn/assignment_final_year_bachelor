<template>
	<div class="attendance">
		<div class="attendance-container">
			<div class="attendance__header">
				<div class="attendance__header--title">attendances</div>

				<div class="attendance__header--actions">
					<div class="attendance__header--actions__date-ranges">
						<form-kit v-model="weekYear" type="week" />
					</div>
				</div>
			</div>

			<div class="attendance__body">
				<table class="attendance__body-container">
					<thead class="attendance__body__header">
						<tr tabindex="1">
							<th class="attendance__body__header--header" />
							<th
								v-for="(dow, dIndex) in daysOfWeek"
								:key="dIndex"
								class="attendance__body__header--item"
							>
								<span>
									{{ dow }}
									({{ getDateMonth(dateRange[dIndex]) }})
								</span>
							</th>
						</tr>
					</thead>

					<tbody class="attendance__body__content">
						<tr
							v-for="(
								slotAttends, paIndex
							) in processedAttendances"
							:key="paIndex"
							class="attendance__body__content__row"
						>
							<th class="attendance__body__content__row--header">
								<div
									class="attendance__body__content__row--header-container"
								>
									<div>Slot {{ paIndex + 1 }}</div>
									<span>
										{{ attendanceSlots[paIndex][1] }}
										-
										{{ attendanceSlots[paIndex][2] }}
									</span>
								</div>
							</th>

							<attendance-card
								v-for="(attend, aIndex) in slotAttends"
								:key="aIndex"
								:attend="attend"
								@click="handleCardClick(attend)"
							/>
						</tr>
					</tbody>
				</table>
			</div>

			<div class="attendance__footer"></div>
		</div>
	</div>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue';
import { weekNumber } from 'weeknumber';

import type { AttendanceResponse } from '@/types';
import { getDateRangesOfWeek } from '@/utils';

const emits = defineEmits<{
	(e: 'update:dateRanges', value: { from: Date; to: Date }): void;
	(e: 'cardClick', attend?: AttendanceResponse): void;
}>();
const props = defineProps<{
	attendances: AttendanceResponse[];
	initialDates?: { from: Date; to: Date };
}>();

const hourOnlySlots = ['08', '10', '12', '13', '15', '17'];
const daysOfWeek = [
	'Monday',
	'Tuesday',
	'Wednesday',
	'Thursday',
	'Friday',
	'Saturday',
];
const attendanceSlots = (() => {
	const res: (string | number)[][] = [];

	Array.applyRange(0, 4).forEach((index) => {
		if (index == 2) return;

		const firstHourTS = hourOnlySlots[index];
		const secondHourTS = hourOnlySlots[index + 1];
		const firstTSPM = `${firstHourTS}:00`;
		const secondTSPM = `${secondHourTS}:00`;

		res.push([index, firstTSPM, secondTSPM]);
	});

	return res;
})();

const weekYear = ref(getCurrentWeekInputType());

const processedAttendances = computed(() =>
	attendanceSlots.map((slot) =>
		daysOfWeek.map((dow) => {
			const attend = props.attendances.find((attd) => {
				const attdDOW = attd.slot!.day_of_week;
				const attdStartAt = attd.slot!.start_at?.substring(0, 5);

				return attdDOW == dow && attdStartAt == slot[1];
			});

			if (attend?.slot) {
				attend.slot.start_at = slot[1].toString();
				attend.slot.end_at = slot[2].toString();
			}

			return attend;
		})
	)
);
const datePoints = computed(() => {
	const week = Number(weekYear.value.substring(6, 8));
	const year = Number(weekYear.value.substring(0, 4));

	return getDateRangesOfWeek(week, year);
});
const dateRange = computed(() => {
	loadData();

	return Array.applyRange(0, 5).map((index) => {
		if (index == 0) return datePoints.value.from;

		const day = new Date(datePoints.value.from);

		day.setDate(datePoints.value.from.getDate() + index);

		return day;
	});
});

const handleCardClick = (attend?: AttendanceResponse) => {
	emits('cardClick', attend);
};

function getCurrentWeekInputType() {
	const now = new Date();
	const week = weekNumber(now);
	const weekStr = week.toString().length == 1 ? `0${week}` : week;

	return `${now.getFullYear()}-W${weekStr}`;
}
function loadData() {
	return emits('update:dateRanges', datePoints.value);
}
const getDateMonth = (d: Date) => `${d.getDate()}/${d.getMonth() + 1}`;
</script>

<style lang="scss">
.attendance {
	@apply flex w-full flex-col items-center justify-center gap-7 pb-[8%] pt-8 sm:px-6;

	&-container {
		@apply w-full table-fixed rounded-lg bg-main-secondary;
	}

	&__header {
		@apply flex justify-between border-b border-main-quaternary/60 p-4 wh-auto md:px-12 md:py-7;

		&--title {
			@apply text-base font-bold capitalize leading-normal text-neutral-800 focus:outline-none dark:text-gray-200 sm:text-lg md:text-xl lg:text-2xl;
		}
	}

	&__body {
		@include apply-scrollbar(#{&}, x);
		@apply pb-1;

		&__header {
			@apply sticky inset-0 box-content h-24 w-full capitalize focus:outline-none;

			&--item {
				@apply text-center text-main-senary;

				span {
					@apply text-center font-normal;
				}
			}

			&--header {
				@apply px-24;
			}
		}

		&__content {
			@apply w-full;

			&__row {
				@apply border-t border-main-tertiary;

				&--header {
					@apply sticky z-[1] h-32 px-8;

					&-container {
						@apply flex h-full flex-col items-start justify-center border-r border-main-tertiary font-normal text-main-senary;
					}
				}
			}
		}
	}

	&__footer {
		@apply flex items-center justify-between border-t border-main-quaternary/60 p-4 md:px-8;
	}
}
</style>
