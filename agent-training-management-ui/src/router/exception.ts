import type { RouteRecordRaw } from 'vue-router';

const exceptionRoutes: RouteRecordRaw[] = [
	{
		name: 'unauthorized',
		path: '/unauthorized',
		component: () => import('@/views/exception/NotFoundPage.vue'),
	},
	{
		name: 'not-found',
		path: '/not-found',
		component: () => import('@/views/exception/NotFoundPage.vue'),
	},
];

export default exceptionRoutes;
