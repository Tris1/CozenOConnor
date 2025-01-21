﻿using Microsoft.AspNetCore.Identity;

namespace MockLawFirm.Server.Entities
{
	public class ApplicationRole : IdentityRole<Guid>
	{
		public Guid CreatedBy { get; set; }
		public Guid ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime ModifiedOn { get; set; }
		public bool IsDeleted { get; set; }
	}
}
