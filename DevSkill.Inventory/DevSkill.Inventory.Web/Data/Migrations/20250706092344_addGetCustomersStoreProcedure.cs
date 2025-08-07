using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class addGetCustomersStoreProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """
                    CREATE OR   ALTER     PROCEDURE [dbo].[GetCustomers] 
                	@PageIndex int = 1,
                	@PageSize int = 10, 
                	@OrderBy nvarchar(50) = '',
                	@Name nvarchar(max) = '%',
                	@Email nvarchar(max) = '%',
                	@CompanyName nvarchar(max) = '%',
                	@MobileNumber nvarchar(max) = '%',
                	@BalanceFrom int = NULL,
                	@BalanceTo int = NULL,
                	@Total int output,
                	@TotalDisplay int output
                AS
                BEGIN

                	SET NOCOUNT ON;

                	Declare @sql nvarchar(2000);
                	Declare @countsql nvarchar(2000);
                	Declare @paramList nvarchar(MAX); 
                	Declare @countparamList nvarchar(MAX);

                	-- Collecting Total
                	Select @Total = count(*) from Products;

                	-- Collecting Total Display
                	SET @countsql = 'select @TotalDisplay = count(*) from Customers c where 1 = 1 ';

                	SET @countsql = @countsql + ' AND c.Name LIKE ''%'' + @xName + ''%''' 

                	SET @countsql = @countsql + ' AND c.Email LIKE ''%'' + @xEmail + ''%''' 

                	SET @countsql = @countsql + ' AND c.CompanyName LIKE ''%'' + @xCompanyName + ''%''' 

                	SET @countsql = @countsql + ' AND c.MobileNumber LIKE ''%'' + @xMobileNumber + ''%''' 

                	IF @BalanceFrom IS NOT NULL
                	SET @countsql = @countsql + ' AND c.CurrentBalance >= @xBalanceFrom'

                	IF @BalanceTo IS NOT NULL
                	SET @countsql = @countsql + ' AND c.CurrentBalance <= @xBalanceTo' 

                	SELECT @countparamlist = '@xName nvarchar(max),
                		@xEmail nvarchar(max),
                		@xCompanyName nvarchar(max),
                		@xMobileNumber nvarchar(max),
                		@xBalanceFrom int,
                		@xBalanceTo int,
                		@TotalDisplay int output' ;

                	exec sp_executesql @countsql , @countparamlist ,
                		@Name,
                		@Email,
                		@CompanyName,
                		@MobileNumber,
                		@BalanceFrom,
                		@BalanceTo,
                		@TotalDisplay = @TotalDisplay output;

                	-- Collecting Data
                	SET @sql = 'select * from Customers c where 1 = 1 ';

                	SET @sql = @sql + ' AND c.Name LIKE ''%'' + @xName + ''%''' 

                	SET @sql = @sql + ' AND c.Email LIKE ''%'' + @xEmail + ''%''' 

                	SET @sql = @sql + ' AND c.CompanyName LIKE ''%'' + @xCompanyName + ''%''' 

                	SET @sql = @sql + ' AND c.MobileNumber LIKE ''%'' + @xMobileNumber + ''%''' 

                	IF @BalanceFrom IS NOT NULL
                	SET @sql = @sql + ' AND c.CurrentBalance >= @xBalanceFrom'

                	IF @BalanceTo IS NOT NULL
                	SET @sql = @sql + ' AND c.CurrentBalance <= @xBalanceTo' 

                	SET @sql = @sql + ' Order by '+@OrderBy+' OFFSET @PageSize * (@PageIndex - 1) 
                	ROWS FETCH NEXT @PageSize ROWS ONLY';

                	SELECT @paramlist = '@xName nvarchar(max),
                		@xEmail nvarchar(max),
                		@xCompanyName nvarchar(max),
                		@xMobileNumber nvarchar(max),
                		@xBalanceFrom int,
                		@xBalanceTo int,
                		@PageIndex int,
                		@PageSize int' ;

                	exec sp_executesql @sql , @paramlist ,
                		@Name,
                		@Email,
                		@CompanyName,
                		@MobileNumber,
                		@BalanceFrom,
                		@BalanceTo,
                		@PageIndex,
                		@PageSize;

                	print @sql;
                	print @countsql;

                END
                """;

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE [dbo].[GetCustomers]";

            migrationBuilder.Sql(sql);
        }
    }
}
