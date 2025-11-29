using System.Windows;
using SimplePDV.WPF.ViewModels;

namespace SimplePDV.WPF.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        
        // Navegar para vendas ao iniciar
        viewModel.NavigateToVendasCommand.Execute(null);
    }
}
