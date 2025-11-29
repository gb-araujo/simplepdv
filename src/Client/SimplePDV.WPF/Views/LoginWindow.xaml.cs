using System.Windows;
using System.Windows.Controls;
using SimplePDV.WPF.ViewModels;

namespace SimplePDV.WPF.Views;

public partial class LoginWindow : Window
{
    private readonly LoginViewModel _viewModel;
    
    public LoginWindow(LoginViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        _viewModel = viewModel;
        
        // Foco no campo de login ao abrir
        TxtLogin.Focus();
        
        // Permitir Enter para fazer login
        TxtLogin.KeyDown += (s, e) => { if (e.Key == System.Windows.Input.Key.Enter) TxtSenha.Focus(); };
        TxtSenha.KeyDown += (s, e) => { if (e.Key == System.Windows.Input.Key.Enter) BtnEntrar_Click(s, e); };
    }
    
    private async void BtnEntrar_Click(object sender, RoutedEventArgs e)
    {
        await _viewModel.LoginCommand.ExecuteAsync(TxtSenha);
    }
}
