﻿CREATE TABLE [dbo].[TB_CLIENTE](
	[ID_CLIENTE] INT IDENTITY NOT NULL,
	[ID_LOG] INT NOT NULL,
	[NOME] VARCHAR(100) NULL,
	[MESA] VARCHAR(70) NULL,
	[LOCAL] VARCHAR(70) NULL,
	CONSTRAINT [PK_CLIENTE] PRIMARY KEY([ID_CLIENTE]),
	CONSTRAINT [FK_LOGIN] FOREIGN KEY ([ID_LOG]) REFERENCES [TB_LOGIN]([ID_LOG])
)
GO