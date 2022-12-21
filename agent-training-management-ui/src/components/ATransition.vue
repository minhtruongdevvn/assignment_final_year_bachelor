<template>
	<transition v-bind="$attrs" :name="type ?? 'fade'">
		<slot />
	</transition>
</template>

<script setup lang="ts">
withDefaults(defineProps<{ duration?: string; type?: 'fade' | string }>(), {
	duration: '200ms',
	type: 'fade',
});
</script>

<style lang="scss" scoped>
.fade {
	&-enter-active,
	&-leave-active {
		@apply transition-opacity duration-[v-bind(duration)] ease-in-out;
	}
	&-enter-from,
	&-leave-to {
		@apply opacity-0;
	}
}
</style>
