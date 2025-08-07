using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class addGetProductStoreProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """
                 CREATE OR   ALTER   PROCEDURE [dbo].[GetProducts] 
                	@PageIndex int = 1,
                	@PageSize int = 10, 
                	@OrderBy nvarchar(50) = '',
                	@Name nvarchar(max) = '%',
                	@BarCode nvarchar(max) = '%',
                	@Category nvarchar(max) = '%',
                	@MRPFrom int = NULL,
                	@MRPTo int = NULL,
                	@StockFrom int = NULL,
                	@StockTo int = NULL,
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
                	SET @countsql = 'select @TotalDisplay = count(*) from Products p where 1 = 1 ';

                	SET @countsql = @countsql + ' AND p.Name LIKE ''%'' + @xName + ''%''' 

                	SET @countsql = @countsql + ' AND p.BarCode LIKE ''%'' + @xBarCode + ''%''' 

                	SET @countsql = @countsql + ' AND p.CategoryName LIKE ''%'' + @xCategory + ''%''' 

                	IF @MRPFrom IS NOT NULL
                	SET @countsql = @countsql + ' AND p.MRPPrice >= @xMRPFrom'

                	IF @MRPTo IS NOT NULL
                	SET @countsql = @countsql + ' AND p.MRPPrice <= @xMRPTo' 

                	IF @StockFrom IS NOT NULL
                	SET @countsql = @countsql + ' AND p.Stock >= @xStockFrom'

                	IF @StockTo IS NOT NULL
                	SET @countsql = @countsql + ' AND p.Stock <= @xStockTo' 

                	SELECT @countparamlist = '@xName nvarchar(max),
                		@xBarCode nvarchar(max),
                		@xCategory nvarchar(max),
                		@xMRPFrom int,
                		@xMRPTo int,
                		@xStockFrom int,
                		@xStockTo int,
                		@TotalDisplay int output' ;

                	exec sp_executesql @countsql , @countparamlist ,
                		@Name,
                		@BarCode,
                		@Category,
                		@MRPFrom,
                		@MRPTo,
                		@StockFrom,
                		@StockTo,
                		@TotalDisplay = @TotalDisplay output;

                	-- Collecting Data
                	SET @sql = 'select * from Products p where 1 = 1 ';

                	SET @sql = @sql + ' AND p.Name LIKE ''%'' + @xName + ''%''' 

                	SET @sql = @sql + ' AND p.BarCode LIKE ''%'' + @xBarCode + ''%''' 

                	SET @sql = @sql + ' AND p.CategoryName LIKE ''%'' + @xCategory + ''%''' 

                	IF @MRPFrom IS NOT NULL
                	SET @sql = @sql + ' AND p.MRPPrice >= @xMRPFrom'

                	IF @MRPTo IS NOT NULL
                	SET @sql = @sql + ' AND p.MRPPrice <= @xMRPTo' 

                	IF @StockFrom IS NOT NULL
                	SET @sql = @sql + ' AND p.Stock >= @xStockFrom'

                	IF @StockTo IS NOT NULL
                	SET @sql = @sql +  ' AND p.Stock <= @xStockTo' 

                	SET @sql = @sql + ' Order by '+@OrderBy+' OFFSET @PageSize * (@PageIndex - 1) 
                	ROWS FETCH NEXT @PageSize ROWS ONLY';

                	SELECT @paramlist = '@xName nvarchar(max),
                		@xBarCode nvarchar(max),
                		@xCategory nvarchar(max),
                		@xMRPFrom int,
                		@xMRPTo int,
                		@xStockFrom int,
                		@xStockTo int,
                		@PageIndex int,
                		@PageSize int' ;

                	exec sp_executesql @sql , @paramlist ,
                		@Name,
                		@BarCode,
                		@Category,
                		@MRPFrom,
                		@MRPTo,
                		@StockFrom,
                		@StockTo,
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
            var sql = "DROP PROCEDURE [dbo].[GetProducts]";

            migrationBuilder.Sql(sql);
        }
    }
}
