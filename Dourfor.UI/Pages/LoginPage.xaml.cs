using Dourfor.Core.Requests.Account;
using Dourfor.UI.Services;

namespace Dourfor.UI.Pages;

public partial class LoginPage : ContentPage
{
    private readonly IAuthService _authService;

    public LoginPage(IAuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Validações básicas
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            ShowError("Por favor, informe seu e-mail");
            return;
        }

        if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            ShowError("Por favor, informe sua senha");
            return;
        }

        // Mostrar loading
        SetLoadingState(true);
        HideError();

        try
        {
            var request = new LoginRequest
            {
                Email = EmailEntry.Text.Trim(),
                Password = PasswordEntry.Text
            };

            var result = await _authService.LoginAsync(request);

            if (result.IsSuccess)
            {
                // Navegar para a página principal
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                ShowError(result.Message ?? "Erro ao fazer login");
            }
        }
        catch (Exception ex)
        {
            ShowError($"Erro inesperado: {ex.Message}");
        }
        finally
        {
            SetLoadingState(false);
        }
    }

    private async void OnCreateAccountTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    }

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }

    private void HideError()
    {
        ErrorLabel.IsVisible = false;
    }

    private void SetLoadingState(bool isLoading)
    {
        LoginButton.IsEnabled = !isLoading;
        LoginButton.IsVisible = !isLoading;
        LoadingIndicator.IsRunning = isLoading;
        LoadingIndicator.IsVisible = isLoading;
        EmailEntry.IsEnabled = !isLoading;
        PasswordEntry.IsEnabled = !isLoading;
    }
}
