import { generateClasses } from '@formkit/themes';

const textStyles = { outer: 'fk-text' };

const boxStyles = {
	fieldset: 'fk-box-fieldset',
	legend: 'fk-box-legend',
	wrapper: 'fk-box-wrapper',
	help: 'fk-box-help',
	input: 'fk-box-input form-check-input',
	label: 'fk-box-label',
};

export default generateClasses({
	button: { outer: 'fk-button' },
	date: textStyles,
	'datetime-local': textStyles,
	checkbox: boxStyles,
	email: textStyles,
	file: {
		label: 'block mb-1 font-bold text-sm',
		inner: ' cursor-pointer',
		input: 'text-gray-600 text-sm mb-1 file:mr-4 file:py-2 file:px-4 file:rounded-full file:border-0 file:text-sm file:bg-blue-500 file:text-white hover:file:bg-blue-600',
		noFiles: 'block text-gray-800 text-sm mb-1',
		fileItem: 'block flex text-gray-800 text-sm mb-1',
		fileRemove: 'ml-auto text-blue-500 text-sm',
	},
	month: textStyles,
	number: textStyles,
	password: textStyles,
	radio: {
		...boxStyles,
		input: boxStyles.input.replace('rounded-sm', 'rounded-full'),
	},
	range: {
		inner: '',
		input: 'form-range appearance-none w-full h-2 p-0 bg-gray-200 rounded-full focus:outline-none focus:ring-0 focus:shadow-none',
	},
	search: textStyles,
	select: textStyles,
	submit: { outer: 'fk-submit' },
	tel: textStyles,
	text: textStyles,
	textarea: {
		...textStyles,
		input: 'block w-full h-32 px-3 border-none text-base text-gray-700 placeholder-gray-400 focus:shadow-outline',
	},
	time: textStyles,
	url: textStyles,
	week: textStyles,
});
