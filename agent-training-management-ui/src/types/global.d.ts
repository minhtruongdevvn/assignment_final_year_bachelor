/* eslint-disable no-var */
export {};

declare global {
	type Nullable<T> = T | undefined | null;
	type GenericObject<T = unknown> = object & Record<string | number, T>;
	type Required<T> = { [P in keyof T]-?: T[P] };
	type AtLeast<T, K extends keyof T> = Partial<T> & Required<Pick<T, K>>;

	interface StorageAdapter {
		session: {
			set: <T>(key: string, value: T) => void;
			get: <TOut>(key: string) => TOut | undefined;
			clear: () => void;
			remove: (key: string) => void;
		};
		local: {
			set: <T>(key: string, value: T) => void;
			get: <TOut>(key: string) => TOut | undefined;
			clear: () => void;
			remove: (key: string) => void;
		};
	}

	interface ArrayConstructor {
		applyRange(start: number, end: number): number[];
		filterCount<T>(data: Array<T>, expression: (obj: T) => boolean): number;
		getDeepProperty<T = unknown>(
			value: unknown[],
			scheme: string,
			seperate?: string
		): T[];
		throwIfNull<T extends unknown[] | []>(
			value: T,
			message?: string
		): NonNullable<T>;
		ifNull<T extends unknown[] | []>(
			value: T,
			onError?: () => void
		): NonNullable<T>;
	}

	interface ObjectConstructor {
		getDeepProperty<T = unknown>(
			value: unknown,
			scheme: string,
			seperate?: string
		): T;
		throwIfNull<T>(value: T, message?: string): NonNullable<T>;
		ifNull<T>(value: T, onError?: () => void): NonNullable<T>;
	}

	interface String {
		toCamelCase: (replaceWhiteSpaces = false) => string;
		toPascalCase: (replaceWhiteSpaces = false) => string;
	}

	var storage: StorageAdapter;
}
