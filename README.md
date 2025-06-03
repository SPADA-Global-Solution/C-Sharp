# SPADA — Sistema de Prevenção de Acidentes Domésticos Automático

## Finalidade do Sistema

O SPADA é um sistema de console, desenvolvido em C#, que tem como finalidade ajudar os usuários a prevenir acidentes domésticos relacionados a falhas de energia, como apagões.

O sistema permite:
- Registro de acidentes domésticos.
- Geração de relatórios organizados.
- Avaliação automática do nível de risco pessoal.
- Sugestão de dicas personalizadas de prevenção.
- Gerenciamento de um checklist de preparação para apagões.

Todos os dados são persistidos automaticamente em um arquivo JSON, garantindo que o usuário possa consultar e atualizar suas informações em sessões futuras.

---

## Instruções de Execução

### Pré-requisitos

- .NET SDK 8.0 ou superior instalado.
- Editor de código ou IDE: Visual Studio 2022 (recomendado) ou outro de sua preferência.
- Sistema Operacional: Windows, Linux ou MacOS.

### Passos para execução

1. Clone o repositório com o comando:

git clone https://github.com/SPADA-Global-Solution/C-Sharp.git

2. Abra o projeto:
  
   No Visual Studio, abra o arquivo SpadaApp.sln.
Ou via terminal, navegue até a pasta SpadaApp.

3. Execute o sistema
4. Faça login, ou cadastre algum usuario

Para Login, pode ser utilizado:

- Username: João
- Senha: 1234


### Dependências 

- .NET SDK 8.0: ambiente de desenvolvimento e execução.
- System.Text.Json: para serialização e persistência de dados no formato JSON (já incluído no .NET 8).


### Estrutura de Pastas

SpadaApp/
├── Program.cs               → Classe principal: controla o fluxo geral do sistema.

├── UserService.cs          → Gerencia usuários, login, cadastro e persistência.

├── InputHelper.cs          → Auxilia na validação e leitura de entradas.

├── User.cs                 → Modelo de dados de usuário.

├── Incident.cs             → Modelo de dados de incidente.

├── ChecklistItem.cs        → Modelo de item de checklist.

├── users.json              → Arquivo persistente contendo os dados de usuários e incidentes.

└── SpadaApp.sln            → Solução do Visual Studio.


# Observações 

- O sistema salva automaticamente as alterações realizadas após cada ação do usuário.
- O arquivo users.json será criado na primeira execução, caso não exista.
- Todo o fluxo é orientado por menus e instruções exibidas no console.
