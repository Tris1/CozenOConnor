using MockLawFirm.Server.ViewModels;

namespace MockLawFirm.Server.Repositories
{
	public interface IUserRoleRepository
	{
		public Task<bool> CheckIfAdminByEmail(UserRoleViewModel urvm);
	}
}
