<template>
	<vue-final-modal
		v-slot="{ params, close }"
		classes="a-modal"
		:content-class="['a-modal-container', containerClass]"
		:="$attrs"
	>
		<div v-if="title || $slots.header" class="a-modal__header">
			<slot v-if="$slots.header" name="header" />
			<template v-else>
				<span class="a-modal__header__title">{{ title }}</span>

				<slot name="header-extend" />

				<a-button
					v-if="closeButton"
					class="a-modal__header__close"
					:icon="{ tag: XIcon }"
					title="close"
					@click="close"
				/>
			</template>
		</div>

		<div v-if="$slots.default" class="a-modal__body">
			<slot :params="params" />
		</div>

		<div v-if="$slots.footer" class="a-modal__footer">
			<slot name="footer" :="{ close }" />
		</div>
	</vue-final-modal>
</template>

<script setup lang="ts">
import { XIcon } from 'vue-tabler-icons';

/** vue-final-modal emits api
 * clickOutside | beforeOpen | opened | beforeClose | closed | cancel
 */
defineProps({
	title: String,
	containerClass: { type: String, default: 'big' },
	closeButton: { type: Boolean, default: false },
});
</script>

<style lang="scss">
/* prettier-ignore */
@mixin _config {
	%bg  { @apply bg-main-secondary; }
	%bdr { @apply border-main-quaternary/70; }
}
@include _config;

.a-modal {
	@apply flex h-full items-center justify-center;
	@include apply-scrollbar(#{&}, y);

	&-container {
		@extend%bg;
		@apply my-4 mx-3 flex max-w-[220vh] flex-col rounded-primary md:mx-6;
	}

	/* 	prettier-ignore */
	&__body, &__footer, &__header { @apply px-8; }

	&__header {
		@extend%bdr;
		@include apply-scrollbar(#{&}, x, $auto-hide: false);
		@apply flex h-auto items-center justify-between gap-3 border-b py-8 pr-5;

		&__title {
			@apply font-title text-lg font-bold capitalize;
		}
		&__close {
			@include apply-hover-bubble($duration: '0.15s');
			@apply bg-transparent wh-10;

			.button-icon {
				@apply stroke-[3] text-main-quinary duration-150 wh-5;
			}
		}
	}

	&__body {
		@include apply-scrollbar(#{&}, y, $auto-hide: false);
		@apply max-h-[500px] px-12 pb-8 pt-6 sm:max-h-[600px] md:max-h-[800px];
	}

	&__footer {
		@extend%bdr;
		@include apply-scrollbar(#{&}, x, $auto-hide: false);
		@apply border-t py-6;
	}
}
</style>
