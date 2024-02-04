using AutoEmptyStandbyList.Client.ViewModels;
using AutoEmptyStandbyList.Client.Views;
using AutoEmptyStandbyList.Shared.Services;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace AutoEmptyStandbyList.Client;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var provider = ConfigureServices().BuildServiceProvider();
            
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<MainWindowViewModel>();
        
        services.AddSingleton<IPreferencesService, PreferencesesService>();
        services.AddSingleton<ITaskService, TaskService>();

        return services;
    }
}