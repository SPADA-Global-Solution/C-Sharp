using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Classe principal do sistema SPADA.
/// Responsável por gerenciar o fluxo de autenticação do usuário e exibir o menu de funcionalidades.
/// </summary>
class Program
{
    /// <summary>
    /// Serviço responsável pelo gerenciamento de usuários.
    /// </summary>
    static UserService userService = new UserService();

    /// <summary>
    /// Usuário atualmente autenticado no sistema.
    /// </summary>
    static User? currentUser = null;

    /// <summary>
    /// Método principal da aplicação. 
    /// Executa o fluxo de autenticação e apresenta o menu com as opções do sistema.
    /// </summary>
    /// <param name="args">Argumentos de linha de comando (não utilizados).</param>
    static void Main(string[] args)
    {
        userService.LoadUsers(); // Carrega usuários salvos anteriormente.

        Console.WriteLine("Bem-vindo ao SPADA!");

        bool autenticado = false;

        // Loop de autenticação até que o usuário esteja autenticado com sucesso.
        while (!autenticado)
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Cadastro");
            Console.Write("Escolha uma opção: ");
            string? input = Console.ReadLine();

            if (input == "1")
            {
                currentUser = userService.Login();
                autenticado = currentUser != null;
            }
            else if (input == "2")
            {
                currentUser = userService.Register();
                autenticado = true;
            }
            else
            {
                Console.WriteLine("Opção inválida!\n");
            }
        }

        // Verifica se o usuário foi autenticado com sucesso.
        if (currentUser == null)
        {
            Console.WriteLine("Erro: usuário não autenticado. Encerrando.");
            return;
        }

        int opcao;

        try
        {
            // Loop do menu principal
            do
            {
                Console.Clear();
                Console.WriteLine($"Usuário logado: {currentUser.Username}");
                Console.WriteLine("1. Registrar Acidente");
                Console.WriteLine("2. Gerar Relatório");
                Console.WriteLine("3. Avaliar Nível de Risco");
                Console.WriteLine("4. Dicas de Prevenção");
                Console.WriteLine("5. Checklist de Preparação");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                string? inputMenu = Console.ReadLine();
                if (!int.TryParse(inputMenu, out opcao))
                {
                    opcao = -1; // Define como inválida se a conversão falhar.
                }

                // Executa a funcionalidade conforme a escolha do usuário.
                switch (opcao)
                {
                    case 1: RegistrarAcidente(); break;
                    case 2: GerarRelatorio(); break;
                    case 3: AvaliarRisco(); break;
                    case 4: MostrarDicas(); break;
                    case 5: ExibirChecklist(); break;
                    case 0: Console.WriteLine("Saindo..."); break;
                    default: Console.WriteLine("Opção inválida."); break;
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();

            } while (opcao != 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
        }

        userService.SaveUsers(); // Salva as alterações nos dados dos usuários.
    }

    /// <summary>
    /// Registra um novo incidente no perfil do usuário atual.
    /// </summary>
    static void RegistrarAcidente()
    {
        if (currentUser == null)
        {
            Console.WriteLine("Nenhum usuário logado.");
            return;
        }

        try
        {
            Console.Write("Tipo de Acidente (Queda/Incêndio/Outro): ");
            string tipo = Console.ReadLine() ?? "";

            Console.Write("Descrição: ");
            string desc = Console.ReadLine() ?? "";

            // Adiciona o incidente à lista do usuário.
            currentUser.Incidents.Add(new Incident
            {
                Date = DateTime.Now,
                Type = tipo,
                Description = desc
            });

            Console.WriteLine("Incidente registrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao registrar incidente: {ex.Message}");
        }
    }

    /// <summary>
    /// Gera um relatório com a contagem de cada tipo de incidente registrado pelo usuário.
    /// </summary>
    static void GerarRelatorio()
    {
        if (currentUser == null)
        {
            Console.WriteLine("Nenhum usuário logado.");
            return;
        }

        try
        {
            Console.WriteLine("Relatório de Incidentes:");

            // Agrupa e conta os incidentes por tipo.
            foreach (var grupo in currentUser.Incidents.GroupBy(i => i.Type.Trim().ToUpper()))
            {
                Console.WriteLine($"{grupo.Key}: {grupo.Count()} ocorrência(s)");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao gerar relatório: {ex.Message}");
        }
    }

    /// <summary>
    /// Avalia o nível de risco do usuário com base na quantidade de incidentes registrados.
    /// </summary>
    static void AvaliarRisco()
    {
        if (currentUser == null)
        {
            Console.WriteLine("Nenhum usuário logado.");
            return;
        }

        try
        {
            int count = currentUser.Incidents.Count;

            // Define o nível de risco conforme a quantidade de incidentes.
            string risco = count < 3 ? "Baixo" : count < 6 ? "Moderado" : "Alto";

            Console.WriteLine($"Seu nível de risco é: {risco}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao avaliar risco: {ex.Message}");
        }
    }

    /// <summary>
    /// Mostra dicas de prevenção com base nos tipos de incidentes registrados pelo usuário.
    /// </summary>
    static void MostrarDicas()
    {
        if (currentUser == null)
        {
            Console.WriteLine("Nenhum usuário logado.");
            return;
        }

        try
        {
            Console.WriteLine("Dicas de Prevenção:");

            // Exibe dicas personalizadas conforme incidentes.
            if (currentUser.Incidents.Exists(i => i.Type == "Incêndio"))
                Console.WriteLine("- Evite acender velas próximas a cortinas.");
            if (currentUser.Incidents.Exists(i => i.Type == "Queda"))
                Console.WriteLine("- Mantenha passagens bem iluminadas.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao mostrar dicas: {ex.Message}");
        }
    }

    /// <summary>
    /// Exibe o checklist de preparação do usuário e permite marcar itens como concluídos.
    /// </summary>
    static void ExibirChecklist()
    {
        if (currentUser == null)
        {
            Console.WriteLine("Nenhum usuário logado.");
            return;
        }

        try
        {
            Console.WriteLine("Checklist de Preparação para Apagões:");

            // Exibe cada item do checklist com seu status.
            for (int i = 0; i < currentUser.Checklist.Count; i++)
            {
                Console.WriteLine($"[{(currentUser.Checklist[i].IsCompleted ? "X" : " ")}] {i + 1}. {currentUser.Checklist[i].Description}");
            }

            Console.Write("Deseja marcar algum item como concluído? (s/n): ");
            string resposta = Console.ReadLine() ?? "n";

            if (resposta.ToLower() == "s")
            {
                Console.Write("Informe o número do item: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int item) && item > 0 && item <= currentUser.Checklist.Count)
                {
                    currentUser.Checklist[item - 1].IsCompleted = true;
                    Console.WriteLine("Item marcado como concluído.");
                }
                else
                {
                    Console.WriteLine("Número inválido.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao exibir checklist: {ex.Message}");
        }
    }
}
