<template>
	<tooltip :="$props" :auto-hide="hideOnClickOutside" :triggers="triggers">
		<slot name="default" />

		<template #popper="{ isShown, hide }">
			<slot
				v-if="!content"
				name="tooltip"
				:is-shown="isShown"
				:hide="hide"
			/>

			<template v-else>
				{{ content }}
			</template>
		</template>
	</tooltip>
</template>

<script setup lang="ts">
import { Tooltip } from 'floating-vue';

import { tooltipProps } from '@/types';

defineEmits<{ (e: 'hide'): void; (e: 'show'): void; (e: 'resize'): void }>();
const props = defineProps(tooltipProps);

const triggers =
	props.triggers != 'none'
		? Array.isArray(props.triggers)
			? props.triggers
			: [props.triggers]
		: [];
</script>

<style lang="scss">
.v-popper--theme- {
	&base-dark {
		.v-popper__inner {
			@apply overflow-hidden rounded-lg bg-main-secondary px-3 py-[6px] font-title text-sm capitalize tracking-wide text-gray-400 brightness-[130%];
		}
	}

	&base-light {
		.v-popper__inner {
			@apply overflow-hidden rounded-xl border border-main-quaternary bg-main-quaternary px-3 py-1 font-reading text-sm capitalize text-gray-200;
		}
	}

	&base-dark,
	&base-light {
		.v-popper__arrow-outer,
		.v-popper__arrow-inner {
			@apply border-transparent;
		}
	}
}

.v-popper__popper {
	@apply focus-visible:outline-none;
}
</style>
