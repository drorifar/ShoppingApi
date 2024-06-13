
using Microsoft.AspNetCore.StaticFiles;

namespace Shopping
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {                
                options.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters(); // we add the optioms to get the response ad XML (for legacy programs)

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
        }
    }
}
