using Microsoft.AspNetCore.Identity;
using MockLawFirm.Server.Entities;
using MockLawFirm.Server.ViewModels;

namespace MockLawFirm.Server.Repositories
{
	public class UserRoleRepository : IUserRoleRepository
	{
		private UserManager<ApplicationUser> _userManager;
		private RoleManager<ApplicationRole> _roleManager;

		public UserRoleRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) 
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public UserRoleRepository() { }

		public async Task<bool> CheckIfAdminByEmail(UserRoleViewModel urvm) 
		{
			var user = await _userManager.FindByEmailAsync(urvm.Email);
			if (user == null) 
				return false;

			var roles = await _userManager.GetRolesAsync(user);
			if (roles == null) 
				return false;

			if (roles.Contains("Admin"))
				return true;

			return false;
		}
	}
}
