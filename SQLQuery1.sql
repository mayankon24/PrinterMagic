USE [Data]
GO
/****** Object:  UserDefinedFunction [dbo].[GetMinValue]    Script Date: 12/02/2010 01:16:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetMinValue]
(
	-- Add the parameters for the function here
	@value1  DECIMAL(10,2), @value2  DECIMAL(10,2)
)
RETURNS  DECIMAL(10,2)
AS
BEGIN
	-- Declare the return variable here
	declare @ReturnValue  DECIMAL(10,2)
	if(@value1>@value2)
		set @ReturnValue= @value2   
    else
		set @ReturnValue = @value1 

    return  @ReturnValue

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetMaxValue]    Script Date: 12/02/2010 01:16:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetMaxValue]
(
	-- Add the parameters for the function here
	@value1  DECIMAL(10,2), @value2  DECIMAL(10,2)
)
RETURNS  DECIMAL(10,2)
AS
BEGIN
	-- Declare the return variable here
	declare @ReturnValue  DECIMAL(10,2)
	if(@value1>@value2)
		set @ReturnValue= @value1   
    else
		set @ReturnValue = @value2 

    return  @ReturnValue

END
GO
/****** Object:  Table [dbo].[M_COMPANY]    Script Date: 12/02/2010 01:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_COMPANY](
	[company_id] [bigint] IDENTITY(1,1) NOT NULL,
	[tin_no] [nvarchar](50) NULL,
	[company_name] [nvarchar](150) NULL,
	[address1] [nvarchar](450) NULL,
	[pan_no] [nvarchar](50) NULL,
	[city] [nvarchar](50) NULL,
	[state] [nvarchar](50) NULL,
	[pincode] [nvarchar](50) NULL,
	[email] [nvarchar](150) NULL,
	[phone] [nvarchar](100) NULL,
	[is_active] [int] NULL,
	[added_date] [datetime] NULL,
	[midified_date] [datetime] NULL,
	[future1] [nvarchar](100) NULL,
	[future2] [nvarchar](100) NULL,
 CONSTRAINT [PK_M_COMPANY] PRIMARY KEY CLUSTERED 
(
	[company_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_PROJECT]    Script Date: 12/02/2010 01:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_PROJECT](
	[project_id] [bigint] IDENTITY(1,1) NOT NULL,
	[project_name] [nvarchar](150) NULL,
	[book_name] [nvarchar](150) NULL,
	[book_quantity] [decimal](10, 2) NULL,
	[book_page] [decimal](10, 2) NULL,
	[form_use] [decimal](10, 2) NULL,
	[westage_percentage] [decimal](10, 2) NULL,
	[westage_form] [decimal](10, 2) NULL,
	[is_active] [int] NULL,
	[added_date] [datetime] NULL,
	[midified_date] [datetime] NULL,
	[future1] [nchar](10) NULL,
	[future2] [nchar](10) NULL,
 CONSTRAINT [PK_M_PROJECT] PRIMARY KEY CLUSTERED 
(
	[project_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[M_PAPER]    Script Date: 12/02/2010 01:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_PAPER](
	[paper_id] [bigint] IDENTITY(1,1) NOT NULL,
	[paper_name] [nvarchar](250) NULL,
	[quality] [nvarchar](150) NULL,
	[size] [nvarchar](150) NULL,
	[weight] [nvarchar](150) NULL,
	[color] [nvarchar](150) NULL,
	[is_active] [int] NULL,
	[added_date] [datetime] NULL,
	[midified_date] [datetime] NULL,
	[future1] [nchar](10) NULL,
	[future2] [nchar](10) NULL,
 CONSTRAINT [paper_id] PRIMARY KEY CLUSTERED 
(
	[paper_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TX_COMPANY_PAPER]    Script Date: 12/02/2010 01:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TX_COMPANY_PAPER](
	[company_paper_id] [bigint] IDENTITY(1,1) NOT NULL,
	[company_id] [bigint] NULL,
	[paper_id] [bigint] NULL,
	[is_active] [int] NULL,
	[added_date] [datetime] NULL,
	[midified_date] [datetime] NULL,
	[future1] [nchar](10) NULL,
	[future2] [nchar](10) NULL,
 CONSTRAINT [PK_M_PAPER] PRIMARY KEY CLUSTERED 
(
	[company_paper_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertCompany]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCompany]
(
	@tin_no nvarchar(50),
	@company_name nvarchar(50),
	@address1 nvarchar(150),
	@pan_no nvarchar(150),
	@city nvarchar(50),
	@state nvarchar(50),
	@pincode nvarchar(50),
	@email nvarchar(150),
	@phone nvarchar(100)
	
)
AS
	SET NOCOUNT OFF;
INSERT INTO [M_COMPANY](  [tin_no]
						, [company_name]
						, [address1]
						, [pan_no]
						, [city]
						, [state]
						, [pincode]
						, [email]
						, [phone]
						, [is_active]
						, [added_date]
						, [midified_date]
					  )
			 VALUES 
					( @tin_no
					, @company_name
					, @address1
					, @pan_no
					, @city
					, @state
					, @pincode
					, @email
					, @phone
					, 1
					, GETDATE()
					, GETDATE()
				   );
						
Declare @CompanyId int
set @CompanyId = SCOPE_IDENTITY()
select @CompanyId
GO
/****** Object:  StoredProcedure [dbo].[SelectCompanyById]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectCompanyById]
(
	@company_id int
)
AS
	SET NOCOUNT ON;
SELECT        company_id
			, tin_no
			, company_name
			, address1
			, pan_no
			, city
			, state
			, pincode
			, email
			, phone
			
FROM M_COMPANY
WHERE (company_id = @company_id)
GO
/****** Object:  StoredProcedure [dbo].[SelectCompanyAll]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectCompanyAll]

AS
	SET NOCOUNT ON;
SELECT        company_id
            , company_name AS [Company Name] 
			, tin_no AS [Tin No]		
			, pan_no as [Pan No]
			, phone	As [Phone]		
			, address1 as [Address]
			, city as [City]
			, state as [State]
			, pincode as [Pin Code]
			, email As [Email]
			
FROM M_COMPANY
order by company_name
GO
/****** Object:  StoredProcedure [dbo].[DeleteCompany]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCompany]
(
	@company_id int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [M_COMPANY] WHERE (([company_id] = @company_id))
GO
/****** Object:  StoredProcedure [dbo].[UpdateCompany]    Script Date: 12/02/2010 01:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCompany]
(
	@tin_no nvarchar(50),
	@company_name nvarchar(50),
	@address1 nvarchar(150),
	@pan_no nvarchar(150),
	@city nvarchar(50),
	@state nvarchar(50),
	@pincode nvarchar(50),
	@email nvarchar(150),
	@phone nvarchar(100),
	@company_id BIGINT	
)
AS
	SET NOCOUNT OFF;
UPDATE [M_COMPANY] 
  SET  [tin_no] = @tin_no
	 , [company_name] = @company_name
	 , [address1] = @address1
	 , [pan_no] = @pan_no
	 , [city] = @city
	 , [state] = @state
	 , [pincode] = @pincode
	 , [email] = @email
	 , [phone] = @phone					
 WHERE [company_id] = @company_id;
GO
/****** Object:  Table [dbo].[M_CONFIG]    Script Date: 12/02/2010 01:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_CONFIG](
	[config_id] [bigint] IDENTITY(1,1) NOT NULL,
	[company_paper_id] [bigint] NULL,
	[default_westage_percentage] [decimal](10, 2) NULL,
	[default_westage_form] [decimal](10, 2) NULL,
	[is_active] [int] NULL,
	[added_date] [datetime] NULL,
	[midified_date] [datetime] NULL,
	[future1] [nchar](10) NULL,
	[future2] [nchar](10) NULL,
 CONSTRAINT [PK_M_CONFIG] PRIMARY KEY CLUSTERED 
(
	[config_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TX_RECEIVING]    Script Date: 12/02/2010 01:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TX_RECEIVING](
	[receive_id] [bigint] IDENTITY(1,1) NOT NULL,
	[company_paper_id] [bigint] NOT NULL,
	[narration] [nvarchar](1000) NULL,
	[date] [datetime] NULL,
	[challan_no] [nvarchar](50) NULL,
	[receipt_ream] [decimal](10, 2) NULL,
	[ream_size] [decimal](10, 2) NULL,
	[is_active] [int] NULL,
	[added_date] [datetime] NULL,
	[midified_date] [datetime] NULL,
	[future1] [nchar](10) NULL,
	[future2] [nchar](10) NULL,
 CONSTRAINT [PK_M_RECEIVING] PRIMARY KEY CLUSTERED 
(
	[receive_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TX_ISSUE]    Script Date: 12/02/2010 01:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TX_ISSUE](
	[issue_id] [bigint] IDENTITY(1,1) NOT NULL,
	[company_paper_id] [bigint] NULL,
	[project_id] [bigint] NULL,
	[date] [datetime] NULL,
	[challan_no] [nvarchar](50) NULL,
	[issue_quantity] [decimal](10, 2) NULL,
	[is_active] [int] NULL,
	[added_date] [datetime] NULL,
	[midified_date] [datetime] NULL,
	[future1] [nchar](10) NULL,
	[future2] [nchar](10) NULL,
 CONSTRAINT [PK_M_ISSUE] PRIMARY KEY CLUSTERED 
(
	[issue_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[UpdateReceiving]    Script Date: 12/02/2010 01:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateReceiving]
(
    @narration NVARCHAR(1000),
	@date datetime,
	@challan_no nvarchar(50),
	@received_ream  DECIMAL(10,2),
	@ream_size  DECIMAL(10,2),
	@receive_id BIGINT
	
)
AS
	SET NOCOUNT OFF;
	
UPDATE [TX_RECEIVING]
   SET [narration] = @narration
      ,[date] = @date
      ,[challan_no] = @challan_no
      ,[receipt_ream] = @received_ream
      ,[ream_size] = @ream_size      
 WHERE (receive_id = @receive_id)
GO
/****** Object:  StoredProcedure [dbo].[UpdateIssue]    Script Date: 12/02/2010 01:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateIssue]
(
	@date datetime,
	@challan_no nvarchar(50),
	@issue_quantity  DECIMAL(10,2),
	@issue_id BIGINT,
	
	@project_name nvarchar(150),
	@book_name nvarchar(150),
	@book_quantity  DECIMAL(10,2),
	@book_page  DECIMAL(10,2),
	@form_use  DECIMAL(10,2),
	@wastage_percentage  DECIMAL(10,2),
	@wastage_form  DECIMAL(10,2)	
)
AS
	SET NOCOUNT OFF;  
UPDATE [TX_ISSUE] 
SET               [date] = @date
				, [challan_no] = @challan_no
				, [issue_quantity] = @issue_quantity
				
WHERE [issue_id] = @issue_id

	
	
DECLARE @project_id BIGINT
SELECT @project_id = project_id FROM TX_ISSUE(NOLOCK) WHERE issue_id = @issue_id



UPDATE [M_PROJECT]
   SET [project_name] = @project_name
      ,[book_name] = @book_name
      ,[book_quantity] = @book_quantity
      ,[book_page] = @book_page
      ,[form_use] = @form_use
      ,[westage_percentage] = @wastage_percentage
      ,[westage_form] = @wastage_form      
 WHERE (project_id = @project_id)
GO
/****** Object:  StoredProcedure [dbo].[UpdateCompanyPaper]    Script Date: 12/02/2010 01:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCompanyPaper]
(
    @paper_name nvarchar(50),
	@quality nvarchar(50),	
	@size nvarchar(50),
	@weight nvarchar(50),
	@color nvarchar(50),
	@default_westage_percentage  DECIMAL(10,2),
	@default_westage_form  DECIMAL(10,2),
	@company_paper_id BIGINT
	
)
AS
	SET NOCOUNT OFF;
	
DECLARE @paper_id BIGINT 
SELECT @paper_id = paper_id FROM TX_COMPANY_PAPER WHERE company_paper_id = @company_paper_id

	
DECLARE @config_id BIGINT 
SELECT @config_id = config_id FROM M_CONFIG WHERE company_paper_id = @company_paper_id	
	
UPDATE [M_PAPER] 
SET   [paper_name]= @paper_name
	, [quality] = @quality
	, [size] = @size
	, [weight] = @weight
	, [color] = @color 
WHERE [paper_id] = @paper_id;


UPDATE [M_CONFIG]
   SET [default_westage_percentage] = @default_westage_percentage
      ,[default_westage_form] = @default_westage_form  
 WHERE config_id = @config_id
	
SELECT paper_id, quality, size, weight, color, future1, future2 FROM M_PAPER WHERE (paper_id = @paper_id)
GO
/****** Object:  StoredProcedure [dbo].[ReportFinal]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ReportFinal]
(
	  @company_paper_id Bigint
	, @from_date datetime
	, @to_date datetime
)
AS
	SET NOCOUNT ON;


SELECT    [Type]
        , [Id] 
		, [Date]
		, [Challan No]
		, [Particulars]
		, CONVERT(DECIMAL(10,2),[Receipt]) AS [Receipt in Ream]
		, [Quantity]
		, [Number of form]
		, CONVERT(DECIMAL(10,4),[Quantity in sheets]) AS [Quantity Consume in sheets]
		, [Wastage in %]
		, CONVERT(DECIMAL(10,4),[Wastage in sheets]) AS [Wastage in sheets]
		, [Minimum Wastage]
		, CONVERT(DECIMAL(10,4),[Final Wastage]) AS [Final Wastage]
		, CONVERT(DECIMAL(10,4),[Final Quantity]) AS [Final Quantity in sheets]
		, CONVERT(DECIMAL(10,4),[Final Quantity in Ream]) AS [Final Quantity in Ream]
		, CONVERT(DECIMAL(10,4),0) AS [Balance]
		,CONVERT(NVARCHAR(1000),'') AS [Balance in Ream]
FROM (
		SELECT    0 AS [Type]    
		        , TX_ISSUE.issue_id AS [Id]
				, TX_ISSUE.date AS [Date]
				, TX_ISSUE.challan_no AS [Challan No]
				, M_PROJECT.book_name AS [Particulars]
				, NULL AS [Receipt]
				, TX_ISSUE.issue_quantity AS [Quantity]
				, M_PROJECT.form_use AS [Number of form]
				, (TX_ISSUE.issue_quantity * M_PROJECT.form_use) AS [Quantity in sheets]
				, M_PROJECT.westage_percentage AS [Wastage in %]
				, ((TX_ISSUE.issue_quantity * M_PROJECT.form_use)*M_PROJECT.westage_percentage/100) AS [Wastage in sheets]
				, M_PROJECT.westage_form AS [Minimum Wastage]
				, dbo.GetMaxValue(((TX_ISSUE.issue_quantity * M_PROJECT.form_use)*M_PROJECT.westage_percentage/100),M_PROJECT.westage_form) AS [Final Wastage]
				, dbo.GetMaxValue(((TX_ISSUE.issue_quantity * M_PROJECT.form_use)*M_PROJECT.westage_percentage/100),M_PROJECT.westage_form) + (TX_ISSUE.issue_quantity * M_PROJECT.form_use) AS [Final Quantity]
				, (dbo.GetMaxValue(((TX_ISSUE.issue_quantity * M_PROJECT.form_use)*M_PROJECT.westage_percentage/100),M_PROJECT.westage_form) + (TX_ISSUE.issue_quantity * M_PROJECT.form_use))/500 AS [Final Quantity in Ream]
				
		FROM M_PROJECT INNER JOIN
			 TX_ISSUE ON M_PROJECT.project_id = TX_ISSUE.project_id
		WHERE (TX_ISSUE.company_paper_id = @company_paper_id) AND
			  (TX_ISSUE.date >= @from_date) AND
			  (TX_ISSUE.date <= @to_date)   
			     
        UNION  
            
		SELECT   1 AS [Type]
		        , receive_id AS [Id]
				,[date] AS [Date]
				,[challan_no] AS [Challan No]
				,[narration] AS [Particulars]
				,((receipt_ream * ream_size)/500)  AS [Receipt]
				, NULL AS [Quantity]
				, NULL AS [Number of form]
				, NULL AS [Quantity in sheets]
				, NULL AS [Wastage in %]
				, NULL AS [Wastage in sheets]
				, NULL AS [Min Wastage]
				, NULL AS [Final Wastage]
				, NULL AS [Final Quantity]
				, NULL AS [Final Quantity in Ream]			
		FROM TX_RECEIVING
		WHERE (TX_RECEIVING.company_paper_id = @company_paper_id) AND
			  (TX_RECEIVING.date >= @from_date) AND
			  (TX_RECEIVING.date <= @to_date)
		
	    
		
) AS t	  
ORDER By [Date]
			  

  
 
  
--WITH cte( [Id] 
--		, [Date]
--		, [Challan No]
--		, [Particulars]
--		, [Receipt]
--		, [Quantity]
--		, [Number of form]
--		, [Quantity in sheets]
--		, [Wastage in %]
--		, [Wastage in sheets]
--		, [Min Wastage]
--		, [Final Wastage]
--		, [Final Quantity]
--		, [Final Quantity in Ream] 
--		, Balance
--		, [Type]
--		, Rno
--        )
--AS
--(
--	 SELECT   NULL [Id] 
--			, NULL AS [Date]
--			, NULL AS [Challan No]
--			, ('Opening Balance') AS [Particulars]
--			, NULL AS [Receipt]
--			, NULL AS [Quantity]
--			, NULL AS [Number of form]
--			, NULL AS [Quantity in sheets]
--			, NULL AS [Wastage in %]
--			, NULL AS [Wastage in sheets]
--			, NULL AS [Min Wastage]
--			, NULL AS [Final Wastage]
--			, NULL AS [Final Quantity]
--			, NULL AS [Final Quantity in Ream] 
--			, @OpeningBalance
--			, 1 AS [Type]
--			, 0 AS Rno
--	union all	
--	select t2.[Id]
--		, t2.[Date]
--		, t2.[Challan No]
--		, t2.[Particulars]
--		, t2.[Receipt]
--		, t2.[Quantity]
--		, t2.[Number of form]
--		, t2.[Quantity in sheets]
--		, t2.[Wastage in %]
--		, t2.[Wastage in sheets]
--		, t2.[Min Wastage]
--		, t2.[Final Wastage]
--		, t2.[Final Quantity]
--		, t2.[Final Quantity in Ream] 
--		, case  
--			when c.[type]= 1
--				then  convert(int,c.Balance - t2.[Final Quantity in Ream])
--			when c.[type] IS NULL
--				THEN convert(int,c.Balance - t2.[Final Quantity in Ream])
--			else 
--				convert(int,c.Balance + t2.[Final Quantity in Ream])
--			END 
--		, t2.[Type]
--		, t2.Rno 
--	from #temp t2 inner join cte as c on t2.Rno-1=c.Rno
--)
--select * from cte


--DROP Table #t
GO
/****** Object:  StoredProcedure [dbo].[GetOpeningBalnce]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetOpeningBalnce]
(
	  @company_paper_id Bigint
	, @from_date datetime
	, @to_date datetime
)
AS
	SET NOCOUNT ON;

DECLARE @IssueTotal  DECIMAL(10,2)
SELECT @IssueTotal = ISNULL(SUM(dbo.GetMaxValue(((TX_ISSUE.issue_quantity * M_PROJECT.form_use)*M_PROJECT.westage_percentage/100),M_PROJECT.westage_form) + (TX_ISSUE.issue_quantity * M_PROJECT.form_use)),0)
FROM M_PROJECT INNER JOIN
     TX_ISSUE ON M_PROJECT.project_id = TX_ISSUE.project_id
WHERE (TX_ISSUE.company_paper_id = @company_paper_id) AND
      (TX_ISSUE.date < @from_date)
      
      
DECLARE @ReceivingTotal  DECIMAL(10,2)
SELECT @ReceivingTotal = ISNULL(SUM(receipt_ream*ream_size),0)     
FROM TX_RECEIVING
WHERE (TX_RECEIVING.company_paper_id = @company_paper_id) AND
	  (TX_RECEIVING.date < @from_date)
	  
	  
DECLARE @OpeningBalance  DECIMAL(10,2)
SET @OpeningBalance = ISNULL(@ReceivingTotal - @IssueTotal,0)/500

Select @OpeningBalance
GO
/****** Object:  StoredProcedure [dbo].[SelectReceivingById]    Script Date: 12/02/2010 01:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectReceivingById]
(
    @receive_id BIGINT
)
AS
	SET NOCOUNT ON;
SELECT    narration 			
		, date 
		, challan_no 
		, receipt_ream 
		, ream_size 	

FROM  TX_RECEIVING 
WHERE receive_id = @receive_id
GO
/****** Object:  StoredProcedure [dbo].[SelectReceivingByComPaperId]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectReceivingByComPaperId]
(
    @company_paper_id BIGINT
)
AS
	SET NOCOUNT ON;
SELECT        receive_id
            , narration AS [Particulars] 			
			, date as [Date]
			, challan_no as [Challan No]
			, receipt_ream as [Receipt Ream]
			, ream_size as [Ream Size]
			--, CONVERT(DECIMAL(10,2),(receipt_ream * ream_size)) AS [Total Form]

FROM  TX_RECEIVING 
WHERE company_paper_id = @company_paper_id
GO
/****** Object:  StoredProcedure [dbo].[SelectIssueById]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectIssueById]
(
	 @issue_id BIGINT
)
AS
	SET NOCOUNT ON;
SELECT  TX_ISSUE.issue_id,
		TX_ISSUE.date,
		TX_ISSUE.challan_no,
		TX_ISSUE.issue_quantity,
		M_PROJECT.book_name, 
		M_PROJECT.book_quantity,
		M_PROJECT.form_use,
		M_PROJECT.westage_percentage,
		M_PROJECT.westage_form
FROM M_PROJECT INNER JOIN
     TX_ISSUE ON M_PROJECT.project_id = TX_ISSUE.project_id
WHERE TX_ISSUE.issue_id = @issue_id
GO
/****** Object:  StoredProcedure [dbo].[SelectIssueByComPaperId]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectIssueByComPaperId]
(
	 @company_paper_id BIGINT
)
AS
	SET NOCOUNT ON;
SELECT 
	      issue_id
		, [Date]
		, [Challan No]
		, [Particulars]
		, [Quantity]
		, [Number of form]
		, CONVERT(DECIMAL(10,2),[Quantity in sheets]) AS [Quantity in sheets] 
		, [Wastage in %]
		, CONVERT(DECIMAL(10,4),[Wastage in sheets]) AS [Wastage in sheets]
		, CONVERT(DECIMAL(10,2),[Min Wastage]) AS [Min Wastage]
		, CONVERT(DECIMAL(10,4),[Final Wastage]) AS [Final Wastage]
		, CONVERT(DECIMAL(10,4),[Final Quantity]) AS [Final Quantity]
		, CONVERT(DECIMAL(10,4),[Final Quantity in Ream]) AS [Final Quantity in Ream]
FROM(
		SELECT  TX_ISSUE.issue_id
				, TX_ISSUE.date AS [Date]
				, TX_ISSUE.challan_no AS [Challan No]
				, M_PROJECT.book_name AS [Particulars]
				, TX_ISSUE.issue_quantity AS [Quantity]
				, M_PROJECT.form_use AS [Number of form]
				, (TX_ISSUE.issue_quantity * M_PROJECT.form_use) AS [Quantity in sheets]
				, M_PROJECT.westage_percentage AS [Wastage in %]
				, ((TX_ISSUE.issue_quantity * M_PROJECT.form_use)*M_PROJECT.westage_percentage/100) AS [Wastage in sheets]
				, M_PROJECT.westage_form AS [Min Wastage]
				, dbo.GetMaxValue(((TX_ISSUE.issue_quantity * M_PROJECT.form_use)*M_PROJECT.westage_percentage/100),M_PROJECT.westage_form) AS [Final Wastage]
				, dbo.GetMaxValue(((TX_ISSUE.issue_quantity * M_PROJECT.form_use)*M_PROJECT.westage_percentage/100),M_PROJECT.westage_form) + (TX_ISSUE.issue_quantity * M_PROJECT.form_use) AS [Final Quantity]
				, (dbo.GetMaxValue(((TX_ISSUE.issue_quantity * M_PROJECT.form_use)*M_PROJECT.westage_percentage/100),M_PROJECT.westage_form) + (TX_ISSUE.issue_quantity * M_PROJECT.form_use))/500 AS [Final Quantity in Ream]
		FROM M_PROJECT INNER JOIN
			 TX_ISSUE ON M_PROJECT.project_id = TX_ISSUE.project_id
		WHERE TX_ISSUE.company_paper_id = @company_paper_id
	)AS t
ORDER BY [Date]
GO
/****** Object:  StoredProcedure [dbo].[SelectCompanyPaperById]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectCompanyPaperById]
(
	@company_paper_id BIGINT
)

AS
	SET NOCOUNT ON;
SELECT        
			  paper_name 
			, quality 
			, size 
			, weight 
			, default_westage_form
			, default_westage_percentage			
FROM M_PAPER INNER JOIN
      TX_COMPANY_PAPER ON TX_COMPANY_PAPER.paper_id = M_PAPER.paper_id INNER JOIN
      M_CONFIG On M_CONFIG.company_paper_id = TX_COMPANY_PAPER.company_paper_id
WHERE TX_COMPANY_PAPER.company_paper_id = @company_paper_id
GO
/****** Object:  StoredProcedure [dbo].[SelectCompanyPaperByCompanyId]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectCompanyPaperByCompanyId]
(
	@company_id BIGINT
)

AS
	SET NOCOUNT ON;
SELECT        TX_COMPANY_PAPER.company_paper_id
			, paper_name as [Paper Name]
			, quality As [Quality]
			, size As [Size]
			, weight as [Weight]
			, default_westage_form AS [Default Min Westage]
			, default_westage_percentage AS [Default % Westage]	
						
FROM  M_PAPER INNER JOIN
      TX_COMPANY_PAPER ON TX_COMPANY_PAPER.paper_id = M_PAPER.paper_id INNER JOIN
      M_CONFIG On M_CONFIG.company_paper_id = TX_COMPANY_PAPER.company_paper_id
WHERE TX_COMPANY_PAPER.company_id = @company_id
GO
/****** Object:  StoredProcedure [dbo].[InsertReceiving]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertReceiving]
(
	
	@company_paper_id BIGINT,
	@narration nvarchar(1000),
	@date datetime,
	@challan_no nvarchar(50),
	@receipt_ream  DECIMAL(10,2),
	@ream_size  DECIMAL(10,2)
	
)
AS
	SET NOCOUNT OFF;
INSERT INTO [TX_RECEIVING]
           ([company_paper_id]
           ,[narration]
           ,[date]
           ,[challan_no]
           ,[receipt_ream]
           ,[ream_size]
           ,[is_active]
           ,[added_date]
           ,[midified_date])          
     VALUES
           (@company_paper_id
           ,@narration
           ,@date
           ,@challan_no
           ,@receipt_ream
           ,@ream_size
           ,1
           ,GETDATE()
           ,GETDATE()
           )
          
DECLARE @receive_id BIGINT
SET @receive_id = SCOPE_IDENTITY()
SELECT @receive_id
GO
/****** Object:  StoredProcedure [dbo].[InsertIssue]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertIssue]
(
	@project_name nvarchar(150),
	@book_name nvarchar(150),
	@book_quantity  DECIMAL(10,2),
	@book_page  DECIMAL(10,2),
	@form_use  DECIMAL(10,2),
	@westage_percentage  DECIMAL(10,2),
	@westage_form  DECIMAL(10,2),
	
	
	@company_paper_id BIGINT,
	@date DATETIME,
	@challan_no NVARCHAR(50)
	
)
AS
	SET NOCOUNT OFF;
INSERT INTO [M_PROJECT]
           ([project_name]
           ,[book_name]
           ,[book_quantity]
           ,[book_page]
           ,[form_use]
           ,[westage_percentage]
           ,[westage_form]
           ,[is_active]
           ,[added_date]
           ,[midified_date])          
     VALUES
           (@project_name
           ,@book_name
           ,@book_quantity
           ,@book_page
           ,@form_use
           ,@westage_percentage
           ,@westage_form
           ,1
           ,GETDATE()
           ,GETDATE()
           )

declare @ProjectId BIGINT
set  @ProjectId =  SCOPE_IDENTITY()


INSERT INTO [TX_ISSUE]
           ([company_paper_id]
           ,[project_id]
           ,[date]
           ,[challan_no]
           ,[issue_quantity]
           ,[is_active]
           ,[added_date]
           ,[midified_date])          
     VALUES
           (@company_paper_id
           ,@ProjectId
           ,@date
           ,@challan_no
           ,@book_quantity
           ,1
           ,GETDATE()
           ,GETDATE()
           )

declare @issue_id BIGINT
set  @issue_id =  SCOPE_IDENTITY()
SELECT @issue_id
GO
/****** Object:  StoredProcedure [dbo].[InsertCompanyPaper]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCompanyPaper]
(
    @company_id BIGINT,
    @paper_name  nvarchar(50),
	@quality nvarchar(50),
	@size nvarchar(50),
	@weight nvarchar(50),
	@color nvarchar(50),
	@default_westage_percentage  DECIMAL(10,2),
	@default_westage_form  DECIMAL(10,2)
	
)
AS
	SET NOCOUNT OFF;
INSERT INTO [M_PAPER] (	  paper_name
						, [quality]
						, [size]
						, [weight]
						, [color]
						, [is_active]
                        , [added_date]
                        , [midified_date]
					  )
				VALUES(
						  @paper_name
						, @quality
						, @size
						, @weight
						, @color
						, 1
						, GETDATE()
						, GETDATE()
						);
DECLARE @paper_id BIGINT
SET @paper_id = SCOPE_IDENTITY()



INSERT INTO TX_COMPANY_PAPER

           ( [company_id]
           , [paper_id]
           , [is_active]
           , [added_date]
           , [midified_date]
           )
         
     VALUES
           ( @company_id
           , @paper_id
           , 1
           , GETDATE()
           , GETDATE()
           )
           
           
DECLARE @company_paper_id BIGINT
SET @company_paper_id = SCOPE_IDENTITY()

INSERT INTO [M_CONFIG]
           ([company_paper_id]
           ,[default_westage_percentage]
           ,[default_westage_form]
           ,[is_active]
           ,[added_date]
           ,[midified_date])          
     VALUES
           (@company_paper_id
           ,@default_westage_percentage
           ,@default_westage_form
           ,1
           ,GETDATE()
           ,GETDATE())
           
SELECT @company_paper_id
GO
/****** Object:  StoredProcedure [dbo].[GetConfig]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetConfig]
(
	  @company_paper_id Bigint
)
AS
	SET NOCOUNT ON;
	
SELECT default_westage_percentage
      ,default_westage_form    
  FROM M_CONFIG
  WHERE company_paper_id = @company_paper_id
GO
/****** Object:  StoredProcedure [dbo].[DeleteReceiving]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[DeleteReceiving]
(
	@receive_id int
)
AS
	SET NOCOUNT OFF;
DELETE FROM [TX_RECEIVING] WHERE [receive_id] = @receive_id
GO
/****** Object:  StoredProcedure [dbo].[DeleteIssue]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteIssue]
(
	@issue_id int
)
AS
	SET NOCOUNT OFF;
	
	
	
DECLARE @project_id BIGINT
SELECT @project_id = project_id FROM TX_ISSUE(NOLOCK) WHERE issue_id = @issue_id


DELETE FROM [TX_ISSUE] WHERE ([issue_id] = @issue_id)

DELETE FROM [M_PROJECT] WHERE (project_id = @project_id)
GO
/****** Object:  StoredProcedure [dbo].[DeleteCompanyPaper]    Script Date: 12/02/2010 01:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCompanyPaper]
(
	@company_paper_id bigint
)
AS
	SET NOCOUNT OFF;
	
	
DECLARE @paper_id BIGINT 
SELECT @paper_id = paper_id FROM TX_COMPANY_PAPER WHERE company_paper_id = @company_paper_id

	
DECLARE @config_id BIGINT 
SELECT @config_id = config_id FROM M_CONFIG WHERE company_paper_id = @company_paper_id

DELETE FROM M_CONFIG WHERE (config_id = @config_id)

DELETE FROM TX_COMPANY_PAPER WHERE (company_paper_id = @company_paper_id)

DELETE FROM [M_PAPER] WHERE ([paper_id] = @paper_id)
GO
/****** Object:  ForeignKey [FK_M_CONFIG_TX_COMPANY_PAPER]    Script Date: 12/02/2010 01:16:26 ******/
ALTER TABLE [dbo].[M_CONFIG]  WITH CHECK ADD  CONSTRAINT [FK_M_CONFIG_TX_COMPANY_PAPER] FOREIGN KEY([company_paper_id])
REFERENCES [dbo].[TX_COMPANY_PAPER] ([company_paper_id])
GO
ALTER TABLE [dbo].[M_CONFIG] CHECK CONSTRAINT [FK_M_CONFIG_TX_COMPANY_PAPER]
GO
/****** Object:  ForeignKey [FK_TX_COMPANY_PAPER_M_COMPANY]    Script Date: 12/02/2010 01:16:26 ******/
ALTER TABLE [dbo].[TX_COMPANY_PAPER]  WITH CHECK ADD  CONSTRAINT [FK_TX_COMPANY_PAPER_M_COMPANY] FOREIGN KEY([company_id])
REFERENCES [dbo].[M_COMPANY] ([company_id])
GO
ALTER TABLE [dbo].[TX_COMPANY_PAPER] CHECK CONSTRAINT [FK_TX_COMPANY_PAPER_M_COMPANY]
GO
/****** Object:  ForeignKey [FK_TX_COMPANY_PAPER_M_PAPER]    Script Date: 12/02/2010 01:16:26 ******/
ALTER TABLE [dbo].[TX_COMPANY_PAPER]  WITH CHECK ADD  CONSTRAINT [FK_TX_COMPANY_PAPER_M_PAPER] FOREIGN KEY([paper_id])
REFERENCES [dbo].[M_PAPER] ([paper_id])
GO
ALTER TABLE [dbo].[TX_COMPANY_PAPER] CHECK CONSTRAINT [FK_TX_COMPANY_PAPER_M_PAPER]
GO
/****** Object:  ForeignKey [FK_TX_ISSUE_M_PROJECT]    Script Date: 12/02/2010 01:16:26 ******/
ALTER TABLE [dbo].[TX_ISSUE]  WITH CHECK ADD  CONSTRAINT [FK_TX_ISSUE_M_PROJECT] FOREIGN KEY([project_id])
REFERENCES [dbo].[M_PROJECT] ([project_id])
GO
ALTER TABLE [dbo].[TX_ISSUE] CHECK CONSTRAINT [FK_TX_ISSUE_M_PROJECT]
GO
/****** Object:  ForeignKey [FK_TX_ISSUE_TX_COMPANY_PAPER]    Script Date: 12/02/2010 01:16:26 ******/
ALTER TABLE [dbo].[TX_ISSUE]  WITH CHECK ADD  CONSTRAINT [FK_TX_ISSUE_TX_COMPANY_PAPER] FOREIGN KEY([company_paper_id])
REFERENCES [dbo].[TX_COMPANY_PAPER] ([company_paper_id])
GO
ALTER TABLE [dbo].[TX_ISSUE] CHECK CONSTRAINT [FK_TX_ISSUE_TX_COMPANY_PAPER]
GO
/****** Object:  ForeignKey [FK_TX_RECEIVING_TX_COMPANY_PAPER]    Script Date: 12/02/2010 01:16:26 ******/
ALTER TABLE [dbo].[TX_RECEIVING]  WITH CHECK ADD  CONSTRAINT [FK_TX_RECEIVING_TX_COMPANY_PAPER] FOREIGN KEY([company_paper_id])
REFERENCES [dbo].[TX_COMPANY_PAPER] ([company_paper_id])
GO
ALTER TABLE [dbo].[TX_RECEIVING] CHECK CONSTRAINT [FK_TX_RECEIVING_TX_COMPANY_PAPER]
GO
