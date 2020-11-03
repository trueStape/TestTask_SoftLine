using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using TestTaskSoftline.BLL.Interfaces;
using TestTaskSoftline.BLL.Services;
using TestTaskSoftline.DAL;
using TestTaskSoftline.DAL.Interfaces;
using TestTaskSoftline.DAL.Repositories;
using TestTaskSoftLine.Options;

namespace TestTaskSoftLine
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
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "SoftLine Api", Version = "v1" });
            });

            //Connection to DataBase
            services.AddDbContext<DatabaseContext>
            (options => options.UseSqlServer(Configuration["ConnectionString:TestTaskDb"],
                b => b.MigrationsAssembly("TestTaskSoftLine")));

            
            services.AddRazorPages();

            services.AddControllers();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            //Dependency injection
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //Swagger
            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(o => { o.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });

            app.UseHttpsRedirection();
            

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new
                    List<string> { "index.html" }
            });

            app.UseStaticFiles();
        }
    }
}