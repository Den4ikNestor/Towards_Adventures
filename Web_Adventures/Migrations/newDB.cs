using System.Data.Entity.Migrations;

namespace Web_Adventures.Migrations
{
    public partial class newDB : DbMigration
    {
        public override void Up()
        {
            //Здесь формируется вид БД (задаются столбцы и их тип данных)
            CreateTable(
                "dbo.DbPurchaseTickets",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FilledTime = c.DateTime(nullable: false),
                    BeginPoint = c.String(),
                    EndPoint = c.String(),
                    Price = c.Double(nullable: false, defaultValue: 0),
                    RestaurantFood = c.Boolean(nullable: false, defaultValue: false),
                    Fridge = c.Boolean(nullable: false, defaultValue: false),
                    AdditionalServicePrice = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DbPersonalDatas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Sex = c.String(),
                    DateBirth = c.DateTime(nullable: false),
                    DocumentType = c.String(),
                    Series = c.Int(),
                    Number = c.Int(),
                    DbPurchaseTicket_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbPurchaseTicket", t => t.DbPurchaseTicket_Id)
                .Index(t => t.DbPurchaseTicket_Id);

            CreateTable(
                "dbo.DbNameBuyers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LastName = c.String(),
                    FirstName = c.String(),
                    Patronymic = c.String(),
                    DbPersonalData_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.DbPursonalDatas", "DbPurchaseTicket_Id", "dbo.DbPurchaseTickets");
            DropForeignKey("dbo.DbNameBuyers", "DbPersonalData_Id", "dbo.DbPerosnalDatas");

            DropIndex("dbo.DbPersonalDatas", new[] { "DbPurchaseTicket_Id" });

            DropTable("dbo.DbPurchaseTickets");
            DropTable("dbo.DbPersonalDatas");
            DropTable("dbo.DbNameBuyers");
        }
    }
}