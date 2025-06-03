using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

/// <summary>
/// Classe responsável pela gestão dos usuários do sistema, incluindo autenticação,
/// cadastro e persistência dos dados em arquivo JSON.
/// </summary>
public class UserService
{
    /// <summary>
    /// Caminho do arquivo JSON utilizado para armazenar os dados dos usuários.
    /// </summary>
    private const string FilePath = "users.json";

    /// <summary>
    /// Lista que mantém os usuários cadastrados em memória.
    /// </summary>
    public List<User> Users { get; private set; } = new List<User>();

    /// <summary>
    /// Carrega os usuários do arquivo JSON para a memória.
    /// Caso o arquivo não exista ou ocorra algum erro, mantém a lista vazia.
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
    /// Salva os dados da lista de usuários no arquivo JSON.
    /// Substitui completamente o conteúdo anterior.
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
    /// Realiza o processo de autenticação do usuário.
    /// Solicita nome de usuário e senha, e busca correspondência na lista de usuários.
    /// </summary>
    /// <returns>
    /// Retorna o objeto <see cref="User"/> correspondente se a autenticação for bem-sucedida,
    /// ou <c>null</c> se falhar.
    /// </returns>
    public User? Login()
    {
        string username = InputHelper.ReadNonEmptyString("Usuário: ");
        string password = InputHelper.ReadNonEmptyString("Senha: ");

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
    /// Realiza o cadastro de um novo usuário no sistema.
    /// Garante que o nome de usuário seja único e que a senha tenha pelo menos 6 caracteres.
    /// </summary>
    /// <returns>
    /// Retorna o objeto <see cref="User"/> recém-cadastrado.
    /// </returns>
    public User Register()
    {
        string username = InputHelper.ReadNonEmptyString("Novo usuário: ");

        // Validação para evitar nomes de usuários duplicados.
        if (Users.Exists(u => u.Username == username))
        {
            Console.WriteLine("Erro: este nome de usuário já está em uso.");
            return Register();
        }

        string password;

        // Solicita a senha até que ela atenda ao requisito mínimo de 6 caracteres.
        do
        {
            password = InputHelper.ReadNonEmptyString("Nova senha (mínimo 6 caracteres): ");
            if (password.Length < 6)
                Console.WriteLine("Erro: senha deve ter pelo menos 6 caracteres.");
        } while (password.Length < 6);

        var newUser = new User { Username = username, Password = password };
        Users.Add(newUser);
        SaveUsers();

        Console.WriteLine("Cadastro realizado com sucesso!\n");
        return newUser;
    }
}
