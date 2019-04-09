using System;
using System.IO;
using System.Reflection;
using Data.RepositoryInterfaces;
using Data.RepositoryInterfaces.Menu;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Persistence;
using Repositories.Repositories;
using Repositories.Repositories.Menu;
using Repositories.ServicesInterfaces;
using Services.Services;
using Serilog;
using Serilog.Core;
using Swashbuckle.AspNetCore.Swagger;


namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            const string connectionString = @"Server=.\SQLEXPRESS;Database=RestaurantDB;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies().EnableSensitiveDataLogging());

            //services
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<Repositories.ServicesInterfaces.IStoreService,StoreService>();

            //repositories

            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            services.AddScoped<ICommandRepository, CommandRepository>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuIngredientsRepository, MenuIngredientsRepository>();
            services.AddScoped<IMenuIngredientRepository, MenuIngredientRepository>();

            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IProviderIngredientsRepository, ProviderIngredientsRepository>();
            services.AddScoped<IProviderIngredientRepository, ProviderIngredientRepository>();

            services.AddScoped<IStoreRepository,StoreRepository>();



            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.File("Logs/.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();


            services
                 .AddMvc()
                 .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                 .AddJsonOptions(options =>
                 {
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Restaurant API", Version = "v1" });

//                 Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<Context>().Database.Migrate();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Restaurant API");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
