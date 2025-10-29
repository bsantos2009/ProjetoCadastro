CREATE DATABASE dbProjetoCad;

USE dbProjetoCad;

CREATE TABLE tbUsuarios (
Id INT AUTO_INCREMENT PRIMARY KEY,
Nome VARCHAR(55) NOT NULL ,
Email VARCHAR(225) NOT NULL UNIQUE,
Senha VARCHAR(225) NOT NULL
);

CREATE TABLE tbProdutos (
Id INT AUTO_INCREMENT PRIMARY KEY,
Nome VARCHAR(55) NOT NULL,
Descricao VARCHAR(255),
Preco DOUBLE NOT NULL CHECK(Preco >=0) ,
Quantidade INT NOT NULL CHECK(Quantidade >=0)
);

INSERT INTO tbUsuarios (Nome, Email, Senha)
VALUES 
('Bruno Santos', 'bruno@email.com', '1234'),
('Luciene Teodoro', 'luciene@email.com', 'abcd'),
('Caio Silva', 'caio@email.com', 'senha123');

INSERT INTO tbProdutos (Nome, Descricao, Preco, Quantidade)
VALUES
('Mouse Gamer M711 Cobra', 'Mouse com iluminação RGB', 150.00, 10),
('Teclado Mecânico Redragon Lakshmi', 'Teclado com LED Branco', 320.00, 5),
('Monitor LG 24"', 'Monitor Full HD 100Hz', 900.00, 3);

-- Mostrar produtos com estoque abaixo de 5

SELECT * FROM tbProdutos WHERE Quantidade < 5;

-- Select padrão

SELECT * FROM tbUsuarios;
SELECT * FROM tbProdutos;

-- UPDATE

UPDATE tbUsuarios
SET Email = 'bruno.santos@email.com'
WHERE Id = 1;

UPDATE tbProdutos
SET Preco = 280.00, Quantidade = 8
WHERE Nome = 'Teclado Mecânico';

-- DELETE

DELETE FROM tbUsuarios WHERE Id = 3;

DELETE FROM tbProdutos WHERE Nome = 'Mouse Gamer';