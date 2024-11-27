﻿CREATE TABLE COUNTERS
(
	ID						INT			 NOT NULL,
	TABLE_NAME				VARCHAR(128) NOT NULL,
	PRIMARY_KEY_COLUMN_NAME VARCHAR(128) NOT NULL,
	CURRENT_ID				INT			 NOT NULL,
	INCREMENT_BY            INT			 NOT NULL,
	CONSTRAINT PK_COUNTERS_ID PRIMARY KEY(ID)
)
GO

CREATE UNIQUE INDEX UX_COUNTERS_TABLE_NAME
ON COUNTERS
(
	TABLE_NAME
)