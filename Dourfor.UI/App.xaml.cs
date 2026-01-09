namespace Dourfor.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var shell = new AppShell();
            
            // Definir a tela de login como inicial
            shell.CurrentItem = shell.Items.FirstOrDefault(item => item.Route == "IMPL_LoginPage");
            
            return new Window(shell);
        }
    }
}