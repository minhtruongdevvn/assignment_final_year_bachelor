import vue from '@vitejs/plugin-vue';
import unplug from 'unplugin-vue-components/vite';
import { fileURLToPath, URL } from 'url';
import { defineConfig } from 'vite';

export default defineConfig({
	server: { port: 3001 },
	plugins: [vue(), unplug({ directives: false })],
	resolve: { alias: { '@': fileURLToPath(new URL('src', import.meta.url)) } },
	css: {
		preprocessorOptions: {
			scss: {
				additionalData: `
					@import "./src/assets/styles/partials/_utils.scss";
					@import "./src/assets/styles/partials/_mixins.scss";
					@import "./src/assets/styles/partials/_formkit.scss";
					@import "./src/assets/styles/partials/_modal.scss";
				`,
			},
		},
	},
});
