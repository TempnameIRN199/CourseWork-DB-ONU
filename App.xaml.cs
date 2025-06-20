namespace CourseWork_DB_ONU
{
    using CourseWork_DB_ONU.Data;
    using CourseWork_DB_ONU.Services.EnumService;
    using CourseWork_DB_ONU.Services.Order;
    using CourseWork_DB_ONU.Services.Organization;
    using CourseWork_DB_ONU.Services.Outlet;
    using CourseWork_DB_ONU.Services.Product;
    using CourseWork_DB_ONU.Services.Seller;
    using CourseWork_DB_ONU.Services.Supplier;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Windows;

    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        static App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json", optional: false);
                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("SPAS12Connection");

                    services.AddDbContext<DatabaseContext>(options =>
                        options.UseSqlServer(connectionString));

                    services.AddSingleton<MainWindow>();
                    services.AddScoped<IProductService, ProductService>();
                    services.AddScoped<IOrganizationService, OrganizationService>();
                    services.AddScoped<ISupplierService, SupplierService>();
                    services.AddScoped<IOrderService, OrderService>();
                    services.AddScoped<IOutletService, OutletService>();
                    services.AddScoped<ISellerService, SellerService>();
                    services.AddScoped<IEnumService, EnumService>();

                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            AppHost.Start();

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }

}
