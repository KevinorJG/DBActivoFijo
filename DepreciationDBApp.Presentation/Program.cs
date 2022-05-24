using DepreciationDBApp.Applications.Interfaces;
using DepreciationDBApp.Applications.Services;
using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using DepreciationDBApp.Forms;
using DepreciationDBApp.Infrastructure.Repositories;
using DepreciationDBApp.Presentation.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;

namespace DepreciationDBApp.Presentation
{
    static class Program
    {
        public static IConfiguration Configuration;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //string path = Path.GetFullPath("appsettings.json").Replace(@"\DepreciationDBApp\bin\Debug\net5.0-windows\appsettings.json", string.Empty) + @"\DepreciationDBApp\appsettings.json";
            //var configurationBuilder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

            var builder = new HostBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<DepreciacionDBContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("default"));
                });
            });

            var host = builder.Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            services.AddDbContext<DepreciacionDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("default"));
            });
            services.AddScoped<IDepreciacionDBContext, DepreciacionDBContext>();
            services.AddScoped<IAssetRepository, EFAssetRepository>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IEmpleadoRepository, EFEmpleadoRepository>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IAssetEmployeeRepository, EFAssetEmployeeRepository>();
            services.AddScoped<IAssetEmployeeServices, AssetEmployeeServices>();
            services.AddScoped<IExcelRepository, ExcelAndEmailRepository>();
            services.AddScoped<IExcelServices, ExcelServices>();
            services.AddScoped<Form1>();
            services.AddScoped<FormAsignar>();

            using (var serviceScope = services.BuildServiceProvider())
            {
                var main = serviceScope.GetRequiredService<Form1>();
                Application.Run(main);
            }

        }
    }
}
