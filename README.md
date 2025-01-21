# CozenOConnor

Set up instructions:
1. Clone to selected location
2. Open MockLawFirm.sln
3. Build Solution
4. Initialize the database via running the following commands from the Package Manager Console
	1. Add-Migration "init" or any string of text
	2. Update Database

Usage Instructions:

1. The database should have been seeded with 2 users (1 admin and 1 non-admin). You should be able to login in with the following credentials for each user to test various app functionality.
	a. Admin username: "admin@admin.com", Admin password: "Password1!"
	b. Non-admin username: "user@user.com", Non-admin password: "Password1!"

Running Tests:
The unit/integration tests live in the 'MockLawForm.Tests' project and can be run in the test explorer.