import type { FormKitSchemaCondition, FormKitSchemaNode } from '@formkit/core';
import type { PropType } from 'vue';

import RowCellTag from '@/components/table/rowCell/RowCellTag.vue';
import RowCellText from '@/components/table/rowCell/RowCellText.vue';
import { appConfigs } from '@/constants';
import type { VuePropsWrapper } from '@/utils';

import type { SieveResponse } from '../entities';

export type TableCellOptions = {
	fitContent?: boolean;
	displayProp?: string;
	isDateTypeData?: boolean;
};

class CellTypeDenominator {
	text = RowCellText;
	tag = RowCellTag;
	image = RowCellTag;
	status = RowCellTag;
}
type ICellTypeDenominator = CellTypeDenominator;

export type CellTypes = keyof ICellTypeDenominator;
export const CellTypeTags = new CellTypeDenominator();

export type RowCellProps = {
	value?: unknown;
	type?: CellTypes;
	options?: TableCellOptions;
};
export const rowCellProps: VuePropsWrapper<RowCellProps> = {
	value: { type: null, required: true },
	type: { type: String as PropType<CellTypes>, default: 'text' },
	options: { type: Object as PropType<TableCellOptions> },
};

export type TablePropsSchema = {
	key: string;
	name?: string;
	hidden?: boolean;
	sortable?: boolean;
	filterable?: boolean;
	filterOperation?: string;
	options?: TableCellOptions;
	type?: CellTypes;
}[];

export type TableProps = {
	title: string;
	schema?: TablePropsSchema;
	response?: SieveResponse<GenericObject>;
	rowIndex?: boolean;
	widthFull?: boolean;
	modals?: ContextContextModalProps;
	reload?: boolean;
};
export const tableProps: VuePropsWrapper<TableProps> = {
	title: { type: String, required: true },
	schema: { type: Object as PropType<TablePropsSchema> },
	response: { type: Object as PropType<SieveResponse<GenericObject>> },
	widthFull: { type: Boolean, default: appConfigs.table.pagination.withFull },
	rowIndex: { type: Boolean },
	modals: { type: Object as PropType<ContextContextModalProps> },
	reload: { type: Boolean, default: false },
};

type ContextModalFormKitProps = {
	id?: string;
	initialValue?: GenericObject;
	data?: GenericObject;
	schema: FormKitSchemaCondition | FormKitSchemaNode[];
};
type ContextModalProps = {
	entityName: string;
	title?: string;
	shown: boolean;
	formkit: ContextModalFormKitProps;
};
export const createActionModalProps: VuePropsWrapper<ContextModalProps> = {
	entityName: { type: String, required: true },
	title: { type: String },
	shown: { type: Boolean, required: true },
	formkit: {
		type: Object as PropType<ContextModalFormKitProps>,
		required: true,
	},
};

type DudActionModalProps = ContextModalProps & {
	editable?: boolean;
	deletable?: boolean;
	updateFormkit?: ContextModalFormKitProps;
	moreActions?: boolean;
};
export const dudActionModalProps: VuePropsWrapper<DudActionModalProps> = {
	...createActionModalProps,
	editable: { type: Boolean },
	deletable: { type: Boolean },
	moreActions: { type: Boolean, default: false },
	updateFormkit: { type: Object as PropType<ContextModalFormKitProps> },
};
type ContextContextModalProps = Pick<ContextModalProps, 'entityName'> & {
	entityName: string;
	creation?: Omit<ContextModalProps, 'entityName' | 'shown'>;
	dud?: Omit<DudActionModalProps, 'entityName' | 'shown'>;
};
