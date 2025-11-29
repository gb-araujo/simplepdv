# 📦 Entregáveis SimplePDV

## ✅ Sistema Completo Criado

### 🏗️ Estrutura do Projeto

```
SimplePDV/
├── SimplePDV.sln                          # Solution principal do Visual Studio
│
├── 📚 Documentação
│   ├── README.md                          # Documentação completa do projeto
│   ├── INICIO_RAPIDO.md                   # Guia de início rápido
│   ├── INSTALACAO.md                      # Instruções detalhadas de instalação
│   ├── API_DOCS.md                        # Documentação completa da API
│   └── ENTREGAVEIS.md                     # Este arquivo
│
├── 🔧 Scripts
│   ├── migrate-database.ps1               # Script para criar banco de dados
│   ├── run-api.ps1                        # Script para executar API
│   ├── run-desktop.ps1                    # Script para executar Desktop
│   └── seed-data.sql                      # Script SQL com dados de teste
│
└── 💻 Código Fonte
    ├── Backend/
    │   ├── SimplePDV.Domain/              # Camada de domínio
    │   │   ├── Entities/                  # Entidades do sistema
    │   │   │   ├── BaseEntity.cs
    │   │   │   ├── Usuario.cs
    │   │   │   ├── Produto.cs
    │   │   │   ├── Venda.cs
    │   │   │   ├── VendaItem.cs
    │   │   │   └── MovimentoEstoque.cs
    │   │   ├── Enums/                     # Enumeradores
    │   │   │   ├── FormaPagamento.cs
    │   │   │   └── TipoMovimento.cs
    │   │   └── Interfaces/                # Interfaces de repositório
    │   │       ├── IRepository.cs
    │   │       ├── IProdutoRepository.cs
    │   │       ├── IVendaRepository.cs
    │   │       ├── IUsuarioRepository.cs
    │   │       └── IMovimentoEstoqueRepository.cs
    │   │
    │   ├── SimplePDV.Application/         # Camada de aplicação
    │   │   ├── DTOs/                      # Data Transfer Objects
    │   │   │   ├── ProdutoDto.cs
    │   │   │   ├── VendaDto.cs
    │   │   │   ├── UsuarioDto.cs
    │   │   │   └── MovimentoEstoqueDto.cs
    │   │   └── Services/                  # Serviços de negócio
    │   │       ├── ProdutoService.cs
    │   │       ├── VendaService.cs
    │   │       ├── UsuarioService.cs
    │   │       └── MovimentoEstoqueService.cs
    │   │
    │   ├── SimplePDV.Infrastructure/      # Camada de infraestrutura
    │   │   ├── Data/
    │   │   │   └── ApplicationDbContext.cs # Contexto EF Core
    │   │   └── Repositories/              # Implementação dos repositórios
    │   │       ├── Repository.cs
    │   │       ├── ProdutoRepository.cs
    │   │       ├── VendaRepository.cs
    │   │       ├── UsuarioRepository.cs
    │   │       └── MovimentoEstoqueRepository.cs
    │   │
    │   └── SimplePDV.API/                 # Web API
    │       ├── Controllers/               # Controllers REST
    │       │   ├── ProdutosController.cs
    │       │   ├── VendasController.cs
    │       │   ├── UsuariosController.cs
    │       │   └── EstoqueController.cs
    │       ├── Program.cs                 # Configuração da API
    │       ├── appsettings.json           # Configurações
    │       └── appsettings.Development.json
    │
    └── Client/
        └── SimplePDV.WPF/                 # Aplicação Desktop WPF
            ├── Data/
            │   └── LocalDbContext.cs      # SQLite local para offline
            ├── Services/                  # Serviços do cliente
            │   ├── ApiService.cs          # Comunicação com API
            │   ├── ProdutoLocalService.cs
            │   ├── VendaLocalService.cs
            │   ├── UsuarioLocalService.cs
            │   └── SincronizacaoService.cs # Sincronização offline
            ├── ViewModels/                # ViewModels MVVM
            │   ├── LoginViewModel.cs
            │   ├── MainViewModel.cs
            │   ├── ProdutosViewModel.cs
            │   ├── VendasViewModel.cs
            │   └── EstoqueViewModel.cs
            ├── Views/                     # Views XAML
            │   ├── LoginWindow.xaml/cs
            │   ├── MainWindow.xaml/cs
            │   ├── VendasView.xaml/cs
            │   ├── ProdutosView.xaml/cs
            │   └── EstoqueView.xaml/cs
            ├── App.xaml/cs                # Aplicação principal
            └── SimplePDV.WPF.csproj
```

