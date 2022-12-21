import type { RouteRecordRaw } from 'vue-router';

const redirectRoutes: RouteRecordRaw[] = [
	{
		name: 'callback',
		path: '/callback',
		component: () => import('@/redirect/CallbackPage.vue'),
	},
	{
		name: 'silent renew',
		path: '/silent-renew',
		component: () => import('@/redirect/SilentRenewPage.vue'),
	},
];

export default redirectRoutes;
