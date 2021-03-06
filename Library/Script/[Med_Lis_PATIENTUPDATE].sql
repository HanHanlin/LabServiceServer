set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


ALTER PROCEDURE [dbo].[Med_Lis_PATIENTUPDATE]
(
    @PID        NVARCHAR(50)
   ,@Name       NVARCHAR(100)
   ,@Diachi     NVARCHAR(200)
   ,@bed     NVARCHAR(300)
   ,@khoa       NVARCHAR(200)
   ,@Age        INT
   ,@Year       INT
   ,@sex        BIT
   ,@test_date  DATETIME,@dichVu bit
)
AS
	IF EXISTS(
	       SELECT 1
	       FROM   L_PATIENT_INFO
	       WHERE  PID = @PID
	   )
	BEGIN
	    UPDATE L_PATIENT_INFO
	    SET    PATIENT_NAME = @Name
	          ,[Address] = @Diachi
	          ,Bed = @bed
	          ,Room = @khoa
	          ,AGE = @Age
	          ,YEAR_BIRTH = @Year
	          ,SEX = @sex
	          ,DATEUPDATE = @test_date,ObjectType = @dichVu
	    WHERE  PID = @PID
	END
	ELSE
	BEGIN
	    INSERT INTO L_PATIENT_INFO
	      (
	        PID
	       ,PATIENT_NAME
	       ,[Address],Bed,Room
	       ,AGE
	       ,YEAR_BIRTH
	       ,SEX
	       ,DATEUPDATE
	       ,ObjectType
	      )
	    VALUES
	      (
	        @PID
	       ,@Name
	       ,@Diachi,@bed,@khoa
	       ,@Age
	       ,@Year
	       ,@sex
	       ,@test_date
	       ,@dichVu
	      )
	END


