using System.ComponentModel.DataAnnotations;

namespace MockLawFirm.Server.Entities
{
	public class Attorney
	{
		[Key]
		public long Id { get; set; }
		public string AttorneyFullName { get; set; }
		public string EmailAddress { get; set; }
		public string PhoneNumber { get; set; }
	}
}
