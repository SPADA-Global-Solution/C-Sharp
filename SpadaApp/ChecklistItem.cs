/// <summary>
/// Representa um item de checklist relacionado à preparação ou prevenção.
/// </summary>
public class ChecklistItem
{
    /// <summary>
    /// Descrição do item do checklist.
    /// Indica a ação ou recurso a ser verificado.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Indica se o item do checklist foi concluído.
    /// true se concluído; caso contrário, false.
    /// </summary>
    public bool IsCompleted { get; set; }
}
