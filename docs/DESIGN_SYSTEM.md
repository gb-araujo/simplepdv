# SimplePDV - Guia de Design System

## Paleta de Cores Light Mode

### Cores Principais
- **Background Geral**: `#F7F7F7` - Cinza muito claro
- **Cards/Caixas**: `#FFFFFF` - Branco puro
- **Bordas**: `#E2E2E2` - Cinza claro
- **Azul Primário**: `#0066FF` - Azul vibrante
- **Azul Hover**: `#0052CC` - Azul escuro

### Cores de Texto
- **Texto Primário**: `#111111` - Preto/Grafite
- **Texto Secundário**: `#555555` - Cinza médio

### Cores de Status
- **Sucesso**: `#10B981` - Verde
- **Aviso**: `#F59E0B` - Laranja
- **Erro**: `#EF4444` - Vermelho

## Tipografia

### Família de Fontes
- Principal: **Segoe UI** (fallback: Inter, Roboto)

### Tamanhos e Pesos
- **Título Principal**: 28px, SemiBold
- **Subtítulo**: 18px, Medium
- **Texto Normal**: 14px, Regular
- **Texto Secundário**: 13px, Regular
- **Labels**: 13px, Medium

## Componentes

### Cards
- Background: Branco
- Border: 1px solid #E2E2E2
- Border Radius: 8px
- Padding: 20px
- Shadow: 0 2px 8px rgba(0,0,0,0.06)

### Botões

#### Botão Primário (Azul)
- Background: #0066FF
- Foreground: White
- Border Radius: 6px
- Padding: 20px (horizontal) × 10px (vertical)
- Hover: #0052CC

#### Botão Secundário
- Background: Transparent
- Border: 1px solid #E2E2E2
- Hover Background: #F7F7F7

#### Botão Sucesso (Verde)
- Background: #10B981
- Foreground: White

#### Botão Perigo (Vermelho)
- Background: #EF4444
- Foreground: White

### Inputs (TextBox)
- Background: White
- Border: 1px solid #E2E2E2
- Border Radius: 6px
- Padding: 12px × 10px
- Focus Border: #0066FF

### DataGrid
- Background: White
- Header Background: #FAFAFA
- Row Height: 48px
- Alternating Row: #FAFAFA
- Selected Row: #E6F2FF
- No grid lines
- Border: 1px solid #E2E2E2

## Espaçamento

### Margins Padrão
- Seção grande: 24px
- Entre cards: 12px
- Entre elementos: 8px
- Interno (padding): 16-20px

## Layouts das Telas

### 1. Tela de Venda (PDV)
```
┌─────────────────────────────────┬────────────────┐
│ Ponto de Venda                  │ Carrinho       │
│                                 │                │
│ [Campo de Busca SKU]  [Buscar]  │ • Item 1       │
│                                 │ • Item 2       │
│ ┌─────────────────────────────┐ │ • Item 3       │
│ │ Lista de Produtos           │ │                │
│ │ • Produto A - R$ 10,00      │ │ TOTAL          │
│ │ • Produto B - R$ 15,00      │ │ R$ 100,00      │
│ │ • Produto C - R$ 20,00      │ │                │
│ └─────────────────────────────┘ │ [💵][💳][💳][📱]│
│                                 │                │
│                                 │ [Finalizar]    │
└─────────────────────────────────┴────────────────┘
```

### 2. Tela de Produtos
```
┌──────────────────────────────────────────────────┐
│ Produtos                      [+ Adicionar]      │
│                                                  │
│ [Campo de Busca]  [Filtros]                     │
│                                                  │
│ ┌──────────┐ ┌──────────┐ ┌──────────┐         │
│ │ Produto  │ │ Produto  │ │ Produto  │         │
│ │ SKU:...  │ │ SKU:...  │ │ SKU:...  │         │
│ │ R$ 10,00 │ │ R$ 15,00 │ │ R$ 20,00 │         │
│ │ [Editar] │ │ [Editar] │ │ [Editar] │         │
│ └──────────┘ └──────────┘ └──────────┘         │
└──────────────────────────────────────────────────┘
```

### 3. Tela de Relatórios
```
┌──────────────────────────────────────────────────┐
│ Relatórios                                       │
│                                                  │
│ ┌─────────┐ ┌─────────┐ ┌─────────┐           │
│ │ Vendas  │ │ Ticket  │ │ Lucro   │           │
│ │ do Dia  │ │ Médio   │ │ Mensal  │           │
│ │ R$ 1.5K │ │ R$ 45   │ │ R$ 5K   │           │
│ └─────────┘ └─────────┘ └─────────┘           │
│                                                  │
│ ┌─────────────────────────────────────────────┐ │
│ │ Gráfico de Vendas                           │ │
│ │                                             │ │
│ └─────────────────────────────────────────────┘ │
└──────────────────────────────────────────────────┘
```

### 4. Tela de Configurações
```
┌──────────────────────────────────────────────────┐
│ Configurações                                    │
│                                                  │
│ ┌─────────────────────────────────────────────┐ │
│ │ Informações da Empresa                      │ │
│ │ Nome: [_________________]                   │ │
│ │ CNPJ: [_________________]                   │ │
│ └─────────────────────────────────────────────┘ │
│                                                  │
│ ┌─────────────────────────────────────────────┐ │
│ │ Impressoras                                 │ │
│ │ Cupom Fiscal: [___________] [Configurar]   │ │
│ └─────────────────────────────────────────────┘ │
└──────────────────────────────────────────────────┘
```

## Ícones

Usar ícones simples, estilo outline (não preenchidos):
- **Busca**: 🔍
- **Adicionar**: ➕
- **Editar**: ✏️
- **Excluir/Remover**: ✕
- **Dinheiro**: 💵
- **Cartão**: 💳
- **PIX**: 📱
- **Configurações**: ⚙️
- **Relatórios**: 📊

## Princípios de Design

1. **Minimalismo**: Menos é mais. Evitar poluição visual.
2. **Clareza**: Hierarquia visual clara com tamanhos e pesos diferentes.
3. **Consistência**: Usar os mesmos estilos em todas as telas.
4. **Respiração**: Espaçamento generoso entre elementos.
5. **Feedback Visual**: Hover states, estados de foco claros.
6. **Acessibilidade**: Alto contraste de texto, botões grandes o suficiente.

## Referências de Inspiração

- Vercel Dashboard (light mode)
- Stripe Dashboard
- Linear App
- Tailwind UI Components
- Material Design 3 (Light Theme)
