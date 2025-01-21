using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MockLawFirm.Server.Repositories;
using MockLawFirm.Server.ViewModels;

namespace MockLawFirm.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AttorneyController : ControllerBase
	{
		private readonly AttorneyRepository _attorneyRepository;
		public AttorneyController(AttorneyRepository attorneyRepository)
		{
			_attorneyRepository = attorneyRepository;
		}

		[AllowAnonymous]
		[HttpGet("getAllAttorneys")]
		public async Task<ActionResult<List<AttorneyViewModel>>> GetAllAttorneys()
		{
			var res = _attorneyRepository.GetAllAttorneys().Result.ToList();
			return res;
		}

		[Authorize(Roles = "Admin")]
		[HttpPost("addAttorney")]
		public async Task<ActionResult<List<AttorneyViewModel>>> AddAttorney(AttorneyViewModel attorneyViewModel)
		{
			var res = _attorneyRepository.AddAttorney(attorneyViewModel).Result.ToList();
			return res;
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("removeAttorney/{id}")]
		public async Task<ActionResult<List<AttorneyViewModel>>> RemoveAttorney(long id)
		{
			var res = _attorneyRepository.RemoveAttorney(id).Result.ToList();
			return res;
		}
	}
}
