using MudBlazor;
using MudBlazor.Utilities;

namespace Dourfor.Web;

public static class Configuration
{
    public const string HttpClientName = "dourfor";
    public static string StripePublicKey { get; set; } = "";

    public static string BackendUrl { get; set; } = "http://localhost:5164";
    
    public static MudTheme Theme = new()
    {
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Raleway", "sans-serif"]
            }
        },
        PaletteLight = new PaletteLight
        {
            Primary = new MudColor("#5DC1E9"),
            PrimaryContrastText = new MudColor("#000000"),
            Secondary = Colors.LightBlue.Lighten4,
            AppbarBackground = new MudColor("#1E9FFC"),
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            DrawerText = Colors.Shades.White,
            DrawerBackground = Colors.BlueGray.Darken4
        },
        PaletteDark = new PaletteDark
        {
            Primary = Colors.LightBlue.Accent3,
            Secondary = Colors.LightBlue.Darken3,
            Background = Colors.LightBlue.Darken4,
            AppbarBackground = Colors.LightBlue.Accent3,
            AppbarText = Colors.Shades.Black,
            PrimaryContrastText = new MudColor("#000000")
        }
    };
}
