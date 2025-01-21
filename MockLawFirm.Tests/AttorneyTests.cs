using Microsoft.EntityFrameworkCore;
using MockLawFirm.Server.Entities;

namespace MockLawFirm.Tests
{
	public class AttorneyTests : TestWithSqlite
	{
		private const string FIRST_ATTORNEY_FULLNAME = "John Morgan";
		private const string FIRST_ATTORNEY_EMAIL_ADDRESS = "blah@blah.com";
		private const string FIRST_ATTORNEY_PHONE_NUMBER = "555-555-1234";

		private const string SECOND_ATTORNEY_FULLNAME = "David Richardson";
		private const string SECOND_ATTORNEY_EMAIL_ADDRESS = "d.richardson@dundermifflin.com";
		private const string SECOND_ATTORNEY_PHONE_NUMBER = "555-867-5309";

		public AttorneyTests()
		{
		}

		[TearDown]
		public void TearDown()
		{
			Context.ChangeTracker.Clear();
			Context.FakeAttorneys.RemoveRange(Context.FakeAttorneys);
			Context.SaveChanges();
		}

		[Test]
		public async Task DatabaseIsAvailableAndCanBeConnectedTo()
		{
			Assert.True(await Context.Database.CanConnectAsync());
		}

		[Test]
		public async Task RemoveAttorneyShouldRemoveAttorney()
		{
			Context.FakeAttorneys.Add(GetFirstAttorney());
			Context.FakeAttorneys.Add(GetSecondAttorney());
			Context.SaveChanges();

			var attorneysList = Context.FakeAttorneys.ToList();

			var secondAttorney = attorneysList[1];
			Context.FakeAttorneys.Remove(secondAttorney);
			Context.SaveChanges();

			var updatedAttorneysList = Context.FakeAttorneys.ToList();

			Assert.That(attorneysList.Count, Is.EqualTo(2));
			Assert.That(updatedAttorneysList.Count, Is.EqualTo(1));
		}

		[Test]
		public async Task AddAttorneyShouldAddAttorney()
		{
			var attorneysList = Context.FakeAttorneys.ToList();
			var newAttorney = GetFirstAttorney();

			var existingAttorney = await Context.FakeAttorneys.FirstOrDefaultAsync(a => a.AttorneyFullName.ToLower() == newAttorney.AttorneyFullName.ToLower());

			if (existingAttorney == null)
			{
				Context.FakeAttorneys.Add(newAttorney);
				Context.SaveChanges();
			}
			var updatedAttorneysList = Context.FakeAttorneys.ToList();
			Assert.That(attorneysList.Count, Is.EqualTo(0));
			Assert.That(updatedAttorneysList.Count, Is.EqualTo(1));
		}

		private Attorney GetFirstAttorney()
		{
			var firstAttorney = new Attorney();
			firstAttorney.EmailAddress = FIRST_ATTORNEY_EMAIL_ADDRESS;
			firstAttorney.PhoneNumber = FIRST_ATTORNEY_PHONE_NUMBER;
			firstAttorney.AttorneyFullName = FIRST_ATTORNEY_FULLNAME;
			firstAttorney.Id = 1;

			return firstAttorney;
		}

		private Attorney GetSecondAttorney()
		{
			var secondAttorney = new Attorney();
			secondAttorney.EmailAddress = SECOND_ATTORNEY_EMAIL_ADDRESS;
			secondAttorney.PhoneNumber = SECOND_ATTORNEY_PHONE_NUMBER;
			secondAttorney.AttorneyFullName = SECOND_ATTORNEY_FULLNAME;
			secondAttorney.Id = 2;

			return secondAttorney;
		}
	}
}
