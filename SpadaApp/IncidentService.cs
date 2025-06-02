using System;
using System.Collections.Generic;

/// <summary>
/// Serviço responsável pelo gerenciamento de incidentes no sistema.
/// Fornece métodos para adicionar e armazenar registros de incidentes.
/// </summary>
public class IncidentService
{
    /// <summary>
    /// Lista que armazena todos os incidentes registrados.
    /// </summary>
    public List<Incident> Incidents { get; set; } = new List<Incident>();

    /// <summary>
    /// Adiciona um novo incidente à lista de incidentes.
    /// </summary>
    /// <param name="incident">O incidente a ser adicionado.</param>
    public void AddIncident(Incident incident)
    {
        try
        {
            Incidents.Add(incident);
            Console.WriteLine("Incidente adicionado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar incidente: {ex.Message}");
        }
    }
}
