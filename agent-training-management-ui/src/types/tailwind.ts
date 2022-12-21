import type { Config } from 'tailwindcss/types/config';

export type TailwindViewPorts = 'sm' | 'md' | 'lg' | 'xl' | '2xl';

export type TailwindThemeConfigs = <TDefaultValue = Config['theme']>(
	path?: string,
	defaultValue?: TDefaultValue
) => TDefaultValue;
