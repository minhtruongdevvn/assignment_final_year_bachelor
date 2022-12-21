<template>
	<a-button class="tag-cell">
		<template v-if="Array.isArray(processedValue)">
			<span
				v-for="(tag, index) in processedValue"
				:key="index"
				class="tag-item"
			>
				{{ tag }}
			</span>

			<span v-if="hidesCount" class="tag-item tag-item__showmore">
				+{{ hidesCount }}
			</span>
		</template>
		<span v-else class="tag-item">
			{{ processedValue }}
		</span>
	</a-button>
</template>

<script setup lang="ts">
import { computed } from 'vue';

import { useUIStore } from '@/stores';

const props = defineProps<{ value: unknown }>();

const store = useUIStore();

const greaterThanXl = store.breakpoints.greater('xl');
const inBetweenMdXl = store.breakpoints.between('md', 'xl');
const processedValue = computed(() => {
	let tagsShown: number;

	greaterThanXl.value && (tagsShown = 3);
	inBetweenMdXl.value && (tagsShown = 1);

	return Array.isArray(props.value)
		? props.value.filter((value, index) => index < tagsShown && value)
		: props.value;
});
const hidesCount = computed(() =>
	Array.isArray(props.value)
		? props.value.length - (processedValue.value as []).length
		: 0
);
</script>

<style lang="scss" scoped>
.tag {
	&-cell {
		@apply inline-flex flex-wrap gap-1;
	}
	&-item {
		@apply items-center whitespace-nowrap rounded-lg bg-accent-tertiary/20 px-[6px] py-[2px] text-sm uppercase tracking-wider text-accent-tertiary;
	}
}
</style>
