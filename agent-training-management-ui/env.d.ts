/// <reference types="vite/client" />

declare module '*.vue' {
	import type { DefineComponent } from 'vue';
	const component: DefineComponent<
		Record<string, never>,
		Record<string, never>,
		Record<string, unknown>
	>;
	export default component;
}

interface ImportMetaEnv {
	readonly VITE_ATM_URL: string;
	readonly VITE_IDS_URL: string;
	readonly VITE_IDS_CLIENT_ID: string;
	readonly VITE_IDS_ATM_SCOPES: string;
	readonly VITE_DISABLE_AUTH: string;
}

interface ImportMeta {
	readonly env: ImportMetaEnv;
}
