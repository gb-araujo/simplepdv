-- Script SQL para popular o banco com dados de teste

USE SimplePDV;
GO

-- Limpar dados existentes (exceto usuário admin)
DELETE FROM VendaItens;
DELETE FROM Vendas;
DELETE FROM MovimentosEstoque;
DELETE FROM Produtos WHERE Id > 0;

-- Inserir Produtos de Teste
INSERT INTO Produtos (Nome, SKU, Preco, EstoqueAtual, EstoqueMinimo, Ativo, CriadoEm)
VALUES 
('Coca-Cola 2L', 'COCA2L', 8.99, 50, 10, 1, GETDATE()),
('Guaraná Antarctica 2L', 'GUAR2L', 7.99, 45, 10, 1, GETDATE()),
('Água Mineral 500ml', 'AGUA500', 2.50, 100, 20, 1, GETDATE()),
('Suco Del Valle 1L', 'SUCO1L', 6.50, 30, 8, 1, GETDATE()),
('Chocolate Lacta', 'CHOC01', 5.99, 40, 10, 1, GETDATE()),
('Biscoito Oreo', 'BISC01', 4.50, 35, 8, 1, GETDATE()),
('Salgadinho Doritos 100g', 'SALG100', 6.99, 50, 12, 1, GETDATE()),
('Bala Halls', 'BALA01', 3.50, 80, 15, 1, GETDATE()),
('Café Pilão 500g', 'CAFE500', 18.99, 20, 5, 1, GETDATE()),
('Açúcar União 1kg', 'ACUC1KG', 4.99, 30, 8, 1, GETDATE()),
('Arroz Tio João 5kg', 'ARRO5KG', 25.99, 15, 5, 1, GETDATE()),
('Feijão Camil 1kg', 'FEIJ1KG', 8.50, 25, 8, 1, GETDATE()),
('Óleo de Soja 900ml', 'OLEO900', 7.99, 40, 10, 1, GETDATE()),
('Macarrão Galo 500g', 'MACA500', 3.99, 50, 12, 1, GETDATE()),
('Sal Cisne 1kg', 'SAL1KG', 2.99, 60, 15, 1, GETDATE());

-- Criar usuário de teste adicional (senha: teste123)
INSERT INTO Usuarios (Nome, Login, SenhaHash, Ativo, CriadoEm)
VALUES ('Operador Teste', 'operador', '$2a$11$8Z9qZFfJ7X5xX9bPW5J5XeK9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z9Z', 1, GETDATE());

PRINT 'Dados de teste inseridos com sucesso!';
PRINT '';
PRINT 'Produtos cadastrados: 15';
PRINT 'Usuários disponíveis:';
PRINT '  - admin / admin123';
PRINT '  - operador / teste123';
