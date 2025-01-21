using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MockLawFirm.Server;
using MockLawFirm.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockLawFirm.Tests
{
	public abstract class TestWithSqlite : IDisposable
	{
		private const string ConnectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
		private readonly SqliteConnection _connection;

		protected readonly TestMockLawFirmContext Context;

		protected TestWithSqlite()
		{
			_connection = new SqliteConnection(ConnectionString);
			_connection.Open();
			var options = new DbContextOptionsBuilder<TestMockLawFirmContext>()
				.UseSqlite(_connection)
				.Options;
            Context = new TestMockLawFirmContext(options);
			Context.Database.EnsureCreated();
		}

		public void Dispose()
		{
			_connection.Close();
		}
	}
}

