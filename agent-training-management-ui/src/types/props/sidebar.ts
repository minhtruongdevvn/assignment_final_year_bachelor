import type { PropType } from 'vue';
import type { TablerIconComponent } from 'vue-tabler-icons';

import type { VuePropsWrapper } from '@/utils';

type NavLinkBase = {
	to: string;
	name: string;
	tag: TablerIconComponent;
};

export type CategoryNavLinkProps = Omit<NavLinkBase, 'to'> & {
	children: Omit<NavLinkBase, 'tag'>[];
};
export type NavLinkProps = AtLeast<
	NavLinkBase & CategoryNavLinkProps,
	'name' | 'tag'
>;

export type SidebarNavSchema = (NavLinkBase | CategoryNavLinkProps)[];

export const categoryNavLinkProps: VuePropsWrapper<CategoryNavLinkProps> = {
	name: { type: String, required: true },
	tag: { type: Object as PropType<TablerIconComponent>, required: true },
	children: {
		type: Array as PropType<Omit<NavLinkBase, 'tag'>[]>,
		required: true,
	},
};

export const navLinkProps: VuePropsWrapper<NavLinkProps> = {
	to: { type: String },
	name: { type: String, required: true },
	tag: { type: Object as PropType<TablerIconComponent>, required: true },
	children: { type: Array as PropType<Omit<NavLinkBase, 'tag'>[]> },
};
