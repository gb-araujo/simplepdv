# SimplePDV 🛒

> Sistema simples de ponto de venda desenvolvido para praticar .NET e aprender desenvolvimento full-stack

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## 📋 Sobre

Meu primeiro projeto full-stack com .NET! Criei esse PDV básico pra aplicar o que aprendi sobre:
- API REST com ASP.NET Core
- Entity Framework pra trabalhar com banco de dados
- WPF pra interface desktop
- Arquitetura em camadas (Domain, Application, Infrastructure)

É um projeto de estudos, então tem bugs e várias coisas pra melhorar, mas tá funcionando! 😊

## ✨ Funcionalidades

**O que dá pra fazer:**
- Cadastrar e listar produtos
- Fazer vendas escolhendo produtos e forma de pagamento
- Consultar estoque básico
- Login simples de usuário
- Tudo funciona local com SQL Server

**Limitações conhecidas:**
- Sem relatórios ainda
- Interface pode travar em algumas operações
- Não tem impressão de cupom
- Validações são bem básicas
- Preciso adicionar mais tratamento de erros

## 🛠️ Tecnologias

- **Backend:** ASP.NET Core Web API + Entity Framework Core
- **Banco:** SQL Server LocalDB
- **Frontend:** WPF com MVVM
- **Arquitetura:** Camadas separadas (tentando organizar bem o código)

## 📁 Estrutura do Projeto

Organizei o código em camadas (aprendi isso estudando Clean Architecture):

```
SimplePDV/
├── src/
│   ├── Backend/
│   │   ├── SimplePDV.Domain/          # Minhas classes principais (Produto, Venda, etc)
│   │   ├── SimplePDV.Application/     # Lógica de negócio
│   │   ├── SimplePDV.Infrastructure/  # Banco de dados com EF Core
│   │   └── SimplePDV.API/             # Controllers da API REST
│   └── Client/
│       └── SimplePDV.WPF/             # Interface desktop
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

Isso cria o banco e já adiciona um usuário admin e alguns produtos de teste pra você brincar.

**4. Rode a API**

```powershell
# Ainda na pasta src/Backend/SimplePDV.API
dotnet run
```

A API vai subir em `https://localhost:7000`. Você pode testar no navegador abrindo `https://localhost:7000/swagger`

**5. Rode a aplicação desktop** (em outro terminal)

```powershell
cd src\Client\SimplePDV.WPF
dotnet run
```

**Pra entrar:**
- Usuário: `admin`
- Senha: `admin123`

## 🔌 API Endpoints

A API tem os principais recursos que você precisa:

**Produtos:**
- `GET /api/produtos` - Lista todos
- `GET /api/produtos/sku/{sku}` - Busca por código
- `POST /api/produtos` - Cadastra novo
- `PUT /api/produtos/{id}` - Atualiza
- `DELETE /api/produtos/{id}` - Inativa produto

**Vendas:**
- `POST /api/vendas` - Registra venda
- `GET /api/vendas` - Lista vendas

**Login:**
- `POST /api/usuarios/login` - Faz login

Tem mais endpoints, mas esses são os principais. Dá pra ver todos no Swagger quando rodar a API!

## 🤔 Decisões de Design

## 💭 Por que fiz isso?

Escolhas que fiz enquanto desenvolvia:

**Separei em camadas (Domain, Application, Infrastructure):**
- Vi que é assim que projetos maiores funcionam
- Facilita quando preciso mudar algo
- Ainda tô aprendendo a fazer isso direito

**Entity Framework Code-First:**
- Achei mais fácil do que criar tabelas manualmente no SQL
- Migrations são legais pra versionar mudanças no banco
- Aprendi bastante sobre relacionamentos

**Inativação em vez de Delete:**
- Se eu deletar um produto, as vendas antigas ficam quebradas
- Então só marco como "inativo" e ele some da lista
- Foi uma dica que vi num vídeo e fez sentido

## 🐛 Bugs e Limitações

Coisas que eu sei que não tão boas:

- A interface às vezes trava (preciso usar mais async/await)
- Não valida direito os campos (aceita preço negativo, por exemplo)
- Erros só aparecem no console, não tem mensagem pro usuário
- A "sincronização offline" que tem no código não funciona
- Design tá básico mas funcional

## 🎯 Próximos Passos

O que quero adicionar/melhorar:

1. **Curto prazo:**
   - Mensagens de erro mais claras
   - Validar campos antes de salvar
   - Loading quando tá processando

2. **Médio prazo:**
   - Relatório simples de vendas
   - Gráfico mostrando vendas do dia
   - Impressão de recibo

3. **Longo prazo:**
   - Aprender a fazer testes
   - Melhorar a segurança (usar JWT de verdade)
   - Deixar a interface mais bonita

## 📚 O que aprendi

Esse projeto me ensinou bastante:

- Como criar uma API REST do zero
- Trabalhar com Entity Framework e banco de dados
- MVVM no WPF (confesso que deu trabalho entender)
- Git e como organizar commits
- Ler documentação (muito!)

Ainda tenho muito o que aprender, mas já deu pra pegar a base. Se você tá começando também, qualquer dúvida pode abrir issue que tento ajudar!

## 📄 Licença

MIT License - pode usar à vontade pra estudar e modificar.

---

**Feito com ☕ por Gabriel Araújo** | [GitHub](https://github.com/gb-araujo)
