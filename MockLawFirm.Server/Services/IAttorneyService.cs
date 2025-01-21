using MockLawFirm.Server.ViewModels;

namespace MockLawFirm.Server.Services
{
	public interface IAttorneyService
	{
		Task<IEnumerable<AttorneyViewModel>> GetAllAttorneys();
		public Task<IEnumerable<AttorneyViewModel>> AddAttorney(AttorneyViewModel avm);
	}
}
