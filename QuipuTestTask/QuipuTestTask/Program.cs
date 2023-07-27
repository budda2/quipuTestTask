using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuipuTestTask.MarkupExtensions;
using QuipuTestTask.Services.Extensions;
using QuipuTestTask.ViewModels;

namespace QuipuTestTask;

public class Program
{
    [STAThread]
    public static void Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
                services.AddTransient<MainWindowViewModel>();
                services.AddServices();
            })
            .Build();
        DependencyInjectionSource.ServiceProvider = host.Services;
        
        var app = host.Services.GetService<App>();
        app?.Run();
    }
}