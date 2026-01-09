using Dourfor.Core.Requests.Account;
using Dourfor.UI.Services;

namespace Dourfor.UI.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly IAuthService _authService;

    public RegisterPage(IAuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Validações
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            ShowError("Por favor, informe seu e-mail");
            return;
        }

        if (!IsValidEmail(EmailEntry.Text))
        {
            ShowError("Por favor, informe um e-mail válido");
            return;
        }

        if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            ShowError("Por favor, informe uma senha");
            return;
        }

        if (PasswordEntry.Text.Length < 6)
        {
            ShowError("A senha deve ter no mínimo 6 caracteres");
            return;
        }

        if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            ShowError("As senhas não conferem");
            return;
        }

        // Mostrar loading
        SetLoadingState(true);
        HideMessages();

        try
        {
            var request = new RegisterRequest
            {
                Email = EmailEntry.Text.Trim(),
                Password = PasswordEntry.Text
            };

            var result = await _authService.RegisterAsync(request);

            if (result.IsSuccess)
            {
                ShowSuccess("Conta criada com sucesso! Redirecionando para o login...");
                
                // Aguardar um pouco para mostrar a mensagem
                await Task.Delay(2000);
                
                // Navegar para a página de login
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                ShowError(result.Message ?? "Erro ao criar conta");
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

    private async void OnLoginTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
        SuccessLabel.IsVisible = false;
    }

    private void ShowSuccess(string message)
    {
        SuccessLabel.Text = message;
        SuccessLabel.IsVisible = true;
        ErrorLabel.IsVisible = false;
    }

    private void HideMessages()
    {
        ErrorLabel.IsVisible = false;
        SuccessLabel.IsVisible = false;
    }

    private void SetLoadingState(bool isLoading)
    {
        RegisterButton.IsEnabled = !isLoading;
        RegisterButton.IsVisible = !isLoading;
        LoadingIndicator.IsRunning = isLoading;
        LoadingIndicator.IsVisible = isLoading;
        EmailEntry.IsEnabled = !isLoading;
        PasswordEntry.IsEnabled = !isLoading;
        ConfirmPasswordEntry.IsEnabled = !isLoading;
    }
}
