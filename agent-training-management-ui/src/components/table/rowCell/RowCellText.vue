<template>
	<div class="text-cell">
		{{ processedValue }}
	</div>
</template>

<script setup lang="ts">
import { computed } from 'vue';

import { getValidDate } from '@/utils';

const props = defineProps<{ value: string | Date }>();

const processedValue = computed(() => {
	if (typeof props.value != 'string') {
		return getValidDate(props.value);
	}

	return props.value;
});
</script>

<style lang="scss" scoped>
.text-cell {
	@apply whitespace-nowrap align-middle font-reading text-sm leading-none text-main-senary transition duration-100 focus:outline-none;
}
</style>
