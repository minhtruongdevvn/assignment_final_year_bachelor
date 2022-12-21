<template>
	<td
		class="row-cell"
		:class="{ 'cell-option__fit-content': options?.fitContent }"
	>
		<component
			:is="CellTypeTags[props.type!]"
			v-if="processedValue"
			:value="processedValue"
			:="options"
		/>
		<span v-else class="status-empty">—</span>
	</td>
</template>

<script setup lang="ts">
import { computed } from 'vue';

import { CellTypeTags, rowCellProps } from '@/types';

const props = defineProps(rowCellProps);

const processedValue = computed(() => {
	if (
		props.value == undefined ||
		(Array.isArray(props.value) && props.value.length == 0)
	) {
		return undefined;
	}

	if (props.options?.isDateTypeData) {
		return new Date(props.value as string | Date);
	}

	const deepProp = props.options?.displayProp;

	if (!deepProp) return props.value.toString();
	else if (Array.isArray(props.value))
		return props.value.length == 1
			? getDeepValue(props.value[0], deepProp, false)
			: getDeepValue(props.value, deepProp);

	return getDeepValue(props.value, deepProp, false);

	function getDeepValue(value: unknown, schema: string, isArray = true) {
		if (isArray) return Array.getDeepProperty(value as [], schema);

		const objValue = Object.getDeepProperty(value, schema);
		return objValue ? objValue : undefined;
	}
});
</script>

<style lang="scss">
.row-cell {
	@apply pl-12;
}

.cell-option {
	&__fit-content {
		@apply w-[1%] whitespace-nowrap;
	}
}

.status-empty {
	@apply pl-2 text-main-quinary/70;
}
</style>
