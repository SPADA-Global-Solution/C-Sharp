using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

/// <summary>
/// Serviço responsável por gerenciar os usuários do sistema,
/// incluindo operações de carregamento, salvamento, autenticação e cadastro.
/// </summary>
public class UserService
{
    /// <summary>
    /// Caminho do arquivo JSON onde os dados dos usuários são armazenados.
    /// </summary>
    private const string FilePath = "users.json";

    /// <summary>
    /// Lista em memória dos usuários registrados no sistema.
    /// </summary>
    public List<User> Users { get; private set; } = new List<User>();

    /// <summary>
    /// Carrega os usuários do arquivo JSON para a lista <see cref="Users"/>.
    /// Caso o arquivo não exista ou haja erro, a lista permanece vazia.
    /// </summary>
    public void LoadUsers()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                var loaded = JsonSerializer.Deserialize<List<User>>(json);
                if (loaded != null)
                    Users = loaded;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar usuários: {ex.Message}");
        }
    }

    /// <summary>
    /// Salva a lista atual de usuários no arquivo JSON especificado por <see cref="FilePath"/>.
    /// </summary>
    public void SaveUsers()
    {
        try
        {
            string json = JsonSerializer.Serialize(Users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar usuários: {ex.Message}");
        }
    }

    /// <summary>
    /// Solicita as credenciais do usuário via console, verifica se correspondem a um usuário existente
    /// e retorna o usuário autenticado ou <c>null</c> caso as credenciais sejam inválidas.
    /// </summary>
    /// <returns>O usuário autenticado ou <c>null</c> se falhar na autenticação.</returns>
    public User? Login()
    {
        Console.Write("Usuário: ");
        string username = Console.ReadLine() ?? "";

        Console.Write("Senha: ");
        string password = Console.ReadLine() ?? "";

        foreach (var user in Users)
        {
            if (user.Username == username && user.Password == password)
            {
                Console.WriteLine("Login bem-sucedido!\n");
                return user;
            }
        }

        Console.WriteLine("Usuário ou senha inválidos!\n");
        return null;
    }

    /// <summary>
    /// Solicita via console a criação de um novo usuário com nome e senha,
    /// adiciona-o à lista de usuários, salva os dados e retorna o novo usuário criado.
    /// </summary>
    /// <returns>O novo usuário registrado.</returns>
    public User Register()
    {
        Console.Write("Novo usuário: ");
        string username = Console.ReadLine() ?? "";

        Console.Write("Nova senha: ");
        string password = Console.ReadLine() ?? "";

        var newUser = new User { Username = username, Password = password };
        Users.Add(newUser);
        SaveUsers();
        Console.WriteLine("Cadastro realizado com sucesso!\n");

        return newUser;
    }
}
