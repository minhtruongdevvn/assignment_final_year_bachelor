import { type GetRouteArgs, RouterUtil } from '@/utils';

const routeUtil = new RouterUtil('lecturer', ['lecturer']);
const routes: GetRouteArgs[] = [{ name: 'class', path: 'classes' }];

export default routeUtil.mapRoutes(routes);
