CREATE TABLE Cliente
(
Id UNIQUEIDENTIFIER NOT NULL,
CI int NOT NULL,
Saldo int,
Cantidad int,
Fecha datetime NOT NULL,
Operacion int NOT NULL
)

INSERT INTO Cliente 
VALUES(NEWID(),21344,250,250,GETDATE(),1),
(NEWID(),21314,12,12,GETDATE(),1),
(NEWID(),345433,32,32,GETDATE(),1),
(NEWID(),213453,250,250,GETDATE(),1),
(NEWID(),213453,350,100,GETDATE(),1)

SELECT * FROM Cliente

SELECT TOP 1 (Saldo) FROM Cliente WHERE CI=213453 ORDER BY Fecha DESC

CREATE PROCEDURE sp_extracto
@ci int
AS
SELECT CI,Saldo,Fecha,Operacion FROM Cliente WHERE CI=@ci
GO

CREATE PROCEDURE sp_deposito
@ci int,
@cantidad int
AS
INSERT INTO Cliente VALUES(NEWID(),@ci,(SELECT TOP 1 (Saldo) FROM Cliente WHERE CI=@ci ORDER BY Fecha DESC)+@cantidad,@cantidad,GETDATE(),2)
GO

CREATE PROCEDURE sp_retiro
@ci int,
@cantidad int
AS
INSERT INTO Cliente VALUES(NEWID(),@ci,(SELECT TOP 1 (Saldo) FROM Cliente WHERE CI=@ci ORDER BY Fecha DESC)-@cantidad,@cantidad,GETDATE(),2)
GO

CREATE PROCEDURE sp_saldo
@ci int
AS
SELECT TOP 1 (Saldo) FROM Cliente WHERE CI=@ci ORDER BY Fecha DESC
GO