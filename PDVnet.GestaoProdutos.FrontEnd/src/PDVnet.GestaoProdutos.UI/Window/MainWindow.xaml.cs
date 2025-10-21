using PDVnet.GestaoProdutos.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PDVnet.GestaoProdutos.UI.Window;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : System.Windows.Window
{
    private readonly DashboardPage _dashboardPage;

    public MainWindow()
    {
        InitializeComponent();

        this.Title = "PDVnet - Projeto";
        this.Height = 800;
        this.Width = 800;

        // Instanciando DashboardPage
        _dashboardPage = new();

        // Incluindo instancia de DashboardPage ao Content de Frame
        MainFrame.Content = _dashboardPage; // Carrega a DashboardPage no Frame
    }
}
