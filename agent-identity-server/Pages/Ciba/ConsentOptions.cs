// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace AgentIdentityServer.Pages.Ciba;

public static class ConsentOptions
{
	public const bool EnableOfflineAccess = true;
	public const string OfflineAccessDisplayName = "Offline Access";

	public const string OfflineAccessDescription =
		"Access to your applications and resources, even when you are offline";

	public const string MustChooseOneErrorMessage = "You must pick at least one permission";
	public const string InvalidSelectionErrorMessage = "Invalid selection";
}
