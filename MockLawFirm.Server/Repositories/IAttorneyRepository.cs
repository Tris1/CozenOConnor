using MockLawFirm.Server.ViewModels;

namespace MockLawFirm.Server.Repositories
{
	public interface IAttorneyRepository
	{
		public Task<IEnumerable<AttorneyViewModel>> GetAllAttorneys();
		public Task<IEnumerable<AttorneyViewModel>> AddAttorney(AttorneyViewModel avm);
		public Task<IEnumerable<AttorneyViewModel>> RemoveAttorney(long id);
	}
}
