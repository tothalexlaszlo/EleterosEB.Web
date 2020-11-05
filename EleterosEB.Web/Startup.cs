using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using EleterosEB.Bll;
using EleterosEB.Data;
using EleterosEB.Data.Repositories;
using EleterosEB.Data.Repositories.Intefaces;
using EleterosEB.Domain;
using EleterosEB.Web.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace EleterosEB.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EleterosEBContext>(opt =>
                opt.UseSqlServer(_configuration.GetConnectionString("EleterosEBConnex"))
            );

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<DoctorService>();

            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<RoomService>();

            services.AddTransient<IAppointmentTypeRepository, AppointmentTypeRepository>();
            services.AddTransient<AppointmentTypeService>();

            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<ClientService>();

            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<PatientService>();

            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<AppointmentService>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<CategoryService>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ProductService>();

            services.AddTransient<ISurgeryRoomAppointmentRepository, SurgeryRoomAppointmentRepository>();
            services.AddTransient<SurgeryRoomAppointmentService>();



            services.AddAutoMapper(Assembly.GetEntryAssembly());

            services.AddControllersWithViews();

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EleterosEBContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    // Error page
            //}
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseNodeModules();
            app.UseRouting();

            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", Action = "Index" });

            });

            context.Database.EnsureCreated();
        }
    }
}
