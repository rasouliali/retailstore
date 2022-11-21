using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailStorePrj.Data.Migrations
{
    public partial class RetailStoreSp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_addRetailStore
	@Id uniqueidentifier,
    @Name nvarchar(100),
    @Address nvarchar(80),
    @IpAddress nvarchar(80),
    @OpenningDate datetime2(7),
    @IsActive bit,
    @IsTel bit,
    @ParentId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;


INSERT INTO [dbo].[RetailStores]
           ([Id]
           ,[Name]
           ,[Address]
           ,[IpAddress]
           ,[OpenningDate]
           ,[IsActive]
           ,[IsTel]
           ,[ParentId])
     VALUES
           (@Id ,
    @Name ,
    @Address ,
    @IpAddress ,
    @OpenningDate ,
    @IsActive ,
    @IsTel ,
    @ParentId)
END
");
            migrationBuilder.Sql(@"CREATE PROCEDURE sp_updateRetailStore
	@Id uniqueidentifier,
    @Name nvarchar(100),
    @Address nvarchar(80),
    @IpAddress nvarchar(80),
    @OpenningDate datetime2(7),
    @IsActive bit,
    @IsTel bit,
    @ParentId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;


update [dbo].[RetailStores]
set
    [Name]=isnull(@name,[name])
    ,[Address]=isnull(@Address,[Address])
    ,[IpAddress]=isnull(@IpAddress,[IpAddress])
    ,[OpenningDate]=isnull(@OpenningDate,[OpenningDate])
    ,[IsActive]=isnull(@IsActive,[IsActive])
    ,[IsTel]=isnull(@IsTel,[IsTel])
    ,[ParentId]=isnull(@ParentId,[ParentId])
where id=@Id

END
");
            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_deleteRetailStore
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

delete from [dbo].[RetailStores]
where id=@Id

END
");
            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_getRetailStoreById
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

SELECT Id, Name, Address, IpAddress, OpenningDate, IsActive, IsTel, ParentId
FROM     RetailStores with(nolock)
WHERE  (Id = @Id)

END
");
            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_getRetailStore
AS
BEGIN
	SET NOCOUNT ON;

SELECT Id, Name, Address, IpAddress, OpenningDate, IsActive, IsTel, ParentId
FROM     RetailStores with(nolock)

END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP  PROC sp_addRetailStore");
            migrationBuilder.Sql("DROP  PROC sp_updateRetailStore");
            migrationBuilder.Sql("DROP  PROC sp_deleteRetailStore");
            migrationBuilder.Sql("DROP  PROC sp_getRetailStoreById");
            migrationBuilder.Sql("DROP  PROC sp_getRetailStore");
        }
    }
}
