using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGetAuthorsProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """
                     CREATE OR ALTER PROCEDURE GetAuthors
                	 @PageIndex int,
                	 @PageSize int,
                	 @OrderBy nvarchar(50),
                	 @Name nvarchar(max) = '%',
                	 @Biography nvarchar(max) = '%',
                	 @RatingFrom int = null,
                	 @RatingTo int = null,
                	 @Total int output,
                	 @TotalDisplay int output
                AS

                BEGIN
                	SET NOCOUNT ON;

                	Declare @sql nvarchar(2000);
                	Declare @countSql nvarchar(2000);
                	Declare @paramList nvarchar(MAX);
                	Declare @countParamList nvarchar(MAX);

                	-- Collecting Total
                	Select @Total = count(*) from Authors;

                	-- Counting total display
                	SET @countSql = ' SELECT @TotalDisplay = count(*)  from Authors as a WHERE 1 = 1 ';
                	SET @countSql = @countSql +' AND a.Name like ''%'' + @xName + ''%'' ' ;
                	SET @countSql = @countSql + ' AND a.Biography like ''%'' + @xBiography + ''%'' ';

                	IF @RatingFrom IS NOT NULL
                	SET @countSql =  @countSql + ' AND a.Rating >= @xRatingFrom ';

                	IF @RatingTo IS NOT NULL
                	SET	@countSql = @countSql + ' AND a.Rating <= @xRatingTo ';


                	SELECT @countParamlist ='@xName nvarchar(max),
                		@xBiography nvarchar(max),
                		@xRatingFrom int,
                		@xRatingTo int,
                		@TotalDisplay int output';

                	exec sp_executesql @countSql, @countParamlist,
                		@Name,
                		@Biography,
                		@RatingFrom,
                		@RatingTo,
                		@TotalDisplay = @TotalDisplay output;


                		-- Collecting data

                		SET @sql = ' select* from Authors as a where 1 = 1 ';
                		SET @sql = @sql + ' AND a.Name like ''%'' + @xName + ''%'' ' ;
                		SET @sql = @sql +  ' AND a.Biography like ''%'' + @xBiography + ''%'' ';

                		IF @RatingFrom IS NOT NULL
                		SET @sql = @sql + ' AND a.Rating >= @xRatingFrom ';

                		IF @RatingTo IS NOT NULL
                		SET @sql = @sql + ' AND a.Rating <= @xRatingTo ';


                			SET @sql = @sql + ' Order by '+@OrderBy+' OFFSET @PageSize * (@PageIndex - 1) 
                				ROWS FETCH NEXT @PageSize ROWS ONLY';

                		SELECT @paramlist = '@xName nvarchar(max),
                		@xBiography nvarchar(max),
                		@xRatingFrom int,
                		@xRatingTo int,
                		@PageIndex int,
                		@PageSize int' ;

                		exec sp_executesql @sql , @paramlist ,
                			@Name,
                			@Biography,
                			@RatingFrom,
                			@RatingTo,
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
            var sql = "DROP PROCEDURE [dbo].[GetAuthors]";

            migrationBuilder.Sql(sql);
        }
    }
}
