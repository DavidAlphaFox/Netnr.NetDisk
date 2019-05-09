using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netnr.Data;

namespace Netnr.NetDisk
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            GlobalTo.Configuration = configuration;
            GlobalTo.HostingEnvironment = env;

            //无创建，有忽略
            using (var db = new ContextBase())
            {
                db.Database.EnsureCreated();
            }

            //初始化
            new Func.Support().Init();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvc(options =>
            {
                //注册全局错误过滤器
                options.Filters.Add(new FilterConfigs.ErrorActionFilter());

                //注册全局过滤器
                options.Filters.Add(new FilterConfigs.GlobalFilter());
            });

            //授权访问信息
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = "Netnr.NetDisk.Auth";
                options.LoginPath = new PathString("/services/bad");
                options.AccessDeniedPath = new PathString("/services/bad");
                options.ExpireTimeSpan = DateTime.Now.AddDays(10) - DateTime.Now;
            });

            //session
            services.AddSession();

            //配置上传文件大小限制（详细信息：FormOptions）
            services.Configure<FormOptions>(options =>
            {
                //100MB
                options.ValueLengthLimit = 1024 * 1024 * 100;
                options.MultipartBodyLengthLimit = 1024 * 1024 * 100;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMemoryCache memoryCache)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //授权访问
            app.UseAuthentication();

            //session
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //缓存
            Core.CacheTo.memoryCache = memoryCache;
        }
    }
}
