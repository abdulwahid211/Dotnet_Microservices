using Microsoft.EntityFrameworkCore;
using UserService.DBContext;
using UserService.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mySqlConnectionStr = builder.Configuration.GetConnectionString("WebApiDatabase");
builder.Services.AddDbContextPool<UserContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
