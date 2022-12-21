class FormKitASelectDenominator {
	value? = '' as string;
	placeholder? = '';
	inputDisplayAffixes? = { left: '', right: '' };
	options = {} as GenericObject<string> | string[];
	caseInsensitive = true;
	searchable = true;
	showIcon = true;
	disabled = true;
}
export type FormKitASelect = FormKitASelectDenominator;
export const formkitASelectProps = Object.keys(new FormKitASelectDenominator());
