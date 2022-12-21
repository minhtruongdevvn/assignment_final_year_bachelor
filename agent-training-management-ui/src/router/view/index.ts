import type { RouteRecordRaw } from 'vue-router';

import admin from './admin';
import lecturer from './lecturer';
import student from './student';

export const homeHierarchyWrapper = (
	children?: RouteRecordRaw[],
	redirect?: string
): RouteRecordRaw => {
	const routes: RouteRecordRaw = {
		path: '/',
		name: 'main',
		redirect,
		meta: { requiresSession: true },
		component: () => import('@/views/MainPage.vue'),
		children: [],
	};

	if (children != null) routes.children = children;

	return routes;
};

export default {
	admin: homeHierarchyWrapper(admin, 'skills'),
	lecturer: homeHierarchyWrapper(lecturer, 'classes'),
	student: homeHierarchyWrapper(student, 'attendances'),
};
