using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace DesafioAEVO.Infrastructure.Migrations.Versions
{
    public abstract class VersionBase : ForwardOnlyMigration
    {
        protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
        {
            return Create.Table(table).WithColumn("ID").AsGuid().PrimaryKey().NotNullable();
        }
    }
}
