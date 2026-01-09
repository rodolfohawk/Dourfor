namespace Dourfor.UI;

public static class Configuration
{
    public const string HttpClientName = "dourfor";
    
    // Ajuste a URL conforme necessário (use seu IP local para Android/iOS em desenvolvimento)
    // Para Android Emulator: http://10.0.2.2:5164
    // Para iOS Simulator: http://localhost:5164
    // Para dispositivo físico: http://<seu-ip>:5164
    public static string BackendUrl { get; set; } = "http://localhost:5164";
}
