import type { Component, PropType } from 'vue';
import type { TablerIconComponent } from 'vue-tabler-icons';

import type { VuePropsWrapper } from '@/utils';

type PartialContentArgs = number | string;
type TotalContentArgs = {
	tag?: string | object;
	content: PartialContentArgs;
	disabled?: boolean;
	classes?: unknown;
};
type ButtonContentArgs = PartialContentArgs | TotalContentArgs;

export type IconArgs = {
	classes?: unknown;
	tag?: TablerIconComponent;
	disabled?: boolean;
};

type ButtonIconArgs = IconArgs | { left?: IconArgs; right?: IconArgs };

export type ButtonProps = {
	tag?: string | Component | object;
	content?: ButtonContentArgs;
	icon?: ButtonIconArgs;
	disabled?: boolean;
};
export const buttonProps: VuePropsWrapper<ButtonProps> = {
	tag: { type: [String, Object], default: () => 'button' },
	content: { type: [String, Number, Object] as PropType<ButtonContentArgs> },
	icon: { type: Object as PropType<ButtonIconArgs> },
	disabled: { type: Boolean },
};
