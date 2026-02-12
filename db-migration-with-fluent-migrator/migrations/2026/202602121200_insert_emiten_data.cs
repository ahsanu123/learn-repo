using FluentMigrator;

namespace Migration2026;

[Migration(version: 202602121200, description: "insert emiten data")]
public class InsertEmitenData : Migration
{
    public override void Up()
    {
        var emitenData = new[] {
          new { name = "Alamitriky tbk", code = "ALTM", description = "some description" },
          new { name = "Belakang Langgeng Klimeter tbk", code = "BLKM", description = "some description" },
          new { name = "Bekasi industri", code = "BKSI", description = "some description" },
          new { name = "PT Jawa banget", code = "JAWI", description = "some description" },
        };

        Insert.IntoTable("emiten").Rows(emitenData);
    }

    public override void Down()
    {
        Delete.FromTable("emiten").AllRows();
    }
}
