using Microsoft.EntityFrameworkCore;
using MockLawFirm.Server.Entities;
using MockLawFirm.Server.ViewModels;

namespace MockLawFirm.Server.Repositories
{
	public class AttorneyRepository : IAttorneyRepository
	{
		private readonly MockLawFirmContext _context;

		public AttorneyRepository(MockLawFirmContext context)
		{
			_context = context;
		}

		public AttorneyRepository()
		{
		}

		public async Task<IEnumerable<AttorneyViewModel>> GetAllAttorneys()
		{
			var attorneys = await _context.Attorneys.ToListAsync();
			var attorneyViewModelList = new List<AttorneyViewModel>();
			foreach (var attorney in attorneys)
			{
				var newAttorney = new AttorneyViewModel();
				newAttorney.Id = attorney.Id;
				newAttorney.AttorneyEmailAddress = attorney.EmailAddress;
				newAttorney.AttorneyPhoneNumber = attorney.PhoneNumber;
				newAttorney.AttorneyName = attorney.AttorneyFullName;
				attorneyViewModelList.Add(newAttorney);
			}
			return attorneyViewModelList;
		}

		public async Task<IEnumerable<AttorneyViewModel>> AddAttorney(AttorneyViewModel avm)
		{
			var existingAttorney = await _context.Attorneys.FirstOrDefaultAsync(a => a.AttorneyFullName.ToLower() == avm.AttorneyName.ToLower());
			if (existingAttorney != null) { new AttorneyViewModel(); }

			var newAttorney = new Attorney()
			{
				AttorneyFullName = avm.AttorneyName,
				EmailAddress = avm.AttorneyEmailAddress,
				PhoneNumber = avm.AttorneyPhoneNumber
			};

			_context.AddAsync(newAttorney);
			_context.SaveChangesAsync();

			var attorneys = await _context.Attorneys.ToListAsync();
			var attorneyViewModelList = new List<AttorneyViewModel>();
			foreach (var attorney in attorneys)
			{
				var newAttorneyViewModel = new AttorneyViewModel();
				newAttorneyViewModel.Id = attorney.Id;
				newAttorneyViewModel.AttorneyEmailAddress = attorney.EmailAddress;
				newAttorneyViewModel.AttorneyPhoneNumber = attorney.PhoneNumber;
				newAttorneyViewModel.AttorneyName = attorney.AttorneyFullName;
				attorneyViewModelList.Add(newAttorneyViewModel);
			}
			return attorneyViewModelList;
		}

		public async Task<IEnumerable<AttorneyViewModel>> RemoveAttorney(long id)
		{
			var existingAttorney = await _context.Attorneys.FirstOrDefaultAsync(x => x.Id == id);
			if (existingAttorney != null)
			{
				_context.Attorneys.Remove(existingAttorney);
				_context.SaveChangesAsync();
			}

			var attorneys = await _context.Attorneys.ToListAsync();
			var attorneyViewModelList = new List<AttorneyViewModel>();
			foreach (var attorney in attorneys)
			{
				var newAttorneyViewModel = new AttorneyViewModel();
				newAttorneyViewModel.Id = attorney.Id;
				newAttorneyViewModel.AttorneyEmailAddress = attorney.EmailAddress;
				newAttorneyViewModel.AttorneyPhoneNumber = attorney.PhoneNumber;
				newAttorneyViewModel.AttorneyName = attorney.AttorneyFullName;
				attorneyViewModelList.Add(newAttorneyViewModel);
			}
			return attorneyViewModelList;
		}
	}
}
