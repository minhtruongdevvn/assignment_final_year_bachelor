import { type UserManagerSettings, UserManager } from 'oidc-client-ts';

let userManager: UserManager;

const idsUrl = import.meta.env.VITE_IDS_URL;
const clientId = import.meta.env.VITE_IDS_CLIENT_ID;
const appUrl = window.location.origin;
const callbackUrl = appUrl + '/callback';
const silentRenewUrl = appUrl + '/silent-renew';
const scopes = [
	import.meta.env.VITE_IDS_ATM_SCOPES,
	'offline_access',
	'openid',
].join(' ');

const settings: UserManagerSettings = {
	authority: idsUrl,
	client_id: clientId,
	response_type: 'code',
	response_mode: 'query',
	automaticSilentRenew: true,
	filterProtocolClaims: true,
	revokeTokensOnSignout: true,
	post_logout_redirect_uri: appUrl,
	silent_redirect_uri: silentRenewUrl,
	redirect_uri: callbackUrl,
	scope: scopes,
};

export const authAPIs = () => (userManager ??= new UserManager(settings));
