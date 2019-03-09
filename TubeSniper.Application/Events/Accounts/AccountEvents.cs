using System;

namespace TubeSniper.Application.Events.Accounts
{
	public static class AccountEvents
	{
		public static event EventHandler<AccountProfileCreated> AccountProfileCreated;
		public static event EventHandler<AccountProfileRemoved> AccountProfileRemoved;
		public static event EventHandler<AccountProfileUpdated> AccountProfileUpdated;

		public static void RaiseAccountProfileCreated(AccountProfileCreated e)
		{
			AccountProfileCreated?.Invoke(null, e);
		}

		public static void RaiseAccountProfileRemoved(AccountProfileRemoved e)
		{
			AccountProfileRemoved?.Invoke(null, e);
		}

		public static void RaiseAccountProfileUpdated(AccountProfileUpdated e)
		{
			AccountProfileUpdated?.Invoke(null, e);
		}
	}
}