---

## 🎯 Funcionalidades Implementadas

### Backend (API)

#### ✅ Gerenciamento de Produtos
- Criar, listar, atualizar e deletar produtos
- Buscar por ID ou SKU
- Listar produtos ativos
- Alertar produtos com estoque baixo

#### ✅ Controle de Vendas
- Registrar vendas com múltiplos itens
- Calcular valor total automaticamente
- Suporte a múltiplas formas de pagamento
- Histórico de vendas por período
- Vendas não sincronizadas (offline)

#### ✅ Gestão de Estoque
- Atualização automática em vendas
- Movimentação manual (entrada/saída/ajuste)
- Histórico completo de movimentações
- Rastreamento de estoque anterior e novo

#### ✅ Autenticação
- Login com usuário e senha
- Hash de senha com BCrypt
- Token de autenticação simples
- Gerenciamento de usuários

#### ✅ Infraestrutura
- Clean Architecture em camadas
- Repository Pattern
- Entity Framework Core 9.0
- SQL Server com migrations
- Swagger UI integrado
- CORS habilitado

---

### Desktop (WPF)

#### ✅ Tela de Login
- Autenticação de usuários
- Validação de credenciais
- Indicador de status (online/offline)

#### ✅ PDV / Tela de Vendas
- Seleção de produtos
- Carrinho de compras
- Cálculo automático de totais
- Seleção de forma de pagamento
- Finalização e cancelamento de vendas
- Atualização de estoque em tempo real

#### ✅ Cadastro de Produtos
- Listagem de produtos
- Busca por SKU
- Visualização de estoque atual
- Filtros e pesquisa

#### ✅ Controle de Estoque
- Visualização de estoque de todos produtos
- Indicador de estoque mínimo
- Status visual de produtos

#### ✅ Modo Offline
- Banco SQLite local
- Operação sem internet
- Armazenamento de vendas não sincronizadas
- Sincronização automática quando online
- Botão manual de sincronização

#### ✅ Arquitetura
- Padrão MVVM
- Dependency Injection
- CommunityToolkit.Mvvm
- ObservableObjects e RelayCommands

---

## 📊 Entidades do Sistema

### Usuario
- Id, Nome, Login, SenhaHash
- Status Ativo/Inativo
- Relacionamentos: Vendas, MovimentosEstoque

### Produto
- Id, Nome, SKU, Preço
- EstoqueAtual, EstoqueMinimo
- Status Ativo/Inativo
- Relacionamentos: VendaItens, MovimentosEstoque

### Venda
- Id, UsuarioId, DataVenda
- ValorTotal, FormaPagamento
- Flag Sincronizado
- Relacionamentos: Usuario, Itens

### VendaItem
- Id, VendaId, ProdutoId
- Quantidade, PrecoUnitario, Subtotal
- Relacionamentos: Venda, Produto

### MovimentoEstoque
- Id, ProdutoId, UsuarioId
- Tipo, Quantidade
- EstoqueAnterior, EstoqueNovo
- Observacao, DataMovimento
- Relacionamentos: Produto, Usuario

---

## 🔌 API Endpoints Implementados

### Produtos
- `GET /api/produtos` - Lista todos
- `GET /api/produtos/{id}` - Por ID
- `GET /api/produtos/sku/{sku}` - Por SKU
- `GET /api/produtos/ativos` - Ativos
- `GET /api/produtos/estoque-baixo` - Estoque baixo
- `POST /api/produtos` - Criar
- `PUT /api/produtos/{id}` - Atualizar
- `DELETE /api/produtos/{id}` - Deletar

