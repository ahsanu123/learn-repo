using System.Data;
using FluentMigrator;

namespace Migration2026;

[Migration(version: 202602121204, description: "altering price.code to use emiten id")]
public class AlteringPriceTableCodeToUseEmitenId : Migration
{
    string fkPriceEmitenId = "FK_price_emiten_id";

    public override void Up()
    {
        Execute.Sql("PRAGMA foreign_keys = ON");

        Alter.Table("price")
          .AddColumn("emiten_id").AsInt64().NotNullable().WithDefaultValue(1);

        // no join in sqlite
        var setEmitenIdUpdateQuery = """
          UPDATE price as p
            SET emiten_id = (
              SELECT id FROM emiten e WHERE e.code = p.code
            )
          """;

        Execute.Sql(setEmitenIdUpdateQuery);


        Create.Table("price_new")
          .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
          .WithColumn("value").AsInt64().NotNullable()
          .WithColumn("date").AsDateTime().NotNullable()
          .WithColumn("code").AsString().NotNullable()
          .WithColumn("emiten_id").AsInt64().NotNullable()
            .ForeignKey("emiten", "id");

        var insertFromOldPrice = """
          INSERT INTO price_new (value, date, code, emiten_id)
          SELECT 
            value, 
            date, 
            code,
            emiten_id
          FROM price
          """;

        Execute.Sql(insertFromOldPrice);

        Delete.Column("code").FromTable("price_new");

        Delete.Table("price");
        Rename.Table("price_new").To("price");
    }

    public override void Down()
    {
        Delete.ForeignKey(fkPriceEmitenId).OnTable("price");
        Delete.Column("emiten_id").FromTable("price");
    }
}
