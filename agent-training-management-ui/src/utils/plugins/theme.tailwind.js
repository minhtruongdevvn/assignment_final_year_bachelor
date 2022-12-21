/** @typedef { import('tailwindcss-themer') } themer */
const themer = require('tailwindcss-themer');

module.exports = themer({
	defaultTheme: {
		extend: {
			colors: {
				divider: '#212131',
				main: {
					primary: '#f5f5f5',
					secondary: '#d4d4da',
					tertiary: '#9d9daa',
					quaternary: '#757585',
					quinary: '#474759',
					senary: '#212131',
				},
				accent: {
					primary: '#5C43FF',
					secondary: '#308BEC',
					tertiary: '#42E9BF',
					contrast: '#15151f',
					info: '#4296e9',
					error: '#eb2f3d',
					warning: '#ff9659',
					success: '#23cf87',
				},
			},
		},
	},
	themes: [
		{
			name: 'dark',
			extend: {
				colors: {
					divider: '#262630',
					main: {
						primary: '#111114',
						secondary: '#17171c',
						tertiary: '#20202A',
						quaternary: '#373745',
						quinary: '#55556a',
						senary: '#b4b4ce',
					},
					accent: {
						primary: '#6956e5',
						secondary: '#439eff',
						tertiary: '#5bffd6',
						contrast: '#d8d8ed',
						info: '#4296e9',
						error: '#eb2f3d',
						warning: '#f58e3d',
						success: '#23cf87',
					},
				},
			},
		},
	],
});
