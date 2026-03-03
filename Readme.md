# Home Control - Sistema de Controle de Gastos Residenciais

Projeto fullstack desenvolvido para o gerenciamento de finanças pessoais e residenciais. O sistema permite cadastrar pessoas, criar categorias e registrar transações (receitas e despesas), gerando automaticamente um dashboard com os saldos consolidados de cada usuário.

## Tecnologias Utilizadas

### Back-end (home-control-api)
* **Framework:** ASP.NET Core 8
* **Linguagem:** C#
* **Banco de Dados:** Entity Framework Core (SQLite)
* **Swagger:** Documentação e interface da API.

### Front-end (admin-web e player-web)
* **Framework:** React v18
* **Linguagem:** TypeScript
* **Gerenciamento de Estado:** Zustand (para estado global)
* **Interface de Usuário:** Ant Design 
* **Roteamento:** React Router DOM
* **Requisições HTTP:** Axios
* **Build Tool:** Vite

## Como Executar o Projeto

### Pré-requisitos
Certifique-se de ter o **SDK do .NET 8** e o **Node.js** instalados em sua máquina.

### 1. Rodando a API (Backend)
1.  Navegue até o diretório `home-control-api`.

2.  Restaure os pacotes NuGet:
    
    dotnet restore
    
3.  Aplique as migrations do banco de dados (o banco será criado automaticamente):

    dotnet ef database update
    
4.  Inicie o servidor:
    
    dotnet run
    
O back-end estará disponível em `http://localhost:5172`. (`http://localhost:5172/swagger` para acessar via Swagger)


### 2. Front-end (control-web)
1.  Abra o terminal na pasta control-web.

2.  Instale as dependências:
    
    npm install
    
3.  Inicie a aplicação de administração:
    
    npm run dev
    
A aplicação estará disponível em `http://localhost:5173`