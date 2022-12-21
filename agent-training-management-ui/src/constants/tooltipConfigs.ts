export default {
	themes: {
		tooltip: { disabled: window.innerWidth <= 640 },
		'base-dark': {
			triggers: ['hover'],
			autoHide: true,
			placement: 'top',
			strategy: 'absolute',
			computeTransformOrigin: true,
			delay: { show: 100, hide: 0 },
		},
	},
};
