using Assignment.Helpers;
using Assignment.Interfaces;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment
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
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache(); //Đăng ký dịch vụ lưu cache
            services.AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(30); }); //Kéo dài 30 phút
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IMonAnService, MonAnService>();
            services.AddTransient<IUploadHelper, UploadHelper>();
            services.AddTransient<IMaHoaHelper, MaHoaHelper>();
            services.AddTransient<INguoiDungService, NguoiDungService>();
            services.AddTransient<IDonHangService, DonHangService>();
            services.AddTransient<IDonHangChiTietService, DonHangChiTietService>();
            services.AddTransient<IKhachHangService, KhachHangService>();
            services.AddAuthentication(o =>
                {
                    o.DefaultScheme = "Application";
                    o.DefaultSignInScheme = "External";
                })
                .AddCookie("Application")
                .AddCookie("External")
                .AddGoogle(o =>
                {
                    o.ClientId = "802902366528-ccsj32911ia395jprc3njk94udpbjuo7.apps.googleusercontent.com";
                    o.ClientSecret = "bhZc44_FwDuy33Z0_7qCGwoj";
                });

            services.AddRazorPages().AddRazorRuntimeCompilation();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
