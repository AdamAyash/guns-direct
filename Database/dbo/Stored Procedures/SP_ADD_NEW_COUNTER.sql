﻿CREATE PROCEDURE SP_ADD_NEW_COUNTER
(
	@TABLE_NAME		varchar(128),
	@COLUMN_NAME	varchar(128),
	@START_AT		int = 1,
	@INCREMENT_BY 	int = 1
)
AS
BEGIN
	DECLARE @nextMaxId as int
	SELECT @nextMaxId = ISNULL(MAX(ID), 0) FROM COUNTERS WITH(NOLOCK)

	IF( SELECT ID FROM COUNTERS WHERE TABLE_NAME = @TABLE_NAME) = NULL
		PRINT('Броячът вече съществува в базата данни.')
	ELSE

	BEGIN TRY
		INSERT INTO COUNTERS
		VALUES(
			@nextMaxId + 1, 
			UPPER(@TABLE_NAME), 
			UPPER(@COLUMN_NAME), 
			@START_AT,
			@INCREMENT_BY
		)
	END TRY
	BEGIN CATCH
		
	END CATCH
END