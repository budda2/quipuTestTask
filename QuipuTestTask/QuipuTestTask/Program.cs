using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            })
            .Build();

        var app = host.Services.GetService<App>();
        app?.Run();
    }
}