import {
	type _ExtractActionsFromSetupStore,
	type _ExtractGettersFromSetupStore,
	type _ExtractStateFromSetupStore,
	type Pinia,
	type Store,
	type StoreGeneric,
	defineStore,
} from 'pinia';

type DefineStoreBaseArgs<Id, SS> = {
	id: Id;
	storeSetup: () => SS;
	persist?: boolean;
};

class DefineStoreFactory<Id extends string, SS> {
	#store?: Store<
		Id,
		_ExtractStateFromSetupStore<SS>,
		_ExtractGettersFromSetupStore<SS>,
		_ExtractActionsFromSetupStore<SS>
	>;

	createStore(id: Id, storeSetup: () => SS, persist = false) {
		const initStore = defineStore(id, storeSetup, { persist: persist });

		const useStore = (options?: { pinia?: Pinia; hot?: StoreGeneric }) =>
			(this.#store ??= initStore(options?.pinia, options?.hot));

		return useStore;
	}
}

export const defineStoreBase = <Id extends string, SS>({
	id,
	storeSetup,
	persist = false,
}: DefineStoreBaseArgs<Id, SS>) =>
	new DefineStoreFactory<Id, SS>().createStore(id, storeSetup, persist);
