using AspNetCoreZhaoMvc.Domain;
using AspNetCoreZhaoMvc.Repository.BaseRepositorys;
using AspNetCoreZhaoMvc.Repository.DataRepository;
using AspNetCoreZhaoMvc.Repository.Students;
using AspNetCoreZhaoMvc.Service.BaseServices;
using AspNetCoreZhaoMvc.Service.Students;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var basePath=AppContext.BaseDirectory;
            #region ���ӿڵķ���ע��
            //var servicesDllFile = Path.Combine(basePath, "AspNetCoreZhaoMvc.Service.dll");
            //var repositoryDllFile = Path.Combine(basePath, "AspNetCoreZhaoMvc.Repository.dll");
            //if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
            //{
            //    throw new Exception("Repository.dll��Service.dll ��ʧ����Ϊ��Ŀ�����ˣ�������Ҫ��F6���룬��F5���У����� bin �ļ��У���������");
            //}
            //// AOP ���أ������Ҫ��ָ���Ĺ��ܣ�ֻ��Ҫ�� appsettigns.json ��Ӧ��Ӧ true ���С�
            //var cacheType = new List<Type>();
            //if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<RedisCacheAOP>();
            //    cacheType.Add(typeof(RedisCacheAOP));
            //}
            //// ��ȡ Service.dll ���򼯷��񣬲�ע��
            //var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            //builder.RegisterAssemblyTypes(assemblysServices)
            //          .AsImplementedInterfaces()
            //          .InstancePerDependency()
            //          .EnableInterfaceInterceptors()//����Autofac.Extras.DynamicProxy;
            //          .InterceptedBy(cacheType.ToArray());//����������������б�����ע�ᡣ

            //// ��ȡ Repository.dll ���򼯷��񣬲�ע��
            //var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            //builder.RegisterAssemblyTypes(assemblysRepository)
            //       .AsImplementedInterfaces()
            //       .InstancePerDependency();

            #endregion
            #region û�нӿڵĵ����� class ע��

            //ֻ��ע������е��鷽�����ұ�����public
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Love)))
            //    .EnableClassInterceptors()
            //    .InterceptedBy(cacheType.ToArray());

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else
            {
                app.UseExceptionHandler();
            }
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
