import type {
	RouteMeta,
	RouteRecordRaw,
	RouteRecordRedirectOption,
} from 'vue-router';

export type GetRouteArgs = {
	name?: string;
	path?: string;
	meta?: RouteMeta;
	children?: RouteRecordRaw[];
	disableComp?: boolean;
	redirect?: RouteRecordRedirectOption;
};

export class RouterUtil {
	constructor(private path: string, private roles: string[]) {}

	getRoute(args: GetRouteArgs): RouteRecordRaw {
		const route: RouteRecordRaw = {
			name: args.name,
			path: `${args.path ?? args.name + 's'}`,
			meta: { requiresSession: true },
			children: args.children,
			component: () =>
				!args.disableComp
					? import(
							`../views/${
								this.path
							}/${args.name?.toPascalCase()}Page.vue`
					  )
					: undefined,
		};

		route.redirect = args.redirect;

		return route;
	}

	mapRoutes(r: GetRouteArgs[]): RouteRecordRaw[] {
		return r.map((args) => this.getRoute(args));
	}
}
