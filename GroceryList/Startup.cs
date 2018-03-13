using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GroceryList.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GroceryList
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfigurationRoot Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Add(new ServiceDescriptor(typeof(IGroceryDatabaseContext), 
            //new GroceryDatabaseContext(Configuration.GetConnectionString("MySQLConnection"))));
            services.AddDbContext<GroceryListEntityFrameworkDbContxt>(
                options => options.UseSqlServer(Configuration.GetConnectionString("GroceryEFConnection"))
                );
            services.AddScoped<IGroceryDatabaseContext, GroceryListEntityFrameworkDbContxt>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Groceries}/{action=Index}/{id?}");
            });
        }
    }
}
