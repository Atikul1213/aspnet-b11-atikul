using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class addGetSalesProductStoreProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """
                    CREATE OR ALTER  PROCEDURE [dbo].[GetSalesProduct] 
                	@PageIndex int = 1,
                	@PageSize int = 10, 
                	@OrderBy nvarchar(50) = '',
                	@CustomerName nvarchar(max) = '%',
                	@StatusId int = NULL,
                	@TotalFrom int = NULL,
                	@TotalTo int = NULL,
                	@DateFrom dateTime = NULL,
                	@DateTo dateTime = NULL,
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
                	SET @countsql = 'select @TotalDisplay = count(*) from Sales s where 1 = 1 ';

                	SET @countsql = @countsql + ' AND s.CustomerName LIKE ''%'' + @xCustomerName + ''%''' 

                	IF @StatusId IS NOT NULL
                	SET @countsql = @countsql + ' AND s.StatusId = @xStatusId'

                	IF @TotalFrom IS NOT NULL
                	SET @countsql = @countsql + ' AND s.TotalAmount >= @xTotalFrom'

                	IF @TotalTo IS NOT NULL
                	SET @countsql = @countsql + ' AND s.TotalAmount <= @xTotalTo' 

                	IF @DateFrom IS NOT NULL
                    SET @countsql = @countsql + ' AND CAST(s.SaleDate AS DATE) >= CAST(@xDateFrom AS DATE)';

                	IF @DateTo IS NOT NULL
                		SET @countsql = @countsql + ' AND CAST(s.SaleDate AS DATE) <= CAST(@xDateTo AS DATE)';

                	SELECT @countparamlist = '@xCustomerName nvarchar(max),
                		@xStatusId int,
                		@xTotalFrom int,
                		@xTotalTo int,
                		@xDateFrom datetime,
                		@xDateTo datetime,
                		@TotalDisplay int output' ;

                	exec sp_executesql @countsql , @countparamlist ,
                		@CustomerName,
                		@StatusId,
                		@TotalFrom,
                		@TotalTo,
                	    @DateFrom,
                		@DateTo,
                		@TotalDisplay = @TotalDisplay output;

                	-- Collecting Data
                	SET @sql = 'select * from Sales s where 1 = 1 ';

                	SET @sql = @sql + ' AND s.CustomerName LIKE ''%'' + @xCustomerName + ''%''' 

                	IF @StatusId IS NOT NULL
                	SET @sql = @sql + ' AND s.StatusId = @xStatusId'

                	IF @TotalFrom IS NOT NULL
                	SET @sql = @sql + ' AND s.TotalAmount >= @xTotalFrom'

                	IF @TotalTo IS NOT NULL
                	SET @sql = @sql +  ' AND s.TotalAmount <= @xTotalTo' 

                	IF @DateFrom IS NOT NULL
                    SET @sql = @sql + ' AND CAST(s.SaleDate AS DATE) >= CAST(@xDateFrom AS DATE)';

                	IF @DateTo IS NOT NULL
                		SET @sql = @sql + ' AND CAST(s.SaleDate AS DATE) <= CAST(@xDateTo AS DATE)';

                	SET @sql = @sql + ' Order by '+@OrderBy+' OFFSET @PageSize * (@PageIndex - 1) 
                	ROWS FETCH NEXT @PageSize ROWS ONLY';

                	SELECT @paramlist = '@xCustomerName nvarchar(max),
                		@xStatusId int,
                		@xTotalFrom int,
                		@xTotalTo int,
                		@xDateFrom datetime,
                        @xDateTo datetime,
                		@PageIndex int,
                		@PageSize int' ;

                	exec sp_executesql @sql , @paramlist ,
                		@CustomerName,
                		@StatusId,
                		@TotalFrom,
                		@TotalTo,
                		@DateFrom,
                		@DateTo,
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
            var sql = "DROP PROCEDURE [dbo].[GetSalesProduct]";

            migrationBuilder.Sql(sql);
        }
    }
}
