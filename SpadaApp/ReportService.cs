using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Serviço responsável pela geração de relatórios de incidentes.
/// Realiza agrupamentos e ordenações para apresentar estatísticas.
/// </summary>
public class ReportService
{
    /// <summary>
    /// Lista de incidentes que será utilizada na geração dos relatórios.
    /// </summary>
    private List<Incident> incidents;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="ReportService"/> com a lista de incidentes especificada.
    /// </summary>
    /// <param name="incidents">Lista de incidentes a ser utilizada nos relatórios.</param>
    public ReportService(List<Incident> incidents)
    {
        this.incidents = incidents;
    }

    /// <summary>
    /// Gera um relatório agrupando os incidentes por tipo e ordenando-os pela quantidade de ocorrências, de forma decrescente.
    /// </summary>
    /// <returns>
    /// Uma lista de strings, cada uma representando um tipo de incidente e a quantidade de ocorrências.
    /// </returns>
    public List<string> GenerateReport()
    {
        try
        {
            return incidents.GroupBy(i => i.Type.Trim().ToUpper())
                            .OrderByDescending(g => g.Count())
                            .Select(g => $"{g.Key}: {g.Count()} ocorrência(s)")
                            .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao gerar relatório: {ex.Message}");
            return new List<string>();
        }
    }
}
