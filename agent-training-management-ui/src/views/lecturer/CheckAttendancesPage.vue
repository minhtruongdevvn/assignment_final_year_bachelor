<template>
	<div class="attendance-checking">
		<div class="attendance-checking-container">
			<div class="attendance-checking-wrapper">
				<div class="attendance-checking__header"></div>

				<div class="attendance-checking__body">
					<div class="attendance-details"></div>

					<div class="checking-list">
						<div class="checking-list-container">
							<div class="checking-list__header">
								<div class="checking-list__header-container">
									<h1 class="checking-list__header__title">
										Checking Students' Attendance
									</h1>
								</div>
							</div>

							<div class="checking-list__body">
								<!-- <a-form-kit :schema="checkListSchema" /> -->
							</div>
						</div>
					</div>
				</div>

				<div class="attendance-checking__footter"></div>
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
import { computedWithControl } from '@vueuse/shared';
import { computed, ref } from 'vue';
import { useRoute } from 'vue-router';

import { scheduleAPIs } from '@/apis';

const api = scheduleAPIs();
const route = useRoute();

const schedule = computedWithControl(
	() => route.params.id,
	async () => {
		const id = route.params.id as string;
		const from = new Date(route.params.from as string);
		const to = new Date(route.params.to as string);

		await api.getAttendances({ from, to, id });
	}
);

// const checkListSchema
</script>

<style lang="scss">
.attendance-checking {
	&-container {
		@apply w-full bg-main-secondary;
	}

	&__header {
	}

	&__body {
		// @apply flex ;

		.checking-list {
			@apply w-full bg-main-tertiary/80;
		}
	}

	&__footer {
	}
}
</style>
