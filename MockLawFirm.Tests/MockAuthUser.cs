using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MockLawFirm.Tests
{
	public class MockAuthUser
	{
		public List<Claim> Claims { get; private set; } = new();

		public MockAuthUser(params Claim[] claims)
			=> Claims = claims.ToList();
	}
}
