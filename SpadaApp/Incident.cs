/// <summary>
/// Representa um incidente ou acidente doméstico registrado pelo usuário.
/// Contém informações sobre a data, tipo e descrição do incidente.
/// </summary>
public class Incident
{
    /// <summary>
    /// Data e hora em que o incidente ocorreu.
    /// </summary>
    public System.DateTime Date { get; set; }

    /// <summary>
    /// Tipo do incidente (por exemplo: Queda, Incêndio, etc.).
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada do incidente.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
