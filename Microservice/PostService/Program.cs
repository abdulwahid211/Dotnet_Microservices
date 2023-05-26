using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PostService.DBContext;
using PostService.Models;
using PostService.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
var mySqlConnectionStr = builder.Configuration.GetConnectionString("WebApiDatabase");
builder.Services.AddDbContextPool<PostContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddMassTransit(x =>
{
    // elided...
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("host.docker.internal", 5672, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("MessageQueue", (c) =>
        {
            c.Consumer<UserConsumer>();
        });

        cfg.ConfigureEndpoints(context);

    });
});

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


class UserConsumer : IConsumer<User>
{
    public async Task Consume(ConsumeContext<User> context)
    {
        var jsonMessage = JsonConvert.SerializeObject(context.Message);
        Console.WriteLine($"User message recived: {jsonMessage}");
    }
}