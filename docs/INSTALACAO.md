# Guia de Instalação - SimplePDV

## Pré-requisitos

1. **.NET 9 SDK**
   - Download: https://dotnet.microsoft.com/download/dotnet/9.0
   - Verificar instalação: `dotnet --version`

2. **SQL Server**
   - LocalDB (vem com Visual Studio)
   - SQL Server Express: https://www.microsoft.com/sql-server/sql-server-downloads
   - Ou SQL Server completo

3. **Visual Studio 2022** (Opcional, mas recomendado)
   - Community Edition (gratuita): https://visualstudio.microsoft.com/

## Passo a Passo

### 1. Baixar o Projeto

```powershell
git clone https://github.com/seu-usuario/simplepdv.git
cd SimplePDV
```

### 2. Configurar Connection String

Abra o arquivo `src\Backend\SimplePDV.API\appsettings.json` e configure:

**Para SQL Server LocalDB:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SimplePDV;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Para SQL Server Express:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=SimplePDV;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Para SQL Server com usuário/senha:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SimplePDV;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;"
  }
}
```

### 3. Criar o Banco de Dados

Execute o script PowerShell (no diretório raiz):

```powershell
.\scripts\migrate-database.ps1
```

Ou manualmente:

```powershell
cd src\Backend\SimplePDV.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../SimplePDV.API
dotnet ef database update --startup-project ../SimplePDV.API
```

### 4. Executar a API

**Opção 1 - Script:**
```powershell
.\scripts\run-api.ps1
```

**Opção 2 - Manual:**
```powershell
cd src\Backend\SimplePDV.API
dotnet run
```

A API estará em: `https://localhost:7000`

### 5. Executar o Desktop

**Em outro terminal:**

**Opção 1 - Script:**
```powershell
.\scripts\run-desktop.ps1
```

**Opção 2 - Manual:**
```powershell
cd src\Client\SimplePDV.WPF
dotnet run
```

### 6. Fazer Login

```
Login: admin
Senha: admin123
```

## Resolução de Problemas

### Erro: "Cannot connect to SQL Server"

1. Verifique se o SQL Server está rodando
2. Confirme a connection string
3. Teste a conexão com SQL Server Management Studio

### Erro: "A network-related or instance-specific error"

1. Verifique o nome da instância do SQL Server
2. Habilite TCP/IP no SQL Server Configuration Manager
3. Verifique o firewall

### Erro: "Unable to create database"

Execute como administrador:
```powershell
# Deletar banco existente
cd src\Backend\SimplePDV.API
dotnet ef database drop --project ../SimplePDV.Infrastructure --force

# Recriar
dotnet ef database update --project ../SimplePDV.Infrastructure
```

### Banco SQLite Local (Desktop)

O banco local é criado automaticamente em:
```
C:\Users\[SeuUsuario]\AppData\Local\SimplePDV\simplepdv.db
```

Para resetar o banco local, delete este arquivo.

## Testando a API

Acesse o Swagger UI: `https://localhost:7000/swagger`

### Criar um Produto via API:

```bash
POST https://localhost:7000/api/produtos
Content-Type: application/json

{
  "nome": "Produto Teste",
  "sku": "TEST001",
  "preco": 10.50,
  "estoqueAtual": 100,
  "estoqueMinimo": 10
}
```

## Populando Dados de Teste

Execute no SQL Server:

```sql
USE SimplePDV;

-- Inserir produtos de teste
INSERT INTO Produtos (Nome, SKU, Preco, EstoqueAtual, EstoqueMinimo, Ativo, CriadoEm)
VALUES 
('Coca-Cola 2L', 'COCA2L', 8.99, 50, 10, 1, GETDATE()),
('Água Mineral 500ml', 'AGUA500', 2.50, 100, 20, 1, GETDATE()),
('Chocolate Barra', 'CHOC01', 5.99, 30, 5, 1, GETDATE()),
('Salgadinho 100g', 'SALG100', 4.50, 40, 10, 1, GETDATE());
```

## Próximos Passos

- Criar mais usuários via API
- Adicionar produtos pelo desktop
- Realizar vendas
- Testar modo offline (desligar API)
- Sincronizar vendas offline

## Suporte

Para problemas, verifique:
1. Logs da API no console
2. Event Viewer do Windows
3. Arquivo de log do SQL Server
