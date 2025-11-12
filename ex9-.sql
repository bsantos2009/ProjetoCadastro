CREATE DATABASE dbBanco;
USE dbBanco;
CREATE TABLE tbbanco(
Codigo int primary key,
Nome varchar(50) not null);

CREATE TABLE tbagencia(
CodBanco int,
NumeroAgencia int primary key,
Endereco varchar(50) not null,
foreign key (CodBanco) references tbbanco(Codigo));

CREATE TABLE tbconta(
NumeroConta int primary key,
Saldo decimal(7,2) null,
TipoConta smallint null,
NumAgencia int,
foreign key (NumAgencia) references tbagencia(NumeroAgencia));

CREATE TABLE tbcliente(
CPF bigint primary key,
Nome varchar(50) not null,
Sexo char(1) not null,
Endereco varchar(50) not null);

CREATE TABLE tbtelefone_cliente(
CPF bigint,
Telefone int primary key,
foreign key (CPF) references tbcliente(CPF));

CREATE TABLE tbhistorico(
CPF bigint,
NumeroConta int,
DataInicio date,
primary key (CPF, NumeroConta),
foreign key(CPF) references tbcliente(CPF),
foreign key(NumeroConta) references tbconta(NumeroConta));

INSERT into tbbanco (Codigo, Nome) values(1, 'Banco do Brasil'),
(104, 'Caixa Economica Federal'),
(801, 'Banco Escola');

INSERT INTO tbagencia (CodBanco, NumeroAgencia, Endereco) values(1, 123, 'Av Paulista, 78'),
(104, 159, 'Rua Liberdade, 124'),
(801, 401, 'Rua vinte três, 23'),
(801, 485, 'Av Marechal, 68');

INSERT INTO tbcliente (CPF, Nome, Sexo, Endereco) values(12345678910, 'Enildo', 'M', 'Rua Grande, 75'),
(12345678911, 'Astrogildo', 'M', 'Rua Pequena, 789'),
(12345678912, 'Monica', 'F', 'Av Larga, 148'),
(12345678913, 'Cascão', 'M', 'Av Principal, 369');

INSERT INTO tbconta (NumeroConta, Saldo, TipoConta, NumAgencia) values(9876, 456.05, 1, 123),
(9877, 321.00, 1, 123),
(9878, 100.00, 2, 485),
(9879, 5589.48, 1, 401);

INSERT INTO tbhistorico (CPF, NumeroConta, DataInicio) values(12345678910, 9876, '2001-04-15'),
(12345678911, 9877, "2001-03-10"),
(12345678912, 9878, "2021-03-11"),
(12345678913, 9879, "2000-07-05");

INSERT INTO tbtelefone_cliente (CPF, Telefone) values(12345678910, 912345678),
(12345678911, 912345679),
(12345678912, 912345680),
(12345678913, 912345681);

ALTER TABLE tbcliente ADD COLUMN email varchar(100); 
describe tbcliente;

SELECT CPF, Endereco FROM tbcliente WHERE Nome = "Monica";

describe tbagencia;
SELECT NumeroAgencia, Endereco FROM tbagencia WHERE CodBanco = 801;

SELECT *FROM tbcliente WHERE Sexo = "M";
