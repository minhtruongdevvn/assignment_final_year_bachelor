import type { EntityBase } from '@/commons';

import type { ScheduleResponse } from './attendance';

export interface SlotResponse extends EntityBase {
	day_of_week?: string;
	start_at?: string | Date;
	end_at?: string | Date;
	schedules?: ScheduleResponse[];
}
