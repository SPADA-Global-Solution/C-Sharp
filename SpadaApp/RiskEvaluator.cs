using System;
using System.Collections.Generic;

/// <summary>
/// Classe responsável por avaliar o nível de risco com base nos incidentes registrados
/// e fornecer dicas personalizadas de prevenção.
/// </summary>
public class RiskEvaluator
{
    /// <summary>
    /// Calcula o nível de risco com base na quantidade total de incidentes.
    /// </summary>
    /// <param name="incidents">Lista de incidentes registrados.</param>
    /// <returns>
    /// Uma string que representa o nível de risco:
    /// "Baixo" para menos de 3 incidentes,
    /// "Moderado" para 3 a 5 incidentes,
    /// "Alto" para 6 ou mais incidentes.
    /// Retorna "Desconhecido" caso ocorra alguma exceção.
    /// </returns>
    public string CalculateRiskLevel(List<Incident> incidents)
    {
        try
        {
            int count = incidents.Count;

            if (count < 3)
                return "Baixo";

            if (count < 6)
                return "Moderado";

            return "Alto";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao calcular risco: {ex.Message}");
            return "Desconhecido";
        }
    }

    /// <summary>
    /// Gera uma lista de dicas personalizadas de prevenção com base nos tipos de incidentes existentes.
    /// </summary>
    /// <param name="incidents">Lista de incidentes registrados.</param>
    /// <returns>Lista de strings com dicas de prevenção personalizadas.</returns>
    public List<string> GetPersonalizedTips(List<Incident> incidents)
    {
        try
        {
            var tips = new List<string>();

            if (incidents.Exists(i => i.Type == "Incêndio"))
                tips.Add("Evite acender velas próximas a cortinas.");

            if (incidents.Exists(i => i.Type == "Queda"))
                tips.Add("Mantenha passagens bem iluminadas.");

            return tips;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao gerar dicas: {ex.Message}");
            return new List<string>();
        }
    }
}
