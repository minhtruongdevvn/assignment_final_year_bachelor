import 'vue-router';

declare module '*.vue' {
	import Vue from 'vue';
	export default Vue;
}

declare module 'vue-router' {
	interface RouteMeta {
		requiresSession?: boolean;
	}
}
