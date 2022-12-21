<template>
	<a-button
		class="table-utils__action"
		:icon="{ tag: sieve.filters.length == 0 ? FilterOffIcon : FilterIcon }"
		content="filters"
		:onclick="toggleModal"
	/>

	<filter-modal
		v-if="isShown"
		:schemas="propsSchema!"
		:shown="isShown"
		:sieve="sieve!"
		@close="toggleModal"
		@on-filter="handleFilter"
	/>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
import { FilterIcon, FilterOffIcon } from 'vue-tabler-icons';

import { injectionKeys } from '@/constants';
import { requireInjection } from '@/utils';

const { propsSchema, sieve, updateSieve } = requireInjection(
	injectionKeys.TABLE
);

const isShown = ref(false);

const toggleModal = () => {
	isShown.value = !isShown.value;
};

const handleFilter = (value: string[]) => {
	toggleModal();
	updateSieve({ ...sieve.value, filters: value, page: 1, page_size: 5 });
};
</script>
