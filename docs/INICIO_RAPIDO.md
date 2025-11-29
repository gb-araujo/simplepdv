# 🚀 Início Rápido - SimplePDV

## Passos para Executar

### 1️⃣ Abrir o Projeto
```powershell
cd c:\Users\gabriel\Documents\projetos\SimplePDV
```

### 2️⃣ Criar o Banco de Dados
```powershell
.\scripts\migrate-database.ps1
```

Ou manualmente:
```powershell
cd src\Backend\SimplePDV.API
dotnet ef database update --project ..\SimplePDV.Infrastructure
```

### 3️⃣ Executar a API (Terminal 1)
```powershell
.\scripts\run-api.ps1
```

Aguarde a mensagem: `Now listening on: https://localhost:7000`

### 4️⃣ Executar o Desktop (Terminal 2)
```powershell
.\scripts\run-desktop.ps1
```

### 5️⃣ Fazer Login
```
Login: admin
Senha: admin123
```

---

## 🎯 O Que Foi Criado

### Backend (.NET 9 Web API)
✅ 5 projetos em camadas (Domain, Application, Infrastructure, API)  
✅ Entity Framework Core com SQL Server  
✅ Autenticação com BCrypt  
✅ Endpoints REST completos  
✅ Swagger UI habilitado  

### Desktop (WPF)
✅ Aplicação desktop C# com WPF  
✅ MVVM com CommunityToolkit.Mvvm  
✅ SQLite local para operação offline  
✅ Sincronização automática com backend  
✅ Telas: Login, PDV/Vendas, Produtos, Estoque  

### Funcionalidades
✅ Cadastro de produtos (nome, SKU, preço, estoque)  
✅ Registro de vendas com múltiplos itens  
✅ Controle automático de estoque  
✅ Movimentação manual de estoque  
✅ Autenticação de usuários  
✅ **Modo Offline** - funciona sem internet  
✅ **Sincronização** - envia vendas offline quando online  

---

## 📊 Estrutura do Banco de Dados

### Tabelas Criadas
- `Usuarios` - Usuários do sistema
- `Produtos` - Catálogo de produtos
- `Vendas` - Registro de vendas
- `VendaItens` - Itens de cada venda
- `MovimentosEstoque` - Histórico de movimentações

---

## 🧪 Testar a API

### Swagger UI
Abra: https://localhost:7000/swagger

### Criar Produto via API
```powershell
$body = @{
    nome = "Coca-Cola 2L"
    sku = "COCA2L"
    preco = 8.99
    estoqueAtual = 50
    estoqueMinimo = 10
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7000/api/produtos" `
    -Method POST `
    -Body $body `
    -ContentType "application/json" `
    -SkipCertificateCheck
```

---

## 📱 Usar o Desktop

### Fluxo de Venda
1. **Login** com admin/admin123
2. Clicar em **PDV / Vendas**
3. Selecionar produto e quantidade
4. Clicar em **Adicionar**
5. Escolher forma de pagamento
6. Clicar em **Finalizar Venda**

### Modo Offline
- O desktop funciona sem internet
- Vendas são salvas localmente (SQLite)
- Ao conectar, clique em **Sincronizar**
- Vendas são enviadas ao servidor

---

## 🗂️ Arquivos Importantes

```
SimplePDV/
├── SimplePDV.sln                    # Solution principal
├── README.md                         # Documentação completa
├── INSTALACAO.md                     # Guia de instalação
├── API_DOCS.md                       # Documentação da API
├── scripts/
│   ├── migrate-database.ps1          # Cria o banco
│   ├── run-api.ps1                   # Executa API
│   ├── run-desktop.ps1               # Executa Desktop
│   └── seed-data.sql                 # Dados de teste
└── src/
    ├── Backend/
    │   ├── SimplePDV.Domain/         # Entidades
    │   ├── SimplePDV.Application/    # Lógica de negócio
    │   ├── SimplePDV.Infrastructure/ # EF Core, Repos
    │   └── SimplePDV.API/            # Web API
    └── Client/
        └── SimplePDV.WPF/            # Desktop WPF
```

---

## 🔧 Comandos Úteis

### Recriar Banco de Dados
```powershell
cd src\Backend\SimplePDV.API
dotnet ef database drop --project ..\SimplePDV.Infrastructure --force
dotnet ef database update --project ..\SimplePDV.Infrastructure
```

### Adicionar Nova Migration
```powershell
cd src\Backend\SimplePDV.Infrastructure
dotnet ef migrations add NomeDaMigration --startup-project ..\SimplePDV.API
```

### Compilar Solução Completa
```powershell
dotnet build SimplePDV.sln
```

### Ver Banco SQLite Local (Desktop)
Local: `C:\Users\[Usuario]\AppData\Local\SimplePDV\simplepdv.db`

---

## 📝 Endpoints Principais

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | /api/usuarios/login | Login |
| GET | /api/produtos | Listar produtos |
| POST | /api/produtos | Criar produto |
| GET | /api/vendas | Listar vendas |
| POST | /api/vendas | Registrar venda |
| POST | /api/estoque/movimento | Ajustar estoque |

---

## ❓ Problemas Comuns

### Erro de Conexão SQL
- Verifique se SQL Server está rodando
- Ajuste connection string em `appsettings.json`

### Porta 7000 em Uso
- Altere a porta em `Properties/launchSettings.json`

### Desktop não Conecta na API
- Verifique se API está rodando
- Desktop funciona offline normalmente

---

## 🎓 Próximos Passos

1. ✅ Adicionar mais produtos via API ou Desktop
2. ✅ Realizar vendas no PDV
3. ✅ Testar modo offline (desligar API)
4. ✅ Sincronizar vendas offline
5. ✅ Criar novos usuários
6. ✅ Consultar relatórios de vendas

---

**Sistema SimplePDV criado com sucesso!** 🎉
