using MockLawFirm.Server.Repositories;
using MockLawFirm.Server.ViewModels;

namespace MockLawFirm.Server.Services
{
	public class AttorneyService : IAttorneyService
	{
		private readonly IAttorneyRepository _repository;

		public AttorneyService( IAttorneyRepository repository) 
		{
			_repository = repository;
		}

		public async Task<IEnumerable<AttorneyViewModel>> AddAttorney(AttorneyViewModel avm)
		{
			return _repository.AddAttorney(avm).Result;
		}

		public async Task<IEnumerable<AttorneyViewModel>> GetAllAttorneys()
		{
			return await _repository.GetAllAttorneys();
		}

	}
}
