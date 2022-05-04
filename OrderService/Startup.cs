using MassTransit;
using MassTransit.Definition;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderService.Consumers;
using OrderService.DatabaseContext;
using OrderService.Services;
using Shared.BusConfiguration;
using Shared.DbCOnfiguration;
using Shared.Messages.Commands;
using Shared.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService
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
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(cfg=>
            {
                cfg.AddRequestClient<IOrderInitiate>();

                cfg.AddConsumer<OrderStartConsumer>();
                cfg.AddConsumer<OrderCancelledConsumer>();

                //for state machine
                cfg.AddSagaStateMachine<OrderStateMachine, OrderState>()
                       .EntityFrameworkRepository(r =>
                       {
                           r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                           r.AddDbContext<DbContext, OrderStateDbContext>((provider, builder) =>
                           {
                               builder.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
                           });
                       });

                cfg.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                    cfg.Host(new Uri(BusConstants.RabbitMqUri),
                        h =>
                        {
                            h.Username(BusConstants.Username);
                            h.Password(BusConstants.Password);
                        }
                    );
                });
            });
            services.AddMassTransitHostedService();

            services.AddDbContext<OrderDbContext>(o =>
            o.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddScoped<IOrderDataAccess, OrderDataAccess>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderService v1"));
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
