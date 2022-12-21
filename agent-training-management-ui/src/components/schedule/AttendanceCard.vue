<template>
	<td class="attendance-item">
		<div class="attendance-item-wrapper">
			<div class="attendance-item-container">
				<template v-if="attend">
					<div
						class="attendance-item-container--sched"
						@click="handleClick"
					>
						<div class="attendance-item__schedule-info">
							<span class="attendance-item__schedule-info__item">
								{{ attend.class?.skill?.name }} at
								{{ attend.class?.placement }}
							</span>
						</div>

						<div class="attendance-item__slot-time">
							<span>
								{{ attend.slot?.start_at }}
							</span>
							-
							<span>
								{{ attend.slot?.end_at }}
							</span>
						</div>

						<div class="attendance-item__attended">
							<span>{{ getStatus(attend.is_attended) }}</span>
						</div>
					</div>
				</template>
			</div>
		</div>
	</td>
</template>

<script setup lang="ts">
import type { AttendanceResponse } from '@/types';

const emits = defineEmits<{
	(e: 'click', d: unknown): void;
}>();

defineProps<{ attend?: AttendanceResponse }>();

const getStatus = (isAttended?: boolean) => {
	return isAttended == null ? 'Future' : isAttended ? 'Attended' : 'Absences';
};
const handleClick = (d: unknown) => emits('click', d);
</script>

<style lang="scss">
.attendance-item {
	&-container {
		@apply m-1 w-48;

		&--sched {
			@apply flex cursor-pointer flex-col justify-between gap-2 rounded-primary bg-main-tertiary px-4 py-5 text-sm transition wh-full hover:bg-main-quaternary/50;

			&__slot-time {
				@apply flex gap-2;
			}
		}
	}
}
</style>
