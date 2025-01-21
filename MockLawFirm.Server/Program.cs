using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MockLawFirm.Server;
using MockLawFirm.Server.Entities;
using MockLawFirm.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddDbContext<MockLawFirmContext>(options =>
{
	options.UseSqlite(
		builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
	.AddRoles<ApplicationRole>()
	.AddEntityFrameworkStores<MockLawFirmContext>()
	.AddApiEndpoints();

builder.Services.AddScoped<AttorneyRepository>();
builder.Services.AddScoped<UserRoleRepository>();

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapIdentityApi<ApplicationUser>();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

public partial class Program { }
