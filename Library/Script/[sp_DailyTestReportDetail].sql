set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go









ALTER PROCEDURE [dbo].[sp_DailyTestReportDetail]
(
    @pTestDateFrom  DATETIME
   ,@pTestDateTo    DATETIME
   ,@pTestType_ID   INT
   ,@ObjectType     SMALLINT
)
AS
	SELECT PARA_NAME
	      ,TESTTYPE_ID
	      ,TESTTYPE_NAME
	      ,UpdateNum AS Amount
	      ,PrintDetail
	      ,(
	           SELECT COUNT(DISTINCT(patient_ID))
	           FROM   T_TEST_INFO T2
	           WHERE  dbo.trunc( TEST_DATE)BETWEEN dbo.trunc( @pTestDateFrom) 
	                  AND dbo.trunc (@pTestDateTo) 
--	           CONVERT(DATETIME ,TEST_DATE ,103) BETWEEN CONVERT(DATETIME ,@pTestDateFrom ,103) 
--	                  AND CONVERT(DATETIME ,@pTestDateTo ,103)
	                  AND TESTTYPE_ID = T1.TESTTYPE_ID
	                  AND EXISTS (
	                          SELECT 1
	                          FROM   L_PATIENT_INFO
	                          WHERE  PATIENT_ID = T2.PATIENT_ID
	                                 AND (@ObjectType=-1 OR ObjectType=@ObjectType)
	                      )
	       ) AS NumOfTest
	FROM   (
	           SELECT PARA_NAME
	                 ,TESTTYPE_ID
	                 ,(
	                      SELECT TESTTYPE_NAME
	                      FROM   T_TEST_TYPE_LIST
	                      WHERE  TESTTYPE_ID = T.TESTTYPE_ID
	                  ) AS TESTTYPE_NAME
	                 ,SUM(ISNULL(UpdateNum ,1)) AS UpdateNum
	                 ,--COUNT(PARA_NAME) AS Amount,
	                  --COUNT(DISTINCT(PATIENT_ID)) as NumOfTest,
	                  (
	                      SELECT COUNT(*)
	                      FROM   D_DATA_CONTROL
	                      WHERE  DEVICE_ID IN (SELECT DEVICE_ID
	                                           FROM   D_DEVICE_LIST
	                                           WHERE  TESTTYPE_ID = T.TESTTYPE_ID)
	                  ) AS CountParam
	                 ,(
	                      SELECT PrintDetail
	                      FROM   T_TEST_TYPE_LIST
	                      WHERE  TESTTYPE_ID = T.TESTTYPE_ID
	                  ) AS PrintDetail
	           FROM   T_RESULT_DETAIL T
	           WHERE   dbo.trunc( TEST_DATE)BETWEEN dbo.trunc( @pTestDateFrom) 
	                  AND dbo.trunc (@pTestDateTo)
--	           CONVERT(DATETIME ,TEST_DATE ,103) BETWEEN CONVERT(DATETIME ,@pTestDateFrom ,103) 
--	                  AND CONVERT(DATETIME ,@pTestDateTo ,103)
	                  AND (@pTestType_ID=-1 OR TESTTYPE_ID=@pTestType_ID)
	                  AND EXISTS (
	                          SELECT 1
	                          FROM   L_PATIENT_INFO
	                          WHERE  PATIENT_ID = T.PATIENT_ID
	                                 AND (@ObjectType=-1 OR ObjectType=@ObjectType)
	                      )
	           GROUP BY
	                  PARA_NAME
	                 ,TESTTYPE_ID--,UpdateNum
	       )T1
	ORDER BY
	       TESTTYPE_ID
	      ,PARA_NAME


