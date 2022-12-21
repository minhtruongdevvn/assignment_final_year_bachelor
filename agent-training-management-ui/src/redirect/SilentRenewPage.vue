<script lang="ts" setup>
import { onMounted } from 'vue';

import { authAPIs } from '@/apis';

onMounted(async () => await authAPIs().signinSilentCallback());
</script>

<template />

<style lang="scss" scoped>
body {
	@apply bg-main-secondary;
}
</style>
