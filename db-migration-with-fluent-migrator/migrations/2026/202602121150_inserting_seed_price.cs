using FluentMigrator;

namespace Migration2026;

[Migration(version: 202602121150, description: "insert seed data to price table")]
public class InsertSeedPriceToPriceTable : Migration
{
    public override void Up()
    {
        var prices = new[] {
          new { value = 60, date = DateTime.Now, code = "ALTM" },
          new { value = 10, date = DateTime.Now, code = "BLKM" },
          new { value = 20, date = DateTime.Now, code = "BKSI" },
          new { value = 50, date = DateTime.Now, code = "JAWI" },
        };

        Insert.IntoTable("price").Rows(prices);
    }

    public override void Down()
    {
        Delete.FromTable("price").AllRows();
    }
}
