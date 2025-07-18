using FluentMigrator;

namespace DesafioAEVO.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.VERSION_INITIAL, "Create tables to save the product's, order's and orderItem's information's")]
    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Products")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Price").AsDecimal(18, 2).NotNullable();

            CreateTable("Orders")
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("Status").AsString(50).NotNullable();

            CreateTable("OrderItems")
                .WithColumn("OrderID").AsGuid().NotNullable().ForeignKey("Orders", "ID")
                .WithColumn("ProductID").AsGuid().NotNullable().ForeignKey("Products", "ID")
                .WithColumn("Quantity").AsInt32().NotNullable()
                .WithColumn("UnitPrice").AsDecimal(18, 2).NotNullable();


        }
    }
}
