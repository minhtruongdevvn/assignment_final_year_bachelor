<template>
	<router-view />
	<modals-container />
</template>

<script setup lang="ts">
import { watch } from 'vue';
import { useRoute } from 'vue-router';

import { useUIStore } from './stores';

const route = useRoute();
const store = useUIStore();

const smallerThanLg = store.breakpoints.smaller('lg');

watch(
	() => [route.path, smallerThanLg.value],
	() => !store.isSidebarFolded && smallerThanLg.value && store.foldSidebar(),
	{ immediate: true, flush: 'post' }
);
</script>
