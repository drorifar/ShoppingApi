
using Microsoft.AspNetCore.StaticFiles;
using Serilog;
using Shopping.Context;
using Shopping.services;

namespace Shopping
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
#if DEBUG
                .WriteTo.Console()
#endif
                .WriteTo.File("logs/mylog.txt", rollingInterval: RollingInterval.Day) //write to file and create new file every day
                .CreateLogger(); //create the serilog configuration (we can do it with the appsetting)

            builder.Host.UseSerilog(); //add the serilog to injuction

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {                
                options.ReturnHttpNotAcceptable = true;
            })
                .AddNewtonsoftJson() // we add the newtonSoft json for workinf with the jsonPatch
                .AddXmlDataContractSerializerFormatters(); // we add the option to get the response as XML (for legacy programs)

            // we add a costume problem details that will add to the problem message (401,500...)
            builder.Services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = ctx =>
                {
                    ctx.ProblemDetails.Extensions.Add("Exemple", "Simon Was Here");
                    ctx.ProblemDetails.Extensions.Add("Machine", Environment.MachineName);
                };
            });

            //builder.Services.AddControllers(); // we add in the begining
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); //add a singletone service for fileType auto detect

#if DEBUG
            builder.Services.AddTransient<IMailService, LocalMailService>(); //add the mail service we created to the dependency injection flow  
#else
            builder.Services.AddTransient<IMailService, ProductionMailService>(); //add the production mail service we created to the dependency injection flow  
#endif

            builder.Services.AddDbContext<MyDBContext>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler(); //add exception handler when in production (show only what nececery to the client)  
            }

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
        }
    }
}
