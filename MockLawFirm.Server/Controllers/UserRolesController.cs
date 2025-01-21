using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MockLawFirm.Server.Repositories;
using MockLawFirm.Server.ViewModels;

namespace MockLawFirm.Server.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UserRolesController : ControllerBase
	{
		private readonly UserRoleRepository _userRolesRepository;
		public UserRolesController(UserRoleRepository userRoleRepository) 
		{
			_userRolesRepository = userRoleRepository;
		}

		[HttpPost("isAdminRole")]
		public IActionResult CheckIfUserIsAdmin(UserRoleViewModel urvm) 
		{
			var res = _userRolesRepository.CheckIfAdminByEmail(urvm).Result;
			return Ok(res);
		}
	}
}
