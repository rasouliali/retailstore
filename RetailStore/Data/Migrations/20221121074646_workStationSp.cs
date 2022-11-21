using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailStorePrj.Data.Migrations
{
    public partial class workStationSp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_AddWorkstations
	 @RetailStoreId uniqueidentifier
    ,@Name nvarchar(100)
    ,@Location nvarchar(100)
    ,@IsActive bit
AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO [dbo].[Workstations]
           ([RetailStoreId]
           ,[Name]
           ,[Location]
           ,[IsActive])
     VALUES
           ( @RetailStoreId 
			,@Name 
			,@Location 
			,@IsActive)

END
            ");

            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_UpdateWorkstations
	 @RetailStoreId uniqueidentifier
    ,@Name nvarchar(100)
    ,@Location nvarchar(100)
    ,@IsActive bit
AS
BEGIN
	SET NOCOUNT ON;

update [dbo].[Workstations]
set 
           [Name]=isnull(@Name ,[name])
           ,[Location]=isnull(@Location ,[Location])
           ,[IsActive]=isnull(@IsActive,[IsActive])
where [RetailStoreId] =@RetailStoreId 

END
            ");
            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_DeleteWorkstations
	 @RetailStoreId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

delete from [dbo].[Workstations]
where [RetailStoreId] =@RetailStoreId 

END
            ");
            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_GetWorkstationsById
	 @RetailStoreId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

SELECT Id, RetailStoreId, Name, Location, IsActive
FROM     Workstations with(nolock)
WHERE  (RetailStoreId = @RetailStoreId)

END
            ");
            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_GetWorkstations
AS
BEGIN
	SET NOCOUNT ON;

SELECT Id, RetailStoreId, Name, Location, IsActive
FROM     Workstations with(nolock)

END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP  PROC sp_AddWorkstations");
            migrationBuilder.Sql("DROP  PROC sp_UpdateWorkstations");
            migrationBuilder.Sql("DROP  PROC sp_DeleteWorkstations");
            migrationBuilder.Sql("DROP  PROC sp_GetWorkstationsById");
            migrationBuilder.Sql("DROP  PROC sp_GetWorkstations");
        }
    }
}
