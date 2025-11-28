# SimplePDV

Sistema de ponto de venda desenvolvido em .NET 9 com interface desktop e API REST.

## Tecnologias Utilizadas

**Backend**
- ASP.NET Core Web API (.NET 9)
- Entity Framework Core 9.0
- SQL Server
- Clean Architecture

**Frontend**
- WPF (.NET 9)
- MVVM Pattern
- SQLite para operação offline

## Funcionalidades

O sistema possui duas partes principais: a API backend e a aplicação desktop.

**API Backend:**
- Gerenciamento completo de produtos (cadastro, edição, exclusão)
- Registro e consulta de vendas
- Controle automático de estoque
- Sistema de autenticação
- Documentação Swagger

**Aplicação Desktop:**
- Interface para realizar vendas
- Cadastro e consulta de produtos
- Funciona offline com banco SQLite local
- Sincroniza vendas com o servidor quando conectado

## Estrutura do Projeto

```
SimplePDV/
├── src/
│   ├── Backend/
│   │   ├── SimplePDV.Domain/          # Entidades e interfaces
│   │   ├── SimplePDV.Application/     # Serviços e DTOs
│   │   ├── SimplePDV.Infrastructure/  # Acesso a dados e repositórios
│   │   └── SimplePDV.API/             # Controllers e configuração da API
│   └── Client/
│       └── SimplePDV.WPF/             # Aplicação desktop
```

## Como Executar

**Requisitos:**
- .NET 9 SDK
- SQL Server (LocalDB ou Express)
- Visual Studio 2022 (opcional)

**1. Clonar o repositório**

```bash
git clone https://github.com/gb-araujo/simplepdv.git
cd SimplePDV
```

**2. Configurar o banco de dados**

Edite o arquivo `src/Backend/SimplePDV.API/appsettings.json` com sua connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SimplePDV;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**3. Criar o banco de dados**

No diretório `src/Backend/SimplePDV.API`:

```powershell
dotnet ef database update --project ../SimplePDV.Infrastructure
```

**4. Executar a API**

```powershell
cd src\Backend\SimplePDV.API
dotnet run
```

A API estará disponível em `https://localhost:7000` e a documentação Swagger em `https://localhost:7000/swagger`

**5. Executar a aplicação desktop**

```powershell
cd src\Client\SimplePDV.WPF
dotnet run
```

**Credenciais padrão:**
- Login: `admin`
- Senha: `admin123`

## Principais Endpoints

**Produtos**
- `GET /api/produtos` - Lista produtos
- `GET /api/produtos/{id}` - Busca por ID
- `POST /api/produtos` - Criar produto
- `PUT /api/produtos/{id}` - Atualizar produto
- `DELETE /api/produtos/{id}` - Deletar produto

**Vendas**
- `GET /api/vendas` - Lista vendas
- `POST /api/vendas` - Registrar venda
- `GET /api/vendas/nao-sincronizadas` - Vendas pendentes

**Usuários**
- `POST /api/usuarios/login` - Autenticação
- `GET /api/usuarios` - Lista usuários

**Estoque**
- `GET /api/estoque/produto/{id}` - Movimentações
- `POST /api/estoque/movimento` - Movimento manual

## Sincronização Offline

O aplicativo desktop funciona mesmo sem conexão com o servidor. As vendas são salvas localmente no SQLite e enviadas automaticamente quando a conexão é restabelecida.

## Licença

Projeto de código aberto para fins educacionais.
