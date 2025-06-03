using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Classe principal do sistema SPADA (Sistema de Prevenção e Apoio em Desastres e Acidentes).
/// Responsável por gerenciar a interação com o usuário via console e orquestrar as funcionalidades
/// de autenticação, registro de acidentes, geração de relatórios, avaliação de risco, dicas de prevenção e checklist.
/// </summary>
class Program
{
    /// <summary>
    /// Serviço de gerenciamento de usuários, incluindo autenticação, registro e persistência.
    /// </summary>
    static UserService userService = new UserService();

    /// <summary>
    /// Usuário atualmente autenticado na aplicação.
    /// </summary>
    static User? currentUser = null;

    /// <summary>
    /// Método principal da aplicação. Realiza a autenticação do usuário e exibe o menu de funcionalidades.
    /// </summary>
    static void Main(string[] args)
    {
        userService.LoadUsers();  // Carrega usuários salvos previamente.

        Console.WriteLine("Bem-vindo ao SPADA!");

        bool autenticado = false;

        // Loop de autenticação: até que o usuário efetue login ou cadastro.
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

        if (currentUser == null)
        {
            Console.WriteLine("Erro: usuário não autenticado. Encerrando.");
            return;
        }

        int opcao;

        try
        {
            // Loop principal do menu de funcionalidades.
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

                opcao = InputHelper.ReadInt("Escolha uma opção: ");

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

                // Aguarda interação antes de retornar ao menu.
                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
        }

        // Salva alterações nos dados dos usuários antes de encerrar.
        userService.SaveUsers();
    }

    /// <summary>
    /// Permite ao usuário logado registrar um novo incidente, informando tipo e descrição.
    /// </summary>
    static void RegistrarAcidente()
    {
        if (currentUser == null) return;

        try
        {
            string tipo = InputHelper.ReadNonEmptyString("Tipo de Acidente (Queda/Incêndio/Outro): ");
            string desc = InputHelper.ReadNonEmptyString("Descrição: ");

            currentUser.Incidents.Add(new Incident
            {
                Date = DateTime.Now,
                Type = tipo,
                Description = desc
            });

            userService.SaveUsers();
            Console.WriteLine("Incidente registrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao registrar incidente: {ex.Message}");
        }
    }

    /// <summary>
    /// Gera e exibe um relatório agrupado de todos os incidentes registrados pelo usuário.
    /// </summary>
    static void GerarRelatorio()
    {
        if (currentUser == null) return;

        try
        {
            Console.WriteLine("Relatório de Incidentes:");
            foreach (var grupo in currentUser.Incidents.GroupBy(i => i.Type))
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
        if (currentUser == null) return;

        try
        {
            int count = currentUser.Incidents.Count;
            string risco = count < 3 ? "Baixo" : count < 6 ? "Moderado" : "Alto";
            Console.WriteLine($"Seu nível de risco é: {risco}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao avaliar risco: {ex.Message}");
        }
    }

    /// <summary>
    /// Exibe dicas de prevenção com base nos tipos de incidentes registrados pelo usuário.
    /// </summary>
    static void MostrarDicas()
    {
        if (currentUser == null) return;

        try
        {
            Console.WriteLine("Dicas de Prevenção:");
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
        if (currentUser == null) return;

        try
        {
            Console.WriteLine("Checklist de Preparação para Apagões:");
            for (int i = 0; i < currentUser.Checklist.Count; i++)
            {
                Console.WriteLine($"[{(currentUser.Checklist[i].IsCompleted ? "X" : " ")}] {i + 1}. {currentUser.Checklist[i].Description}");
            }

            Console.Write("Deseja marcar algum item como concluído? (s/n): ");
            string resposta = Console.ReadLine()?.ToLower() ?? "n";
            if (resposta == "s")
            {
                int item = InputHelper.ReadInt("Informe o número do item: ");
                if (item > 0 && item <= currentUser.Checklist.Count)
                {
                    currentUser.Checklist[item - 1].IsCompleted = true;
                    userService.SaveUsers();
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
