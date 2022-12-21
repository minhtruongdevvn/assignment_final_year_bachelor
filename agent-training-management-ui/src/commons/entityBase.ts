export interface EntityBase extends GenericObject {
	id?: number | string;
	created_by?: string;
	updated_by?: string;
	created_at?: Date | string;
	updated_at?: Date | string;
}
