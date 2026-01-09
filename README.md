# Dourfor

Sistema completo de gestão financeira pessoal com funcionalidades de e-commerce e pagamentos integrados.

## 📋 Sobre o Projeto

Dourfor é uma aplicação web desenvolvida em .NET 9.0 que combina gestão financeira pessoal com um sistema de e-commerce. O sistema permite aos usuários gerenciar suas transações financeiras, categorias, perfis e também realizar compras de produtos com pagamentos integrados via Stripe.

## 🚀 Tecnologias Utilizadas

### Backend (Dourfor.Api)
- **.NET 9.0** - Framework principal
- **ASP.NET Core Web API** - API RESTful
- **Entity Framework Core 9.0** - ORM para acesso a dados
- **SQL Server** - Banco de dados relacional
- **ASP.NET Identity** - Sistema de autenticação e autorização
- **Stripe.NET** - Integração com gateway de pagamentos
- **Swagger/OpenAPI** - Documentação da API

### Frontend (Dourfor.Web)
- **Blazor WebAssembly** - Framework para SPA
- **MudBlazor** - Biblioteca de componentes UI
- **ASP.NET Components WebAssembly Authentication** - Autenticação client-side

### Core (Dourfor.Core)
- Biblioteca de classes compartilhada contendo:
  - Modelos de domínio
  - Interfaces de handlers
  - Requests e Responses
  - Enums e configurações

## 📁 Estrutura do Projeto

```
Dourfor/
├── Dourfor.Api/          # API Backend
│   ├── Common/           # Extensões e utilitários
│   ├── Data/             # Contexto do banco e mapeamentos
│   ├── Endpoints/        # Endpoints da API
│   ├── Handlers/         # Implementação de handlers
│   └── Models/           # Modelos específicos da API
├── Dourfor.Core/         # Biblioteca compartilhada
│   ├── Common/           # Extensões comuns
│   ├── Enums/            # Enumerações
│   ├── Handlers/         # Interfaces de handlers
│   ├── Models/           # Modelos de domínio
│   ├── Requests/         # DTOs de requisição
│   └── Responses/        # DTOs de resposta
└── Dourfor.Web/          # Frontend Blazor
    ├── Components/       # Componentes reutilizáveis
    ├── Handlers/         # Handlers HTTP
    ├── Layouts/          # Layouts da aplicação
    ├── Pages/            # Páginas da aplicação
    └── Security/         # Autenticação e segurança
```

## ⚙️ Funcionalidades

### Gestão Financeira
- ✅ **Transações**: Cadastro e controle de receitas e despesas
- ✅ **Categorias**: Organização de transações por categorias personalizadas
- ✅ **Relatórios**: 
  - Resumo financeiro
  - Receitas por categoria
  - Despesas por categoria
  - Gráficos de receitas e despesas
- ✅ **Perfis**: Gerenciamento de múltiplos perfis financeiros

### E-commerce
- ✅ **Produtos**: Catálogo de produtos disponíveis
- ✅ **Pedidos**: Sistema completo de gerenciamento de pedidos
- ✅ **Vouchers**: Sistema de cupons de desconto
- ✅ **Pagamentos**: Integração com Stripe para processamento de pagamentos
- ✅ **Status de Pedidos**: Rastreamento completo (Aguardando Pagamento, Pago, Cancelado, Reembolsado)

### Autenticação e Autorização
- ✅ **Registro de usuários**
- ✅ **Login/Logout**
- ✅ **Gerenciamento de roles**
- ✅ **Autenticação baseada em cookies**

## 🔧 Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express ou versão completa)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)
- Conta [Stripe](https://stripe.com/) (para funcionalidades de pagamento)

## 🛠️ Instalação e Configuração

### 1. Clone o repositório
```bash
git clone https://github.com/rodolfohawk/Dourfor.git
cd Dourfor
```

### 2. Configure a string de conexão
Edite o arquivo `Dourfor.Api/appsettings.json` e configure a connection string do SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DourforDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Configure as chaves do Stripe
No arquivo `Dourfor.Api/appsettings.json`, adicione suas chaves do Stripe:

```json
{
  "StripeSettings": {
    "ApiKey": "sua_chave_secreta_aqui"
  }
}
```

### 4. Execute as migrations
```bash
cd Dourfor.Api
dotnet ef database update
```

### 5. Execute a aplicação

#### Executar a API:
```bash
cd Dourfor.Api
dotnet run
```
A API estará disponível em `https://localhost:5001` (ou porta configurada)

#### Executar o Frontend:
```bash
cd Dourfor.Web
dotnet run
```
O frontend estará disponível em `https://localhost:5002` (ou porta configurada)

## 📚 Documentação da API

Com a API em execução, acesse a documentação Swagger em:
```
https://localhost:5001/swagger
```

## 🎯 Principais Endpoints

### Categorias
- `GET /v1/categories` - Listar todas as categorias
- `GET /v1/categories/{id}` - Obter categoria por ID
- `POST /v1/categories` - Criar nova categoria
- `PUT /v1/categories/{id}` - Atualizar categoria
- `DELETE /v1/categories/{id}` - Deletar categoria

### Transações
- `GET /v1/transactions` - Listar transações por período
- `GET /v1/transactions/{id}` - Obter transação por ID
- `POST /v1/transactions` - Criar nova transação
- `PUT /v1/transactions/{id}` - Atualizar transação
- `DELETE /v1/transactions/{id}` - Deletar transação

### Produtos
- `GET /v1/products` - Listar todos os produtos
- `GET /v1/products/{slug}` - Obter produto por slug

### Pedidos
- `GET /v1/orders` - Listar pedidos
- `GET /v1/orders/{number}` - Obter pedido por número
- `POST /v1/orders` - Criar novo pedido
- `POST /v1/orders/{number}/pay` - Realizar pagamento
- `POST /v1/orders/{number}/cancel` - Cancelar pedido
- `POST /v1/orders/{number}/refund` - Reembolsar pedido

### Relatórios
- `GET /v1/reports/financial-summary` - Resumo financeiro
- `GET /v1/reports/incomes-expenses` - Receitas e despesas
- `GET /v1/reports/incomes-by-category` - Receitas por categoria
- `GET /v1/reports/expenses-by-category` - Despesas por categoria

### Autenticação
- `POST /v1/identity/register` - Registrar novo usuário
- `POST /v1/identity/login` - Realizar login
- `POST /v1/identity/logout` - Realizar logout

## 🗄️ Modelo de Dados

### Principais Entidades

- **Category**: Categorias de transações
- **Transaction**: Transações financeiras (receitas/despesas)
- **Product**: Produtos do catálogo
- **Order**: Pedidos de compra
- **Voucher**: Cupons de desconto
- **Profile**: Perfis de usuário
- **User**: Usuários do sistema (ASP.NET Identity)

## 🔐 Segurança

- Autenticação baseada em ASP.NET Identity
- Autorização por roles
- CORS configurado para Blazor WebAssembly
- Proteção de endpoints com `[Authorize]`
- Comunicação segura via HTTPS

## 🌐 CORS

O projeto está configurado com política CORS chamada "wasm" para permitir comunicação entre o frontend Blazor e a API.

## 📦 Migrações do Banco de Dados

O projeto inclui as seguintes migrações:
- `first` - Estrutura inicial
- `identity` - Sistema de identidade
- `Voucher` - Sistema de vouchers
- `OrdersProducts` - Relação pedidos e produtos
- `Profiles` - Sistema de perfis

## 🤝 Contribuindo

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request
