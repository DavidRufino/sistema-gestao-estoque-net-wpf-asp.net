using PDVnet.GestaoProdutos.UI.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PDVnet.GestaoProdutos.UI.Pages
{
    public partial class DashboardPage : Page
    {
        private bool _isLoaded = false;

        public DashboardPage()
        {
            InitializeComponent();

            this.Title = "DashboardPage";
            DataContext = new DashboardViewModel(); // Instancia a ViewModel diretamente

            //Loaded -= DashboardPage_Loaded;
            //Loaded += DashboardPage_Loaded;
        }

        private async void DashboardPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_isLoaded) return;
            this._isLoaded = true;

            // carregar os dados da tabela ao exibir
            if (DataContext is DashboardViewModel viewModel)
            {
                await viewModel.LoadDashboardDataCommand.ExecuteAsync(null);
            }
        }
    }
}
