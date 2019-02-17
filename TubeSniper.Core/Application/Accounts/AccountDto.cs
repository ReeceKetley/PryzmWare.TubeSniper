﻿using System;

namespace TubeSniper.Core.Application.Accounts
{
	public class AccountDto
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string RecoveryEmail { get; set; }
	}
}