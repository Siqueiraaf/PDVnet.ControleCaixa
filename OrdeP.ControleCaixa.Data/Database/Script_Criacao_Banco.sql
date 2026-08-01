CREATE DATABASE OrdePControleCaixa;
	GO
	
	USE OrdePControleCaixa;
	GO
	
	CREATE TABLE MovimentacaoCaixa
	(
	    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	
	    Descricao NVARCHAR(200) NOT NULL,
	
	    Tipo INT NOT NULL,
	
	    Categoria NVARCHAR(100) NULL,
	
	    Valor DECIMAL(10,2) NOT NULL,
	
	    DataMovimento DATETIME2 NOT NULL,
	
	    Status BIT NOT NULL DEFAULT 1
	);
	GO
	
	CREATE INDEX IX_MovimentacaoCaixa_DataMovimento
	ON MovimentacaoCaixa(DataMovimento);
	GO
	
	CREATE INDEX IX_MovimentacaoCaixa_Tipo
	ON MovimentacaoCaixa(Tipo);
	GO
	
	CREATE INDEX IX_MovimentacaoCaixa_Status
	ON MovimentacaoCaixa(Status);
	GO
	
	CREATE INDEX IX_MovimentacaoCaixa_Categoria
	ON MovimentacaoCaixa(Categoria);
	GO