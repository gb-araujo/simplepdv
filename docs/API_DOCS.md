# API Endpoints Documentation

## Base URL
`https://localhost:7000/api`

---

## Autenticação

### POST /usuarios/login
Autentica um usuário no sistema.

**Request Body:**
```json
{
  "login": "admin",
  "senha": "admin123"
}
```

**Response (200 OK):**
```json
{
  "token": "YWRtaW46MTczMjc5MTIzMDAwMDAwMDAw",
  "usuario": {
    "id": 1,
    "nome": "Administrador",
    "login": "admin",
    "ativo": true
  }
}
```

---

## Produtos

### GET /produtos
Lista todos os produtos cadastrados.

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "nome": "Coca-Cola 2L",
    "sku": "COCA2L",
    "preco": 8.99,
    "estoqueAtual": 50,
    "estoqueMinimo": 10,
    "ativo": true
  }
]
```

### GET /produtos/{id}
Busca um produto específico por ID.

### GET /produtos/sku/{sku}
Busca um produto por SKU.

### GET /produtos/ativos
Lista apenas produtos ativos.

### GET /produtos/estoque-baixo
Lista produtos com estoque abaixo do mínimo.

### POST /produtos
Cria um novo produto.

**Request Body:**
```json
{
  "nome": "Produto Exemplo",
  "sku": "PROD001",
  "preco": 15.90,
  "estoqueAtual": 100,
  "estoqueMinimo": 20
}
```

### PUT /produtos/{id}
Atualiza um produto existente.

**Request Body:**
```json
{
  "nome": "Produto Atualizado",
  "preco": 19.90,
  "estoqueMinimo": 15,
  "ativo": true
}
```

### DELETE /produtos/{id}
Remove um produto.

---

## Vendas

### GET /vendas
Lista todas as vendas.

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "usuarioId": 1,
    "usuarioNome": "Administrador",
    "dataVenda": "2024-11-28T10:30:00",
    "valorTotal": 26.97,
    "formaPagamento": 1,
    "itens": [
      {
        "id": 1,
        "produtoId": 1,
        "produtoNome": "Coca-Cola 2L",
        "quantidade": 3,
        "precoUnitario": 8.99,
        "subtotal": 26.97
      }
    ]
  }
]
```

### GET /vendas/{id}
Busca uma venda específica com seus itens.

### GET /vendas/periodo?dataInicio={date}&dataFim={date}
Lista vendas em um período específico.

**Exemplo:**
```
GET /vendas/periodo?dataInicio=2024-11-01&dataFim=2024-11-30
```

### GET /vendas/nao-sincronizadas
Lista vendas que ainda não foram sincronizadas (modo offline).

### POST /vendas
Registra uma nova venda.

**Request Body:**
```json
{
  "usuarioId": 1,
  "formaPagamento": 1,
  "itens": [
    {
      "produtoId": 1,
      "quantidade": 2
    },
    {
      "produtoId": 3,
      "quantidade": 1
    }
  ]
}
```

**Formas de Pagamento:**
- 1: Dinheiro
- 2: Cartão Débito
- 3: Cartão Crédito
- 4: PIX
- 5: Outros

### PATCH /vendas/{id}/sincronizar
Marca uma venda como sincronizada.

---

## Estoque

### GET /estoque/produto/{produtoId}
Lista todos os movimentos de estoque de um produto.

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "produtoId": 1,
    "produtoNome": "Coca-Cola 2L",
    "usuarioId": 1,
    "usuarioNome": "Administrador",
    "tipo": 1,
    "quantidade": 50,
    "estoqueAnterior": 0,
    "estoqueNovo": 50,
    "observacao": "Entrada inicial",
    "dataMovimento": "2024-11-28T08:00:00"
  }
]
```

### GET /estoque/periodo?dataInicio={date}&dataFim={date}
Lista movimentações de estoque em um período.

### POST /estoque/movimento
Registra um movimento manual de estoque.

**Request Body:**
```json
{
  "produtoId": 1,
  "usuarioId": 1,
  "tipo": 1,
  "quantidade": 50,
  "observacao": "Reposição de estoque"
}
```

**Tipos de Movimento:**
- 1: Entrada
- 2: Saída
- 3: Ajuste Manual
- 4: Venda (automático)

---

## Usuários

### GET /usuarios
Lista todos os usuários.

### GET /usuarios/{id}
Busca um usuário por ID.

### POST /usuarios
Cria um novo usuário.

**Request Body:**
```json
{
  "nome": "Novo Operador",
  "login": "operador",
  "senha": "senha123"
}
```

### PUT /usuarios/{id}/senha
Altera a senha de um usuário.

**Request Body:**
```json
"nova_senha_aqui"
```

---

## Códigos de Status HTTP

- **200 OK** - Requisição bem-sucedida
- **201 Created** - Recurso criado com sucesso
- **204 No Content** - Requisição bem-sucedida sem conteúdo
- **400 Bad Request** - Dados inválidos
- **401 Unauthorized** - Não autenticado
- **404 Not Found** - Recurso não encontrado
- **500 Internal Server Error** - Erro no servidor

---

## Exemplo de Uso com cURL

### Login
```bash
curl -X POST https://localhost:7000/api/usuarios/login \
  -H "Content-Type: application/json" \
  -d '{"login":"admin","senha":"admin123"}'
```

### Criar Produto
```bash
curl -X POST https://localhost:7000/api/produtos \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Produto Teste",
    "sku": "TEST001",
    "preco": 10.50,
    "estoqueAtual": 100,
    "estoqueMinimo": 10
  }'
```

### Registrar Venda
```bash
curl -X POST https://localhost:7000/api/vendas \
  -H "Content-Type: application/json" \
  -d '{
    "usuarioId": 1,
    "formaPagamento": 1,
    "itens": [
      {"produtoId": 1, "quantidade": 2}
    ]
  }'
```
