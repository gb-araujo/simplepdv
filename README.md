# SimplePDV 🛒

> Sistema de ponto de venda para pequenos comércios, desenvolvido como projeto de estudos em .NET 9

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## 📋 Sobre o Projeto

SimplePDV é um sistema completo de ponto de venda que comecei a desenvolver para aprender mais sobre arquitetura limpa e desenvolvimento full-stack com .NET. O projeto ainda está em desenvolvimento ativo, mas já possui as funcionalidades essenciais para um PDV funcional.

**Status:** 🚧 Em desenvolvimento

### O que já funciona:
- ✅ Cadastro de produtos com SKU único
- ✅ Realização de vendas (dinheiro, débito, crédito, PIX)
- ✅ Controle básico de estoque
- ✅ Sistema de login simples
- ✅ API REST documentada com Swagger
- ✅ Interface desktop em WPF

### O que ainda falta:
- ⏳ Relatórios de vendas e estoque
- ⏳ Dashboard com gráficos
- ⏳ Impressão de cupom fiscal
- ⏳ Backup automático
- ⏳ Melhorar tratamento de erros
- ⏳ Testes unitários

## 🚀 Tecnologias

**Backend**
- ASP.NET Core Web API (.NET 9)
- Entity Framework Core 9.0
- SQL Server LocalDB
- Clean Architecture (tentando seguir as boas práticas)

**Frontend**
- WPF (.NET 9)
- MVVM Pattern com CommunityToolkit.Mvvm
- Design minimalista inspirado em Material Design

## 📁 Estrutura

```
SimplePDV/
├── src/
│   ├── Backend/
│   │   ├── SimplePDV.Domain/          # Entidades de negócio
│   │   ├── SimplePDV.Application/     # Regras de negócio
│   │   ├── SimplePDV.Infrastructure/  # Persistência de dados
│   │   └── SimplePDV.API/             # Endpoints REST
│   └── Client/
│       └── SimplePDV.WPF/             # Aplicação desktop
```

## ⚙️ Como Rodar

**Pré-requisitos:**
- .NET 9 SDK ([Download aqui](https://dotnet.microsoft.com/download))
- SQL Server (LocalDB vem com o Visual Studio)

### Instalação

**1. Clone o repositório**

```bash
git clone https://github.com/gb-araujo/simplepdv.git
cd simplepdv
```

**2. Configure o banco de dados**

A connection string padrão já está configurada para LocalDB. Se precisar mudar, edite `src/Backend/SimplePDV.API/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SimplePDV;Trusted_Connection=True;"
}
```

**3. Crie o banco**

```powershell
cd src\Backend\SimplePDV.API
dotnet ef database update --project ..\SimplePDV.Infrastructure
```

Isso vai criar o banco e popular com dados iniciais (usuário admin e alguns produtos de exemplo).

**4. Rode a API**

```powershell
# Ainda em src/Backend/SimplePDV.API
dotnet run
```

A API vai subir em `https://localhost:7000`. Abra o Swagger em `https://localhost:7000/swagger` pra testar os endpoints.

**5. Rode a aplicação desktop**

Em outro terminal:

```powershell
cd src\Client\SimplePDV.WPF
dotnet run
```

**Login padrão:**
- Usuário: `admin`
- Senha: `admin123`

## 📚 API Endpoints

A documentação completa está disponível no Swagger quando você roda a API.

**Principais recursos:**

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/produtos` | Lista todos os produtos |
| GET | `/api/produtos/sku/{sku}` | Busca produto por SKU |
| POST | `/api/produtos` | Cria novo produto |
| PUT | `/api/produtos/{id}` | Atualiza produto |
| DELETE | `/api/produtos/{id}` | Remove produto |
| POST | `/api/vendas` | Registra nova venda |
| GET | `/api/vendas` | Lista vendas realizadas |
| POST | `/api/usuarios/login` | Faz login |

## 🤔 Decisões de Design

Algumas escolhas que fiz durante o desenvolvimento:

- **Clean Architecture**: Separei bem as camadas pra facilitar manutenção e testes (quando eu fizer eles 😅)
- **MVVM no WPF**: Usei o CommunityToolkit.Mvvm que simplifica muito o binding e comandos
- **EF Core Code-First**: Preferi modelar no código e gerar o banco, é mais fácil de versionar
- **Inativação vs Exclusão**: Produtos não são deletados, apenas inativados, pra manter histórico de vendas

## 🐛 Problemas Conhecidos

- [ ] A sincronização offline ainda não está implementada (tem no código mas não funciona)
- [ ] Não tem validação de CPF/CNPJ no cadastro
- [ ] A interface trava um pouco em operações longas (preciso adicionar async melhor)
- [ ] Falta feedback visual quando salva/deleta algo

## 🛣️ Roadmap

Próximas features que pretendo implementar:

- [ ] Testes unitários (Domain e Application)
- [ ] Dashboard com gráficos de vendas
- [ ] Relatório de produtos mais vendidos
- [ ] Backup automático do banco
- [ ] Impressão de cupom
- [ ] Sistema de permissões (vendedor, gerente, admin)
- [ ] Suporte a múltiplas lojas

## 📝 Aprendizados

Este é meu primeiro projeto "grande" com .NET 9 e estou usando pra aprender:

- Arquitetura limpa na prática
- Padrões de projeto (Repository, Unit of Work)
- MVVM com WPF moderno
- Entity Framework Core avançado
- API REST com boas práticas

Qualquer sugestão ou crítica construtiva é muito bem-vinda!

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

**Desenvolvido por Gabriel Araújo** | [GitHub](https://github.com/gb-araujo)

## Sincronização Offline

O aplicativo desktop funciona mesmo sem conexão com o servidor. As vendas são salvas localmente no SQLite e enviadas automaticamente quando a conexão é restabelecida.

## Licença

Projeto de código aberto para fins educacionais.
