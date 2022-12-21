import { mount } from '@vue/test-utils';
import { describe, expect, it } from 'vitest';

import { AButton } from '../src/components';

describe('AButton', () => {
	it('renders properly', () => {
		const wrapper = mount(AButton, { props: { msg: 'Hello Vitest' } });
		expect(wrapper.text()).toContain('Hello Vitest');
	});
});
