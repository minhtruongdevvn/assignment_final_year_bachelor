export interface IdentityResponseBase {
	user_name?: string;
	email?: string;
	password?: string;
	picture?: string;
	family_name?: string;
	given_name?: string;
	birth_date?: Date | string;
	code?: string;
}

export interface IdentityUpsertBase extends IdentityResponseBase {
	email?: string;
	family_name?: string;
	given_name?: string;
	birth_date?: Date | string;
}
