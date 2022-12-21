<template>
	<div class="app-container">
		<the-sidebar v-if="userSidebarSchema" :schema="userSidebarSchema" />

		<div class="content">
			<the-navbar />

			<div class="content-container">
				<router-view />
			</div>
		</div>
	</div>
</template>

<script setup lang="ts">
import { onMounted, shallowRef } from 'vue';
import { RouterView } from 'vue-router';

import TheNavbar from '@/components/navbar/TheNavbar.vue';
import TheSidebar from '@/components/sidebar/TheSidebar.vue';
import { useUserStore } from '@/stores';
import type { SidebarNavSchema } from '@/types';

const userStore = useUserStore();

const userSidebarSchema = shallowRef<SidebarNavSchema>();

onMounted(() => {
	userSidebarSchema.value = userStore.userSidebarSchema;
});
</script>

<style lang="scss">
.app {
	@apply wh-full;

	&-container {
		@apply h-full;
	}
}

.content {
	@include apply-scrollbar(#{&}, y);
	@apply h-full px-6;

	&-container {
		@apply relative mt-5;
	}
}
</style>
