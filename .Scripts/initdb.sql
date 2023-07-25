CREATE TABLE Tickets (
	Id SERIAL PRIMARY KEY,
	Title varchar(50) NOT NULL,
	Description varchar(180) NOT NULL,
	Priority integer NOT NULL,
	OpenedAt date NOT NULL,
	FinishedAt date NULL,
	CanceledAt date NULL,
	Reference varchar(32) NULL);

INSERT INTO Tickets
(Title, Description, Priority, OpenedAt, Reference)
VALUES
('Solicitacao 1', 'Lorem ipsum', 0, NOW(), 'a5a547c335b249deae0ae0289654e434'),
('Solicitacao 2', 'Lorem ipsum', 0, NOW(), '6d903b2ed8be4d1cbfbf2c42d273af23');
