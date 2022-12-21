import {
	SchoolIcon,
	StarsIcon,
	TypographyIcon,
	UsersIcon,
} from 'vue-tabler-icons';

import type { SidebarNavSchema } from '@/types';

const adminSchema: SidebarNavSchema = [
	{ to: '/skills', name: 'skill', tag: StarsIcon },
	{ to: '/classes', name: 'class', tag: SchoolIcon },
	{
		name: 'users',
		tag: UsersIcon,
		children: [
			{ to: '/lecturers', name: 'lecturers' },
			{ to: '/students', name: 'students' },
			{ to: '/operators', name: 'operators' },
			{ to: '/externals', name: 'externals' },
		],
	},
];

const lecturerSchema: SidebarNavSchema = [
	{ to: '/classes', name: 'class', tag: SchoolIcon },
];

const studentSchema: SidebarNavSchema = [
	{ to: '/attendances', name: 'attendance', tag: TypographyIcon },
];

export default {
	adminSchema,
	lecturerSchema,
	studentSchema,
};
