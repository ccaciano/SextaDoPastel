﻿CREATE TABLE [dbo].[TB_ADICIONAIS](
	[ID_ADD] INT IDENTITY NOT NULL,
	[DESC] VARCHAR(40) NOT NULL,
	[VALOR] DECIMAL(6,2) NOT NULL,
	[STATUS] INT NOT NULL,
	CONSTRAINT [PK_ADICIONAL] PRIMARY KEY([ID_ADD])
)
GO