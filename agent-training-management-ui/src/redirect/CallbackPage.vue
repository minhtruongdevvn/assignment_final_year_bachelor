<script setup lang="ts">
import { onMounted } from 'vue';

import { authAPIs } from '@/apis';
import router from '@/router';

onMounted(
	async () =>
		await authAPIs()
			.signinRedirectCallback()
			.finally(() => router.replace('/'))
			.catch((e) => e)
);
</script>

<template />

<style lang="scss" scoped>
body {
	@apply bg-main-secondary;
}
</style>
