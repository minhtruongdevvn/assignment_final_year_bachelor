/** @typedef { import('tailwindcss/plugin') } plugin */
const plugin = require('tailwindcss/plugin');

module.exports = plugin(
	({ addUtilities, matchUtilities, theme, e }) => {
		// square
		addUtilities(
			Object.entries(theme('width')).map(([key, value]) => ({
				[`.${e(`wh-${key}`)}`]:
					key == 'screen'
						? { width: '100vw', height: '100vh' }
						: { width: `${value}`, height: `${value}` },
			}))
		);
		matchUtilities(
			{ wh: (value) => ({ width: value, height: value }) },
			{ values: theme('wh') }
		);

		// overflow
		addUtilities({
			'.overflow-x-overlay': { 'overflow-x': 'overlay' },
			'.overflow-y-overlay': { 'overflow-y': 'overlay' },
			'.overflow-overlay': {
				'overflow-x': 'overlay',
				'overflow-y': 'overlay',
			},
		});
	},
	{
		variants: { wh: ['responsive', 'hover'] },
		theme: {
			extend: {
				maxHeight: getTheme(),
				maxWidth: getTheme(),
				minHeight: getTheme(),
				minWidth: getTheme(),
				screens: { xs: { min: '320px' } },
			},
		},
	}
);

function getTheme(type = 'spacing') {
	return ({ theme }) => ({ ...theme(type) });
}
