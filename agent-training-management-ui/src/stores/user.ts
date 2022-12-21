import { computed, ref } from 'vue';

import { defineStoreBase } from '@/commons';
import { sidebarSchema } from '@/constants';
import type { SidebarNavSchema } from '@/types';
import type { Profile } from '@/types/entities/Profile';

export const useUserStore = defineStoreBase({
	id: 'user',
	storeSetup() {
		const profile = ref<Profile>();

		const userSidebarSchema = computed(() => {
			let userSidebarSchema: SidebarNavSchema | undefined = undefined;

			switch (profile.value?.role) {
				case 'operator':
					userSidebarSchema = sidebarSchema.adminSchema;
					break;
				case 'agent':
					userSidebarSchema = sidebarSchema.studentSchema;
					break;
				case 'lecturer':
					userSidebarSchema = sidebarSchema.lecturerSchema;
					break;
			}

			return userSidebarSchema;
		});

		return { profile, userSidebarSchema };
	},
});
