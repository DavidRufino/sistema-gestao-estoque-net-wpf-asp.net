using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace PDVnet.GestaoProdutos.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        // App do WPF é muito simples comparado com UWP/WinUI 3
        this.StartupUri = new System.Uri("Window/MainWindow.xaml", System.UriKind.Relative);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // deixar CultureInfo como pt-BR para toda aplicacao
        var culture = new CultureInfo("pt-BR");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }
}