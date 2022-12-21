import '@/assets/styles/main.scss';
import '@/router/routeInterceptor';
import '@/utils/extensions';

import { defaultConfig, plugin as formkitPlugin } from '@formkit/vue';
import FloatingVue from 'floating-vue';
import { createPinia } from 'pinia';
import piniaPluginPersitance from 'pinia-plugin-persistedstate';
import { createApp } from 'vue';
import { vfmPlugin } from 'vue-final-modal';
import VueTablerIcons from 'vue-tabler-icons';

import { formkitVault, tooltipConfigs } from '@/constants';
import router from '@/router';

import App from './App.vue';

const app = createApp(App);

app.use(router)
	.use(vfmPlugin())
	.use(VueTablerIcons)
	.use(FloatingVue, tooltipConfigs)
	.use(formkitPlugin, defaultConfig(formkitVault.configs))
	.use(createPinia().use(piniaPluginPersitance));

app.mount('#app');
