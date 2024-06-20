using EmailQueue.Context;
using EmailQueue.Repository;
using EmailQueue.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MainContext>(o => //add db context for entity f.w
{
    o.UseSqlite(builder.Configuration["ConnectionString:SqliteMain"]);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //add auto mapper

builder.Services.AddScoped<IEmailMessageRepository, EmailMessageRepository>(); //add repositories

builder.Services.AddTransient<IMailService, MailTrapService>();

builder.Services.AddHostedService<EmailBackgroundService>();

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
