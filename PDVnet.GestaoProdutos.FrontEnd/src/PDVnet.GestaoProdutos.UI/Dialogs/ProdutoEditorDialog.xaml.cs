using PDVnet.GestaoProdutos.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PDVnet.GestaoProdutos.UI.Dialogs
{
    /// <summary>
    /// Interaction logic for ProdutoEditorDialog.xaml
    /// </summary>
    public partial class ProdutoEditorDialog : System.Windows.Window
    {
        public ProdutoEditorDialog(ProdutoEditorViewModel viewModel)
        {
            InitializeComponent();

            this.Title = viewModel.WindowTitle;
            this.Height = 250;
            this.Width = 400;

            DataContext = viewModel;

            viewModel.SaveCompleted -= ViewModel_SaveCompleted;
            viewModel.SaveCompleted += ViewModel_SaveCompleted;
        }

        private void ViewModel_SaveCompleted(object? sender, EventArgs e)
        {
            Debug.WriteLine($"[ProdutoEditorDialog] ViewModel_SaveCompleted");
            this.DialogResult = true;
            this.Close();
        }
    }
}
