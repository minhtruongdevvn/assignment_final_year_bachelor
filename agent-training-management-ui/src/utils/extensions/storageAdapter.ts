export {};

globalThis.storage = {
	session: {
		set: <T>(key: string, value: T) => {
			return sessionStorage.setItem(key, JSON.stringify(value));
		},
		get: <TOut>(key: string) => {
			const item = sessionStorage.getItem(key);
			return item ? (JSON.parse(item) as TOut) : undefined;
		},
		clear: () => sessionStorage.clear(),
		remove: (key: string) => sessionStorage.removeItem(key),
	},
	local: {
		set: <T>(key: string, value: T) => {
			return localStorage.setItem(key, JSON.stringify(value));
		},
		get: <TOut>(key: string) => {
			const item = localStorage.getItem(key);
			return item ? (JSON.parse(item) as TOut) : undefined;
		},
		clear: () => localStorage.clear(),
		remove: (key: string) => localStorage.removeItem(key),
	},
};
