/** @type {import('tailwindcss').Config} */
const defaultTheme = require('tailwindcss/defaultTheme');
const { tailwind } = require('./src/utils/plugins');

module.exports = {
	important: true,
	darkMode: 'class',
	plugins: tailwind.plugins,
	content: ['./**/*.html', './src/**/*.{vue,js,ts,css,scss,sass}'],
	theme: {
		extend: {
			borderWidth: { 3: '3px', 5: '5px', 6: '6px', 7: '7px' },
			borderRadius: {
				primary: '16px',
				secondary: '8px',
				tertiary: '12px',
			},
			fontFamily: {
				sans: ['Open Sans', defaultTheme.fontFamily.sans],
				reading: 'Quicksand, sans-serif',
				title: 'Comfortaa, cursive',
				logo: 'Old Standard TT',
				code: 'Recursive, sans-serif',
			},
			screens: {
				'<2xl': { max: '1535px' },
				'<xl': { max: '1279px' },
				'<lg': { max: '1023px' },
				'<md': { max: '767px' },
				'<sm': { max: '639px' },
				'<xs': { max: '319px' },
				'@2xl': { min: '1536px' },
				'@xl': { min: '1280px', max: '1535px' },
				'@lg': { min: '1024px', max: '1279px' },
				'@md': { min: '768px', max: '1023px' },
				'@sm': { min: '640px', max: '767px' },
				'@xs': { min: '320px', max: '639px' },
			},
		},
	},
};
