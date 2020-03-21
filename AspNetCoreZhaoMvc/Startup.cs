using AspNetCoreZhaoMvc.Auth;
using AspNetCoreZhaoMvc.Domain;
using AspNetCoreZhaoMvc.Filters;
using AspNetCoreZhaoMvc.Repository.BaseRepositorys;
using AspNetCoreZhaoMvc.Repository.DataRepository;
using AspNetCoreZhaoMvc.Repository.Students;
using AspNetCoreZhaoMvc.Service.BaseServices;
using AspNetCoreZhaoMvc.Service.Students;
using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace AspNetCoreZhaoMvc
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(opt => {
                //opt.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
                //opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                opt.UseMySQL(configuration.GetConnectionString("MysqlConnection"));
            });
            services.AddScoped<IBaseService<Student>, StudentService>();
            services.AddScoped<IBaseRepository<Student>, StudentRepository>();
            services.AddDbContext<IdentityDbContext>(opt=> {
                opt.UseMySQL(configuration.GetConnectionString("MysqlConnection"));
            });
            services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<IdentityDbContext>();
            //添加策略
            services.AddAuthorization(opt=> {
                opt.AddPolicy("仅用户",policy=>policy.RequireAuthenticatedUser());
                opt.AddPolicy("仅限管理员",policy=>policy.RequireRole("Administators"));
                opt.AddPolicy("编辑专辑",policy=>policy.RequireClaim("Edit Albums"));
                opt.AddPolicy("编辑专辑1", policy => policy.RequireClaim("Edit Albums","123","456"));
                opt.AddPolicy("编辑专辑2", policy => policy.RequireClaim("Edit Albums", new List<string> { "123", "456" }));
                //灵活配置匿名方法
                opt.AddPolicy("编辑专辑3",policy=>policy.RequireAssertion(context=> {
                    if (context.User.HasClaim(x => x.Type == "Edit Albums"))
                        return true;
                    return false;
                }));
                //另一种方法必须同时满足
                opt.AddPolicy("编辑专辑4",policy=>policy.AddRequirements(new EmailRequireMent("@126.com"),new QualifiedUserRequireMent()));
            });

            //注册Handler
            services.AddSingleton<IAuthorizationHandler, EmailHandler>();
            services.AddSingleton<IAuthorizationHandler, CanEditAlbumHandler>();
            services.AddSingleton<IAuthorizationHandler, AdministratorsHandler>();

            //CSRF XSS跨站伪造
            services.AddAntiforgery(opt=> {
                opt.FormFieldName = "AntiforgeryFieldname";
                opt.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
                opt.SuppressXFrameOptionsHeader = false;
            });
            //默认每个post都加入AutoValidateAntiforgeryToken
            services.AddMvc(opt=> {
                opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                //过滤器
               // opt.Filters.Add(new LogResourceFilter());
               // opt.Filters.Add(typeof(LogAsyncResourceFilter));
                opt.Filters.Add<LogResourceFilter>();
                opt.CacheProfiles.Add("Default", new CacheProfile
                {
                    Duration = 60
                });
                opt.CacheProfiles.Add("Never", new CacheProfile
                {
                    Location=ResponseCacheLocation.None,
                    NoStore=true
                });
            });
            //InMemory缓存
            services.AddMemoryCache();
            //redis缓存
            services.AddDistributedRedisCache(options=> {
                options.Configuration = "localhost";
                options.InstanceName = "redis-for-Album";
            });
            //对js css压缩
            services.AddResponseCompression();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var basePath=AppContext.BaseDirectory;
            #region 带接口的服务注入
            //var servicesDllFile = Path.Combine(basePath, "AspNetCoreZhaoMvc.Service.dll");
            //var repositoryDllFile = Path.Combine(basePath, "AspNetCoreZhaoMvc.Repository.dll");
            //if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
            //{
            //    throw new Exception("Repository.dll和Service.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。");
            //}
            //// AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
            //var cacheType = new List<Type>();
            //if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<RedisCacheAOP>();
            //    cacheType.Add(typeof(RedisCacheAOP));
            //}
            //// 获取 Service.dll 程序集服务，并注册
            //var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            //builder.RegisterAssemblyTypes(assemblysServices)
            //          .AsImplementedInterfaces()
            //          .InstancePerDependency()
            //          .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
            //          .InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。

            //// 获取 Repository.dll 程序集服务，并注册
            //var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            //builder.RegisterAssemblyTypes(assemblysRepository)
            //       .AsImplementedInterfaces()
            //       .InstancePerDependency();

            #endregion
            #region 没有接口的单独类 class 注入

            //只能注入该类中的虚方法，且必须是public
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Love)))
            //    .EnableClassInterceptors()
            //    .InterceptedBy(cacheType.ToArray());

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           // app.UseWelcomePage(); 欢迎页
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
            }else
            {
                app.UseExceptionHandler("Home/Error");
                app.UseHsts();
            }
            //压缩js css
            app.UseResponseCompression();//默认是gzip
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "Default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
