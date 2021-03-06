using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RandalsVideoStore.API.Infra;
using RandalsVideoStore.API.Ports;

namespace RandalsVideoStore.API
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
            // This adds an "entrypoint" for our database. Notice that this also handles things lke connection pooling. The Configuration key can be found in
            // appsettings.json.
            services.AddDbContext<VideoStoreContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            // Add a dependency to the dependency container. This way, whenever you construct a class containing an
            // IDatabase dependency it will resolve that interface to an instance of the SqliteDatabase. This instance is managed by the 
            // container so it's lifecycle should be none of your concern.
            services.AddTransient<IDatabase, SqliteDatabase>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RandalsVideoStore.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RandalsVideoStore.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
