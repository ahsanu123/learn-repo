using FluentMigrator;

namespace Migration2026;

[Migration(version: 202602121156, description: "create emiten table")]
public class CreateEmitenTable : Migration
{
    public override void Up()
    {
        Create.Table("emiten")
          .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
          .WithColumn("name").AsString().NotNullable()
          .WithColumn("code").AsString().NotNullable()
          .WithColumn("description").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("emiten");
    }
}
