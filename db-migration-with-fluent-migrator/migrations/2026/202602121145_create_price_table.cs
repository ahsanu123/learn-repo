using FluentMigrator;

namespace Migration2026;

[Migration(version: 202602121145, description: "create price table")]
public class CreatePriceTable : Migration
{
    public override void Up()
    {
        Create.Table("price")
          .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
          .WithColumn("value").AsInt64().NotNullable()
          .WithColumn("date").AsDateTime().NotNullable()
          .WithColumn("code").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("price");
    }
}
