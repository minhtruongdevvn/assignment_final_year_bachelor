<template>
	<div class="user-profile">
		<div
			class="user-profile-container"
			:class="{
				'bg-main-secondary/60 transition hover:bg-main-secondary':
					!uiStore.isSidebarFolded,
			}"
		>
			<a-tooltip
				hide-on-click-outside
				:distance="20"
				:disabled="optionsShown"
				triggers="click"
				theme="user-profile"
				placement="left"
				@hide="showOptions"
				@show="hideOptions"
			>
				<!-- user -->
				<div class="user-profile__user">
					<div
						class="user-profile__user-container"
						:class="[!uiStore.isSidebarFolded ? 'px-5' : '']"
					>
						<div class="user-profile__user--img">
							<img :src="src" alt="user profile" />
						</div>

						<a-transition
							v-if="userStore.profile && !uiStore.isSidebarFolded"
						>
							<div class="user-profile__user--info">
								<div class="user-profile__user--info--name">
									{{ userStore.profile.email }}
								</div>
								<div class="user-profile__user--info--role">
									{{ userStore.profile.role }}
								</div>
							</div>
						</a-transition>
					</div>
				</div>

				<template #tooltip="{ hide }">
					<div class="user-profile__user--options">
						<a-button
							class="user-profile__user--options__actions user-profile__user--options__logout"
							:icon="{ tag: LogoutIcon }"
							content="logout"
							@click="showConfirmLogoutModal(hide)"
						/>
					</div>
				</template>
			</a-tooltip>

			<a-modal
				v-model="confirmLogoutModal"
				esc-to-close
				container-class=""
				class="user-profile__modal--logout"
			>
				<span class="user-profile__modal--logout__message">
					Are you sure to
					<span class="font-extrabold text-red-500">LOGOUT</span>
					?
				</span>

				<template #footer="{ close }">
					<div class="user-profile__modal--logout__footer">
						<a-button
							class="user-profile__modal--logout__actions user-profile__modal--logout__actions--cancel"
							content="cancel"
							@click="close"
						/>
						<a-button
							class="user-profile__modal--logout__actions user-profile__modal--logout__actions--logout"
							content="logout"
							:icon="{ tag: LogoutIcon }"
							@click="handleLogout(close)"
						/>
					</div>
				</template>
			</a-modal>
		</div>
	</div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { LogoutIcon } from 'vue-tabler-icons';

import { authAPIs } from '@/apis';
import UserSilhouette from '@/assets/user-silhouette.png';
import { useUIStore, useUserStore } from '@/stores';

defineProps<{ imgSrc?: string }>();

const src = UserSilhouette;
const userStore = useUserStore();
const uiStore = useUIStore();

const optionsShown = ref(false);
const confirmLogoutModal = ref(false);

const hideOptions = () => (optionsShown.value = false);
const showOptions = () => (optionsShown.value = true);
const showConfirmLogoutModal = (hideTooltip: () => void) => {
	hideTooltip();
	confirmLogoutModal.value = true;
};
const handleLogout = async (dismisModal: () => void) => {
	dismisModal();

	await authAPIs().revokeTokens(['access_token']);
	await authAPIs().signoutRedirect();
};
</script>

<style lang="scss">
.user-profile {
	&-container {
		@apply mx-[25px] rounded-primary;
	}

	&__user {
		&-container {
			@apply flex cursor-pointer items-center justify-start gap-5 rounded-primary py-3 transition;
		}

		&--info {
			&--role {
				@apply text-sm text-main-senary/80;
			}
			&--name {
				@apply text-lg text-main-senary;
			}
			&--role,
			&--name {
				@apply font-title;
			}
		}

		&--img {
			@apply flex-shrink-0 rounded-full border-4 border-transparent transition hover:border-accent-primary/30;

			img {
				@apply rounded-full object-cover wh-11;
			}
		}

		&--options {
			@apply flex w-auto cursor-default flex-col items-start rounded-tertiary border border-main-quaternary bg-main-primary/90 py-2 backdrop-blur backdrop-filter;

			&__actions {
				@apply w-full gap-3 py-2 pl-9 pr-12 capitalize hover:bg-main-secondary;
			}
		}
	}

	&__modal {
		&--logout {
			&__message {
				@apply text-2xl;
			}

			&__footer {
				@apply flex justify-end gap-8;
			}

			&__actions {
				@apply h-10 justify-start rounded-secondary py-2 px-6 text-sm capitalize transition-colors duration-100 disabled:brightness-50;

				&--cancel {
					@apply text-main-senary hover:enabled:bg-main-quaternary/40 disabled:brightness-50;
				}

				&--logout {
					@apply bg-accent-error/30 px-4 text-rose-200 hover:bg-accent-error/50;

					.button {
						&-icon {
							@apply wh-5;
						}
						&-content {
							@apply mx-2;
						}
					}
				}
			}
		}
	}
}

/* prettier-ignore */
.v-popper--theme-user-profile {
	@apply p-0;
	.v-popper__arrow-outer,
	.v-popper__arrow-inner { @apply border-transparent;}
	.v-popper__inner       { @apply border-transparent bg-transparent;}
}
</style>
