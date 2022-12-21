<template>
	<a-tooltip
		hide-on-click-outside
		triggers="click"
		placement="bottom-end"
		theme="table-util"
	>
		<template #default>
			<a-button
				class="table-utils__action"
				:icon="{ tag: ArrowsSortIcon }"
				content="sorts"
			/>
		</template>

		<template #tooltip>
			<div class="table-utils__action-outer">
				<div class="table-utils__action-wrapper">
					<a-button
						v-for="(prop, index) in propsSchema?.filter(
							(ps) => ps.sortable
						)"
						:key="index"
						:content="prop.key"
						:icon="{ tag: getSortIcon(prop.key) }"
						@click="handleSort(prop.key)"
					/>
				</div>
			</div>
		</template>
	</a-tooltip>
</template>

<script setup lang="ts">
import {
	ArrowsSortIcon,
	SortAscendingIcon,
	SortDescendingIcon,
	XIcon,
} from 'vue-tabler-icons';

import { injectionKeys } from '@/constants';
import { requireInjection } from '@/utils';

const { propsSchema, sieve, updateSieve } = requireInjection(
	injectionKeys.TABLE
);

const getSortIcon = (key: string) => {
	const ascIcon = SortAscendingIcon;
	const descIcon = SortDescendingIcon;
	const nilIcon = XIcon;

	return !sieve.value.sorts.some((p) => p == key)
		? sieve.value.sorts.some((p) => p == `-${key}`)
			? descIcon
			: nilIcon
		: ascIcon;
};
const handleSort = (key: string) => {
	const descKey = `-${key}`;
	const sorts = sieve.value.sorts.some((k) => k == key)
		? sieve.value.sorts.map((k) => (k == key ? descKey : k))
		: sieve.value.sorts.some((k) => k == descKey)
		? sieve.value.sorts.filter((k) => k != descKey)
		: [...sieve.value.sorts, key];

	updateSieve({ ...sieve.value, sorts, page: 1 });
};
</script>
