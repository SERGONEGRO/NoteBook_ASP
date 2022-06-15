using Microsoft.EntityFrameworkCore;
using NoteBook_ASP.Data.Interfaces;
using NoteBook_ASP.Data.Repository;

namespace NoteBook_ASP.Data
{
    public class StartUp
    {

        private IConfigurationRoot _confstring;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="hostEnv"></param>
        public StartUp(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostEnv)
        {
            //записываем в переменную файл с строкой подключения, чтобы потом добавить его в сервисы
            _confstring = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }


        /// <summary>
        /// настройка сервисов
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //указываем, какой использовать sql-сервер, с помощью переменной, в которой хранятся настройки из файла 
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confstring.GetConnectionString("DefaultConnection")));

            /*---связываем интерфейс с классом, реализующим этот интерфейс---*/
            //для работы с БД используем repository
            services.AddTransient<IAllPersons, PersonRepository>();

            //Позволяет работать с сессиями
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //делает так, что для разных пользователей выдается разная корзина
            //services.AddScoped(sp => ShopCart.GetCart(sp));

            //используем MVC с отключенным роутингом, поскольку ругается (из-за устаревшей версии)
            services.AddMvc(options => options.EnableEndpointRouting = false);

            //добавляем использование кэша
            services.AddMemoryCache();
            //добавляем сессии
            services.AddSession();


        }


        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            //используем сессии
            app.UseSession();
            //app.UseMvcWithDefaultRoute(); устарело
            app.UseRouting();
            app.UseCors();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("default","{controller=Home}/{action=Index}");
            //});

            //добавляем маршруты
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "categoryFilter", template: "Products/{action}/{category?}",
                    defaults: new { Controller = "Products", action = "List" });
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                //подключаем AppDBContent, чтобы на основе него подключаться к БД
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                //заполняем базу, если она пустая
                DBObjects.Initial(content);
            }

        }
    }
}
