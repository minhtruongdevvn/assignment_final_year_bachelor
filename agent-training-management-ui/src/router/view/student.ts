import { type GetRouteArgs, RouterUtil } from '@/utils';

const routeUtil = new RouterUtil('student', ['agent']);
const routes: GetRouteArgs[] = [
	{ name: 'class', path: 'classes' },
	{ name: 'attendance' },
];

export default routeUtil.mapRoutes(routes);
