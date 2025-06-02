using System.Collections.Generic;

/// <summary>
/// Representa um usuário do sistema, contendo suas credenciais,
/// registros de incidentes e checklist de itens de segurança.
/// </summary>
public class User
{
    /// <summary>
    /// Nome de usuário único que identifica o usuário no sistema.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Senha do usuário para autenticação.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Lista de incidentes domésticos registrados pelo usuário.
    /// </summary>
    public List<Incident> Incidents { get; set; } = new List<Incident>();

    /// <summary>
    /// Checklist de preparação e prevenção com itens padrão para segurança doméstica.
    /// Cada item indica uma tarefa ou cuidado recomendado.
    /// </summary>
    public List<ChecklistItem> Checklist { get; set; } = new List<ChecklistItem>
    {
        new ChecklistItem { Description = "Lanterna carregada" },
        new ChecklistItem { Description = "Extintor acessível" },
        new ChecklistItem { Description = "Evitar velas próximas a cortinas" }
    };
}
