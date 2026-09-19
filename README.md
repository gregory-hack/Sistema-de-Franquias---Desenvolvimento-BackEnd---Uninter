# Sistema de Gestão de Franquias — Franquias.Api

- Trabalho acadêmico da disciplina **Desenvolvimento Back-end** (Uninter).
- Aluno: Gregory Antunes Hack — RU 3699000
- Professor: Rodrigo da S. do Nascimento

## Objetivo do projeto

API REST desenvolvida em C# / ASP.NET Core para centralizar a gestão de uma rede de franquias: cadastro de franqueadora e unidades franqueadas, usuários e perfis de acesso, catálogo de produtos/serviços, fornecedores, controle de estoque por unidade, registro de vendas, cálculo de royalties, chamados de suporte e relatórios/indicadores gerenciais.

## Tecnologias utilizadas

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET 10](https://img.shields.io/badge/.NET%2010-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-512BD4?style=for-the-badge&logo=nuget&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

## Arquitetura

Projeto organizado em camadas, com separação de responsabilidades:

```
Franquias.Api/
├── Controllers/     # Endpoints da API
├── Models/          # Entidades do domínio
├── DTOs/            # Objetos de entrada/saída
├── Services/        # Regras de negócio
├── Repositories/     # Acesso a dados (EF Core)
├── Data/             # DbContext e configuração do banco
└── Migrations/       # Migrations do Entity Framework
```

## Requisitos de execução

- [.NET SDK 10](https://dotnet.microsoft.com/download) instalado
- Não é necessário instalar SQLite separadamente (o driver já vem via pacote NuGet)

## Como executar

1. Clone o repositório e entre na pasta do projeto:
   ```
   git clone: https://github.com/gregory-hack/Sistema-de-Franquias---Desenvolvimento-BackEnd---Uninter
   cd Franquias/Franquias.Api
   ```

2. Restaure os pacotes:
   ```
   dotnet restore
   ```

3. Rode a aplicação:
   ```
   dotnet run
   ```
    
   ```
   (ou `dotnet watch run` durante desenvolvimento, para recarregar automaticamente a cada alteração e abrir direto no swagger)
   
   ```

5. As migrations do banco são aplicadas **automaticamente** na inicialização. Não é necessário rodar `dotnet ef database update` manualmente. O arquivo `franquias.db` já é entregue neste repositório populado com dados de exemplo (várias unidades, produtos, vendas e chamados), para facilitar a o uso e testes.

6. Acesse a documentação interativa (Swagger) em:
   ```
   https://localhost:7064/swagger
   ```
   ou
   ```
   http://localhost:5176/swagger
   ```

## Autenticação

A API usa autenticação via JWT. Ao subir a aplicação pela primeira vez, o banco é populado automaticamente (seed) com três perfis de acesso: **Administrador**, **GestorUnidade** e **Operador**. Nenhum usuário é criado automaticamente, é necessário cadastrar um usuário via `POST /api/usuarios` antes de conseguir logar em `POST /api/auth/login`.

Após o login, copie o token JWT retornado e cole no botão **Authorize** (ícone de cadeado) no topo do Swagger, para acessar os endpoints protegidos.

## Principais grupos de endpoints

| Grupo | Rota base |
|---|---|
| Autenticação | `/api/auth` |
| Usuários | `/api/usuarios` |
| Franqueadora | `/api/franqueadoras` |
| Unidades Franqueadas | `/api/unidades` |
| Franqueados | `/api/franqueados` |
| Categorias | `/api/categorias` |
| Produtos/Serviços | `/api/produtos` |
| Fornecedores | `/api/fornecedores` |
| Estoque | `/api/estoques` |
| Vendas | `/api/vendas` |
| Royalties | `/api/royalties` |
| Chamados de Suporte | `/api/chamados` |
| Relatórios | `/api/relatorios` |
