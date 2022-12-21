export class Kt<U> {
	constructor(public value: Nullable<U>) {}

	let<R>(action: (it: U) => R): Kt<R> {
		return new Kt<R>(this.value ? action(this.value) : undefined);
	}

	also(action: (it: U) => void): Kt<U> {
		if (this.value) action(this.value);

		return this;
	}

	takeIf(predicate: (it: Nullable<U>) => boolean): Kt<U> {
		return new Kt<U>(predicate(this.value) ? this.value : undefined);
	}

	takeUnless(predicate: (it: Nullable<U>) => boolean): Kt<U> {
		return new Kt<U>(predicate(this.value) ? undefined : this.value);
	}
}
