
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shopping.Context;
using Shopping.Repositories;
using Shopping.services;

namespace Shopping
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
#if DEBUG
                .WriteTo.Console()
#endif
                .WriteTo.File("logs/mylog.txt", rollingInterval: RollingInterval.Day) //write to file and create new file every day
                .CreateLogger(); //create the serilog configuration (we can do it with the appsetting)

            builder.Host.UseSerilog(); //add the serilog to injuction

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                // options.ReturnHttpNotAcceptable = true; //remark cause it return an error in the swagger (need to change to application/json everytime)
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

            if (builder.Configuration["Database:Source"] == "MS-SQL")
            {
                builder.Services.AddDbContext<MyDBContext>(options =>
                    options.UseSqlServer(builder.Configuration["ConnectionString:MS-SQL"]));
            }
            else
            {
                builder.Services.AddDbContext<MyDBContext>(options =>
                options.UseSqlite(builder.Configuration["ConnectionString:Main"])); // add the DBContext to the injection flow + call to it constructor (base ctor) with the configuration
                //.UseSqlite("Data Source=MyShop.db"));
            }

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); //add the repository to the injection engine (scope)
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUserReposetory, UserReposetory>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // add the automap to the injection engine, get the assembly of the object for the reflection. here we send the current assembly

            builder.Services.AddAuthentication("Bearer").AddJwtBearer( //add Jwt Beare authentication 
                o =>
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //valid the issuer from the token
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    //valid the Audience from the token
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    //validate that the signature is valid
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Authentication:MyKey"]))
                });

            builder.Services.AddAuthorization(o =>
            {
                o.AddPolicy("IsShopAdmin", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("auth_level", "9"); // check the claims wanted key and value 
                });
            });

           

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyDBContext>(); //get the injection manualy 
                context.Database.Migrate();
            }

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

            app.UseAuthentication(); // we add this to run the authentication check that we add above

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