### Vendas
- `GET /api/vendas` - Lista todas
- `GET /api/vendas/{id}` - Por ID
- `GET /api/vendas/periodo` - Por período
- `GET /api/vendas/nao-sincronizadas` - Não sincronizadas
- `POST /api/vendas` - Registrar venda
- `PATCH /api/vendas/{id}/sincronizar` - Marcar sincronizada

### Usuários
- `GET /api/usuarios` - Lista todos
- `GET /api/usuarios/{id}` - Por ID
- `POST /api/usuarios` - Criar
- `POST /api/usuarios/login` - Login
- `PUT /api/usuarios/{id}/senha` - Alterar senha

### Estoque
- `GET /api/estoque/produto/{id}` - Movimentos do produto
- `GET /api/estoque/periodo` - Por período
- `POST /api/estoque/movimento` - Registrar movimento

---

## 🛠️ Tecnologias Utilizadas

### Backend
- **.NET 9.0** - Framework
- **ASP.NET Core** - Web API
- **Entity Framework Core 9.0** - ORM
- **SQL Server** - Banco de dados
- **BCrypt.Net-Next** - Hash de senhas
- **Swashbuckle** - Swagger UI

### Desktop
- **.NET 9.0 Windows** - Framework
- **WPF** - Interface gráfica
- **MVVM Toolkit** - CommunityToolkit.Mvvm
- **SQLite** - Banco local
- **Entity Framework Core** - ORM SQLite
- **Dependency Injection** - Microsoft.Extensions

---

## 📝 Scripts e Ferramentas

### PowerShell Scripts
- ✅ `migrate-database.ps1` - Cria banco e aplica migrations
- ✅ `run-api.ps1` - Executa a Web API
- ✅ `run-desktop.ps1` - Executa aplicação WPF

### SQL Scripts
- ✅ `seed-data.sql` - Popula banco com dados de teste

---

## 🎓 Como Usar

### 1. Instalação Inicial
```powershell
cd SimplePDV
.\scripts\migrate-database.ps1
```

### 2. Executar Backend
```powershell
.\scripts\run-api.ps1
```
API: https://localhost:7000  
Swagger: https://localhost:7000/swagger

### 3. Executar Desktop
```powershell
.\scripts\run-desktop.ps1
```
Login: admin / admin123

---

## ✨ Diferenciais do Projeto

1. **Arquitetura Limpa** - Separação clara de responsabilidades
2. **Modo Offline** - Funciona sem internet
3. **Sincronização Inteligente** - Envia dados offline quando conectado
4. **Organizado em Camadas** - Domain, Application, Infrastructure, API
5. **Padrões de Projeto** - Repository, MVVM, DI
6. **Documentação Completa** - README, guias, API docs
7. **Scripts Automatizados** - Facilita setup e execução
8. **Pronto para Produção** - Estrutura escalável

---

## 🚀 Próximas Melhorias Possíveis

- [ ] Implementar JWT real na API
- [ ] Adicionar testes unitários
- [ ] Relatórios de vendas e estoque
- [ ] Dashboard com gráficos
- [ ] Impressão de cupom fiscal
- [ ] Cadastro de clientes
- [ ] Gestão de fornecedores
- [ ] Backup automático
- [ ] Logs de auditoria
- [ ] Múltiplas empresas/lojas

---

## 📞 Informações do Sistema

**Nome:** SimplePDV  
**Versão:** 1.0.0  
**Framework:** .NET 9.0  
**Arquitetura:** Clean Architecture  
**Banco de Dados:** SQL Server (Backend) + SQLite (Cliente)  
**Padrão UI:** MVVM  

**Usuário Padrão:**  
Login: admin  
Senha: admin123

---

**Sistema SimplePDV desenvolvido e entregue com sucesso!** ✅
