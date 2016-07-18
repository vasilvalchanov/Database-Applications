USE ATM
GO

CREATE TABLE CardAccounts
(Id int Identity Primary Key,
CardNumber char(10) NOT NULL,
CardPIN char(4) NOT NULL,
CardCash money NOT NULL)
GO

INSERT INTO CardAccounts
VALUES
(0123456789, 8888, 1500),
(1234567890, 1234, 200),
(9876543210, 9889, 5635),
(5566897455, 2488, 1350),
(8778965142, 9076, 350)
GO

Create TABLE TransactionHistory
(Id int IDENTITY PRIMARY KEY,
CardNumber char(10) NOT NULL,
TransactionDate DateTime NOT NULL,
Amount money NOT NULL)