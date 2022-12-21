import {
	type RouteRecordRaw,
	createRouter,
	createWebHistory,
} from 'vue-router';

import exceptionRoutes from './exception';
import redirectRoutes from './redirect';

const baseUrl = import.meta.env.BASE_URL;
const initialRoute: RouteRecordRaw = {
	path: '/:catchAll(.*)',
	meta: { requiresSession: true },
	component: () => import('./PassThroughPage.vue'),
};
const routes = [redirectRoutes, exceptionRoutes, initialRoute].flat();

export default createRouter({
	history: createWebHistory(baseUrl),
	routes,
});
