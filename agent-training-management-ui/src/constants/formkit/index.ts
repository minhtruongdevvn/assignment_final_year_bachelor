import type { DefaultConfigOptions } from '@formkit/vue';

import customInputs from './customInputs';

const formkitVault = {
	configs: { inputs: customInputs } as DefaultConfigOptions,
	dataAttrs: {
		values: { width: { fit: 'fit', big: 'big' } },
		names: {
			width: 'data-width-type', // enum
			lock: 'data-locked', // bool
		},
	},
};

export default formkitVault;
