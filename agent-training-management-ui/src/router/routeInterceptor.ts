import jwt_decode from 'jwt-decode';
import type { RouteRecordRaw } from 'vue-router';

import { authAPIs } from '@/apis';
import router from '@/router';
import roleRoutesHierarchy from '@/router/view';
import { useUserStore } from '@/stores';
import type { Profile } from '@/types/entities/Profile';
import { Kt } from '@/utils';

const api = authAPIs();

/** Check if the route need to be authenticated */
router.beforeEach(async (to) => {
	if (!to.meta.requiresSession) return true;

	const user = await api.getUser().then(async (u) => {
		if (!u) {
			await api.signinRedirect();
			throw new Error();
		}

		return u;
	});

	await api.querySessionStatus().catch(async () => {
		await api.revokeTokens(['access_token']);
		await api.signoutRedirect();
	});

	const userProfile = jwt_decode(user.access_token) as Profile;
	const userRole = userProfile.role?.toLowerCase();
	const roleRoutesAddable = !getMainRoute()?.children.length;

	if (roleRoutesAddable) {
		const userStore = useUserStore();
		const routesPerRole: GenericObject<RouteRecordRaw> = {
			operator: roleRoutesHierarchy.admin,
			lecturer: roleRoutesHierarchy.lecturer,
			agent: roleRoutesHierarchy.student,
		};
		let redirect: Nullable<string> = to.path;

		Object.keys(routesPerRole).forEach((key) => {
			const role = new Kt(userRole)?.takeIf((it) => it == key).value;

			if (!role) return;

			const routes = routesPerRole[role];

			router.addRoute(routes);

			const pathValidated = router
				.getRoutes()
				.some((cr) => cr.path == redirect);

			if (to.path == '/') {
				redirect = routes.redirect?.toString();
			} else if (!pathValidated) {
				redirect = '/not-found';
			}
		});

		userStore.profile = {
			sub: userProfile.sub,
			email: userProfile.email,
			role: userProfile.role,
		};

		return redirect;
	}

	return true;
});

function getMainRoute() {
	return router.getRoutes().find((r) => r.name == 'main');
}
