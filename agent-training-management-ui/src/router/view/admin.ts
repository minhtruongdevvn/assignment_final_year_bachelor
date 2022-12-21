import { type GetRouteArgs, RouterUtil } from '@/utils';

const routeUtil = new RouterUtil('admin', ['operator']);

const routes: GetRouteArgs[] = [
	{ name: 'skill' },
	{ name: 'class', path: 'classes' },

	// users
	{ name: 'student' },
	{ name: 'operator' },
	{ name: 'lecturer' },
	{ name: 'external' },
];

export default routeUtil.mapRoutes(routes);
