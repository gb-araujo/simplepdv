# Como Usar a Collection do Postman

## Importar a Collection

1. Abra o Postman
2. Clique em **Import** (canto superior esquerdo)
3. Selecione o arquivo `SimplePDV.postman_collection.json`
4. Clique em **Import**

## Importar o Environment

1. No Postman, clique no ícone de **Environments** (⚙️ no canto superior direito)
2. Clique em **Import**
3. Selecione o arquivo `SimplePDV.postman_environment.json`
4. Selecione o environment **SimplePDV - Local** no dropdown superior

## Estrutura da Collection

A collection está organizada em 4 pastas principais:

### 1. Produtos
- **GET** Listar Todos os Produtos - `/api/produtos`
- **GET** Listar Produtos Ativos - `/api/produtos/ativos`
- **GET** Buscar Produto por ID - `/api/produtos/{id}`
- **GET** Buscar Produto por SKU - `/api/produtos/sku/{sku}`
- **GET** Listar Produtos com Estoque Baixo - `/api/produtos/estoque-baixo`
- **POST** Criar Produto - `/api/produtos`
- **PUT** Atualizar Produto - `/api/produtos/{id}`
- **DELETE** Inativar Produto - `/api/produtos/{id}`

### 2. Vendas
- **GET** Listar Todas as Vendas - `/api/vendas`
- **GET** Buscar Venda por ID - `/api/vendas/{id}`
- **GET** Vendas por Período - `/api/vendas/periodo?dataInicio&dataFim`
- **GET** Vendas Não Sincronizadas - `/api/vendas/nao-sincronizadas`
- **POST** Criar Venda - `/api/vendas`
- **PATCH** Marcar Venda como Sincronizada - `/api/vendas/{id}/sincronizar`

### 3. Usuários
- **GET** Listar Usuários - `/api/usuarios`
- **GET** Buscar Usuário por ID - `/api/usuarios/{id}`
- **POST** Login - `/api/usuarios/login`
- **POST** Criar Usuário - `/api/usuarios`

### 4. Estoque
- **GET** Movimentações por Produto - `/api/estoque/produto/{id}`
- **GET** Movimentações por Período - `/api/estoque/periodo?dataInicio&dataFim`
- **POST** Registrar Movimento Manual - `/api/estoque/movimento`

## Ordem Recomendada de Testes

### 1. Autenticação
```
POST /api/usuarios/login
Body: { "login": "admin", "senha": "admin123" }
```

### 2. Criar Produto
```
POST /api/produtos
Body: {
  "nome": "Coca-Cola 2L",
  "sku": "COCA2L",
  "preco": 8.50,
  "estoqueAtual": 50,
  "estoqueMinimo": 10
}
```

### 3. Listar Produtos
```
GET /api/produtos/ativos
```

### 4. Criar Venda
```
POST /api/vendas
Body: {
  "usuarioId": 1,
  "formaPagamento": 0,
  "itens": [
    {
      "produtoId": 1,
      "quantidade": 2,
      "precoUnitario": 8.50
    }
  ]
}
```

### 5. Movimento de Estoque
```
POST /api/estoque/movimento
Body: {
  "produtoId": 1,
  "usuarioId": 1,
  "tipo": 0,
  "quantidade": 20,
  "observacao": "Reposição de estoque"
}
```

## Enums Importantes

### FormaPagamento
- `0` - Dinheiro
- `1` - CartaoDebito
- `2` - CartaoCredito
- `3` - PIX
- `4` - Outros

### TipoMovimento
- `0` - Entrada
- `1` - Saida
- `2` - Ajuste
- `3` - Venda

## Variáveis de Environment

A collection usa a variável `{{base_url}}` que está configurada como:
- `http://localhost:5000` (desenvolvimento local)

Você pode alterar no environment conforme necessário.

## Notas Importantes

1. **Inativar Produto**: O endpoint DELETE não exclui fisicamente o produto, apenas marca como inativo (`Ativo = false`)
2. **Estoque Automático**: Ao criar uma venda, o estoque é deduzido automaticamente
3. **Sincronização**: Vendas criadas offline podem ser marcadas como sincronizadas posteriormente
4. **Datas**: Use formato ISO 8601 para datas: `YYYY-MM-DD` ou `YYYY-MM-DDTHH:mm:ss`

## Swagger

A API também possui documentação Swagger disponível em:
```
http://localhost:5000/swagger
```
