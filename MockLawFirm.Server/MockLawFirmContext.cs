using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MockLawFirm.Server.Entities;
using System;
using System.Globalization;

namespace MockLawFirm.Server
{
	public class MockLawFirmContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		Guid adminUserId = new Guid("3e6e8842-9b66-432b-84dc-2294524f0063");
		Guid nonAdminUserId = new Guid("c1572a61-5638-417d-9d7f-a03f0b786e19");
		Guid secruityStampGuid = new Guid("d2beb0f4-3dd0-4373-9290-4e4c37fbe491");
		string adminRoleName = "Admin";
		string adminUsername = "admin@admin.com";
		string adminEmail = "admin@admin.com";
		string nonAdminRoleName = "Plain User";
		string nonAdminUsername = "user@user.com";
		string nonAdminEmail = "user@user.com";
		private readonly IConfiguration _config;

		public MockLawFirmContext(DbContextOptions<MockLawFirmContext> options)
		: base(options)
		{
		}

		public MockLawFirmContext() { }

		public DbSet<Attorney> Attorneys { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	=> optionsBuilder
		.UseSqlite("Data Source=mocklawfirm.db")
		.UseSeeding((MockLawFirmContext, _) =>
		{
			var userManager = MockLawFirmContext.GetService<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>();
			var roleManager = MockLawFirmContext.GetService<Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole>>();			

			var role = new ApplicationRole();
			role.Name = adminRoleName;
			role.NormalizedName = role.Name.ToUpper();
			roleManager.CreateAsync(role);

			var adminSeedUser = new ApplicationUser
			{
				Id = adminUserId,
				UserName = adminUsername,
				NormalizedUserName = adminUsername.ToUpper(),
				EmailConfirmed = true,
				Email = adminEmail,
				NormalizedEmail = adminEmail.ToUpper(),
				LockoutEnabled = false,
				ConcurrencyStamp = secruityStampGuid.ToString()				
			};

			userManager.AddPasswordAsync(adminSeedUser, "Password1!");
			var chkUser = userManager.CreateAsync(adminSeedUser);

			var nonAdminSeedUser = new ApplicationUser
			{
				Id = nonAdminUserId,
				UserName = nonAdminUsername,
				NormalizedUserName = nonAdminUsername.ToUpper(),
				EmailConfirmed = true,
				Email = nonAdminEmail,
				NormalizedEmail = nonAdminEmail.ToUpper(),
				LockoutEnabled = false,
				ConcurrencyStamp = secruityStampGuid.ToString()
			};

			userManager.AddPasswordAsync(nonAdminSeedUser, "Password1!");
			userManager.CreateAsync(nonAdminSeedUser);

			if (chkUser.Result.Succeeded)
			{
				var adminRole = roleManager.FindByNameAsync(adminRoleName);
				var adminUser = userManager.FindByEmailAsync(adminSeedUser.Email);
				if (adminRole != null && adminUser != null)
				{
					MockLawFirmContext.Set<IdentityUserRole<Guid>>().Add(new IdentityUserRole<Guid>()
					{
						RoleId = adminRole.Result.Id,
						UserId = adminUser.Result.Id
					});

					MockLawFirmContext.SaveChangesAsync();
				}
			}
		})
		.UseAsyncSeeding(async (MockLawFirmContext, _, cancellationToken) =>
		{
			var userManager = MockLawFirmContext.GetService<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>();
			var roleManager = MockLawFirmContext.GetService<Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole>>();

			var role = new ApplicationRole();
			role.Name = adminRoleName;
			role.NormalizedName = role.Name.ToUpper();
			await roleManager.CreateAsync(role);

			var adminSeedUser = new ApplicationUser
			{
				Id = adminUserId,
				UserName = adminUsername,
				NormalizedUserName = adminUsername.ToUpper(),
				EmailConfirmed = true,
				Email = adminEmail,
				NormalizedEmail = adminEmail.ToUpper(),
				LockoutEnabled = false,
				ConcurrencyStamp = secruityStampGuid.ToString()
			};

			await userManager.AddPasswordAsync(adminSeedUser, "Password1!");
			var chkUser = await userManager.CreateAsync(adminSeedUser);

			var nonAdminSeedUser = new ApplicationUser
			{
				Id = nonAdminUserId,
				UserName = nonAdminUsername,
				NormalizedUserName = nonAdminUsername.ToUpper(),
				EmailConfirmed = true,
				Email = nonAdminEmail,
				NormalizedEmail = nonAdminEmail.ToUpper(),
				LockoutEnabled = false,
				ConcurrencyStamp = secruityStampGuid.ToString()
			};

			await userManager.AddPasswordAsync(nonAdminSeedUser, "Password1!");
			await userManager.CreateAsync(nonAdminSeedUser);

			if (chkUser.Succeeded)
			{
				var adminRole = roleManager.FindByNameAsync(adminRoleName);
				var adminUser = userManager.FindByEmailAsync(adminSeedUser.Email);
				if (adminRole != null && adminUser != null)
				{
					MockLawFirmContext.Set<IdentityUserRole<string>>().Add(new IdentityUserRole<string>()
					{
						RoleId = adminRole.Result.Id.ToString(),
						UserId = adminUser.Result.Id.ToString()
					});
					await MockLawFirmContext.SaveChangesAsync();
				}
			}
		});
	}
}
