<template>
	<component
		:is="tag"
		class="button"
		:type="$attrs.type ?? 'button'"
		:title="$attrs.title"
		:disabled="disabled"
		@click="handleClick"
	>
		<slot name="icon-left" />

		<component
			:is="mainIcon.tag"
			v-if="mainIcon"
			:class="mainIcon.classes"
			@click="handleIconClick"
		/>

		<component
			:is="iconLeft.tag"
			v-if="iconLeft"
			:class="iconLeft.classes"
			@click="handleIconClick ?? handleIconLeftClick"
		/>

		<a-transition>
			<span
				v-if="['string', 'number'].includes(typeof content)"
				class="button-content"
			>
				{{ content }}
			</span>
			<component
				:is="content.tag ?? 'span'"
				v-else-if="typeof content == 'object' && !content.disabled"
				:class="['button-content', content.classes]"
			>
				{{ content.content }}
			</component>
			<slot v-else />
		</a-transition>

		<slot name="icon-right" />

		<component
			:is="iconRight.tag"
			v-if="iconRight"
			:class="iconRight.classes"
			@click="handleIconClick ?? handleIconRightClick"
		/>
	</component>
</template>

<script setup lang="ts">
import { computed } from 'vue';
import { Flag2Icon } from 'vue-tabler-icons';

import { type IconArgs, buttonProps } from '@/types';

const emits = defineEmits<{
	(e: 'icon-click'): void;
	(e: 'icon-left-click'): void;
	(e: 'icon-right-click'): void;
	(e: 'click', d: Event): void;
}>();
const props = defineProps(buttonProps);

const mainIcon = computed(() =>
	props.icon && 'tag' in props.icon && !props.icon.disabled
		? validateIconProps(props.icon)
		: undefined
);
const iconLeft = computed(() =>
	props.icon && 'left' in props.icon && !props.icon.left?.disabled
		? validateIconProps(props.icon.left)
		: undefined
);
const iconRight = computed(() =>
	props.icon && 'right' in props.icon && !props.icon.right?.disabled
		? validateIconProps(props.icon.right)
		: undefined
);

function validateIconProps(args?: IconArgs): Nullable<IconArgs> {
	if (!args || args.disabled) return undefined;

	return {
		tag: args.tag ?? Flag2Icon,
		disabled: !!args.disabled,
		classes: ['button-icon', args.classes],
	};
}

const handleClick = (d: Event) => emits('click', d);
const handleIconClick = () => emits('icon-click');
const handleIconLeftClick = () => emits('icon-left-click');
const handleIconRightClick = () => emits('icon-right-click');
</script>

<style lang="scss">
.button {
	@apply flex items-center justify-center whitespace-nowrap rounded-lg text-center focus-visible:outline-none;

	&[disabled='true'] {
		@apply brightness-50;
	}
}
</style>
