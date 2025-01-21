﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MockLawFirm.Tests
{
	public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger, UrlEncoder encoder)
			: base(options, logger, encoder)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var claims = new[] { new Claim(ClaimTypes.Name, "Test Admin User"), new Claim(ClaimTypes.Role, "Admin") };
			var identity = new ClaimsIdentity(claims, "Admin");
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, "Bearer");

			var result = AuthenticateResult.Success(ticket);

			return Task.FromResult(result);
		}
	}
}
