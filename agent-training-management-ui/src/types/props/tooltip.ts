import type { PropType } from 'vue';

import type { VuePropsWrapper } from '@/utils';

export type TooltipTriggers = 'hover' | 'click' | 'focus' | 'touch' | 'none';

export type TooltipPlacement =
	| 'top'
	| 'top-start'
	| 'top-end'
	| 'bottom'
	| 'bottom-start'
	| 'bottom-end'
	| 'right'
	| 'right-start'
	| 'right-end'
	| 'left'
	| 'left-start'
	| 'left-end';

export type TooltipProps = {
	content?: string;
	distance?: number;
	disabled?: boolean;
	shown?: boolean;
	hideOnClickOutside?: boolean;
	placement?: TooltipPlacement;
	triggers?: TooltipTriggers | TooltipTriggers[];
	theme?: string;
};

export const tooltipProps: VuePropsWrapper<TooltipProps> = {
	content: { type: String },
	distance: { type: Number },
	disabled: { type: Boolean },
	shown: { type: Boolean },
	hideOnClickOutside: { type: Boolean },
	placement: { type: String as PropType<TooltipPlacement> },
	theme: { type: String, default: 'base-dark' },
	triggers: {
		type: [Array<string>, String] as PropType<
			TooltipTriggers | TooltipTriggers[]
		>,
		default: 'hover',
	},
};
