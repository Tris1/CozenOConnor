using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MockLawFirm.Server;
using MockLawFirm.Server.Entities;
using MockLawFirm.Server.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Principal;

namespace MockLawFirm.Tests
{
	public class ApiTests
	{
		private WebApplicationFactory<Program> _factory;
		JsonContent attorneyViewModelJsonString => JsonContent.Create(GetAttorneyViewModel());
		JsonContent userRoleViewModelJsonString => JsonContent.Create(GetUserRoleViewModel());
		HttpClient authorizedClient => GetAuthorizedClient();


		[OneTimeSetUp]
		public async Task OneTimeSetUp()
		{
			// Replace connection string in DbContext
			_factory = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder =>
				{
					builder.ConfigureServices(services =>
					{
						services.AddDbContext<MockLawFirmContext>(options =>
							options.UseSqlite("Data Source=InMemorySample;Mode=Memory;Cache=Shared"));
					});
				});
		}

		[OneTimeTearDown]
		public async Task OneTimeTearDown()
		{
			_factory.Dispose();
		}

		[Test]
		public async Task Unauthorized_User_Adding_Attorney_Returns_401Unauthorized_Error()
		{
			var client = _factory.CreateClient();

			var response = await client.PostAsync("/api/Attorney/addAttorney", attorneyViewModelJsonString);
			Assert.That(response.StatusCode.ToString(), Is.EqualTo(HttpStatusCode.Unauthorized.ToString()));
		}

		[Test]
		public async Task Authorized_User_Adding_New_Attorney_Should_Return_200OK_Status_Code()
		{
			authorizedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer");

			// Act
			var response = await authorizedClient.PostAsync("/api/Attorney/addAttorney", attorneyViewModelJsonString);
			Assert.That(response.StatusCode.ToString(), Is.EqualTo(HttpStatusCode.OK.ToString()));
		}

		[Test]
		public async Task Unauthorized_User_Removing_Attorney_Returns_401Unauthorized_Error()
		{
			var client = _factory.CreateClient();

			var response = await client.DeleteAsync($"/api/Attorney/removeAttorney/{1}");
			Assert.That(response.StatusCode.ToString(), Is.EqualTo(HttpStatusCode.Unauthorized.ToString()));
		}

		[Test]
		public async Task Authorized_User_Removing_New_Attorney_Should_Return_200OK_Status_Code()
		{
			authorizedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer");

			// Act
			var response = await authorizedClient.DeleteAsync($"/api/Attorney/removeAttorney/{1}");
			Assert.That(response.StatusCode.ToString(), Is.EqualTo(HttpStatusCode.OK.ToString()));
		}

		[Test]
		public async Task Unauthorized_User_Checking_User_Admin_Role_Returns_401Unauthorized_Error()
		{
			var client = _factory.CreateClient();

			var response = await client.PostAsync($"/api/UserRoles/isAdminRole", userRoleViewModelJsonString);
			Assert.That(response.StatusCode.ToString(), Is.EqualTo(HttpStatusCode.Unauthorized.ToString()));
		}

		[Test]
		public async Task Authorized_User_Checking_User_Admin_Role_Returns_200Ok_StatusCode()
		{
			authorizedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer");

			// Act
			var response = await authorizedClient.PostAsync($"/api/UserRoles/isAdminRole", userRoleViewModelJsonString);
			Assert.That(response.StatusCode.ToString(), Is.EqualTo(HttpStatusCode.OK.ToString()));
		}

		private AttorneyViewModel GetAttorneyViewModel()
		{
			return new AttorneyViewModel()
			{
				AttorneyEmailAddress = "Hello",
				AttorneyName = "World",
				AttorneyPhoneNumber = "12345"
			};
		}

		private UserRoleViewModel GetUserRoleViewModel()
		{
			return new UserRoleViewModel()
			{
				Email = "testadmin@testadmin.com"
			};
		}

		private HttpClient GetAuthorizedClient() 
		{
			return _factory.WithWebHostBuilder(builder =>
			{
				builder.ConfigureTestServices(services =>
				{
					services.AddAuthentication(defaultScheme: "Bearer")
						.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
							"Bearer", options => { });
				});
			})
			.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false,
			});
		}
	}
}
