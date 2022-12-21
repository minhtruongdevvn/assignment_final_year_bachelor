import { createInput } from '@formkit/vue';

import FormKitSelect from '@/components/formkit/FormKitSelect.vue';
import { formkitASelectProps } from '@/types';

export default {
	aselect: createInput(FormKitSelect, {
		props: formkitASelectProps,
	}),
};
