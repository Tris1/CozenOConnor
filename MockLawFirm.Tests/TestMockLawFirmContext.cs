using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MockLawFirm.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockLawFirm.Tests
{
	public sealed class TestMockLawFirmContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		public TestMockLawFirmContext(DbContextOptions<TestMockLawFirmContext> contextOptions)
			: base(contextOptions)
		{
		}

		public TestMockLawFirmContext() { }

		public DbSet<Attorney> FakeAttorneys { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<TestMockLawFirmContext>(options => options.UseSqlite("Data Source=InMemorySample;Mode=Memory;Cache=Shared"));
		}
	}
}
