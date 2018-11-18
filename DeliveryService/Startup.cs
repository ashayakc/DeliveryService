using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Models;
using Swashbuckle.AspNetCore.Swagger;
using DeliveryService.Logic;
using DeliveryService.Repository;

namespace DeliveryService
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
			services.AddTransient<IOrderLogic, OrderLogic>();
			services.AddTransient<IOrderRule, DistanceRule>();
			services.AddTransient<IOrderRule, FloorRule>();
			services.AddTransient<IOrderRule, WeekendRule>();
			services.AddTransient<IOrderRule, NewCustomerRule>();
			services.AddTransient<IOrderRule, GoldenCustomerRule>();
			services.AddTransient<IOrderRule, CouponRule>();
			services.AddTransient<IPriceCalculator, PriceCalculator>();

			services.AddTransient<ICustomerRepo, CustomerRepo>();
			services.AddTransient<IOrderRepo, OrderRepo>();

			services.AddMvc();

			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1"					
				});
			});

			services.AddDbContext<DeliveryServiceContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DeliveryServiceContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders API V1");
			});
		}
    }
}
