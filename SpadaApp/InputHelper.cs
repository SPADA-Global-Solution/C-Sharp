using System;

/// <summary>
/// Classe utilitária estática que fornece métodos para leitura e validação segura
/// de entradas de dados fornecidas pelo usuário no console.
/// </summary>
public static class InputHelper
{
    /// <summary>
    /// Solicita a entrada de uma string do usuário, garantindo que ela não seja nula, vazia ou composta apenas por espaços em branco.
    /// O método continuará solicitando até que uma entrada válida seja fornecida.
    /// </summary>
    /// <param name="prompt">Texto exibido ao usuário solicitando a entrada.</param>
    /// <returns>Uma string não vazia e sem espaços em branco nas extremidades.</returns>
    public static string ReadNonEmptyString(string prompt)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrEmpty(input))
                Console.WriteLine("Este campo é obrigatório. Por favor, preencha.");
        } while (string.IsNullOrEmpty(input));

        return input;
    }

    /// <summary>
    /// Solicita a entrada de um número inteiro do usuário, validando o formato.
    /// O método continuará solicitando até que uma entrada numérica válida seja informada.
    /// </summary>
    /// <param name="prompt">Texto exibido ao usuário solicitando a entrada.</param>
    /// <returns>Um número inteiro fornecido pelo usuário.</returns>
    public static int ReadInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine("Entrada inválida. Digite um número inteiro.");
        }
    }
}
