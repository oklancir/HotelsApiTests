namespace Hotels.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedAllModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "RoomType_Id", "dbo.RoomTypes");
            DropIndex("dbo.Rooms", new[] { "RoomType_Id" });
            AlterColumn("dbo.ReservationStatus", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Rooms", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Rooms", "RoomType_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.RoomTypes", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Rooms", "RoomType_Id");
            AddForeignKey("dbo.Rooms", "RoomType_Id", "dbo.RoomTypes", "Id", cascadeDelete: true);
            Sql("INSERT INTO RoomTypes (Id, Name, Price) VALUES (1, '1 bed', 1500)");
            Sql("INSERT INTO RoomTypes (Id, Name, Price) VALUES (2, '2 bed', 2500)");
            Sql("INSERT INTO RoomTypes (Id, Name, Price) VALUES (3, '3 bed', 3500)");
            Sql("INSERT INTO RoomTypes (Id, Name, Price) VALUES (4, 'Penthouse, 10000')");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "RoomType_Id", "dbo.RoomTypes");
            DropIndex("dbo.Rooms", new[] { "RoomType_Id" });
            AlterColumn("dbo.RoomTypes", "Name", c => c.String());
            AlterColumn("dbo.Rooms", "RoomType_Id", c => c.Int());
            AlterColumn("dbo.Rooms", "Name", c => c.String());
            AlterColumn("dbo.ReservationStatus", "Name", c => c.String());
            CreateIndex("dbo.Rooms", "RoomType_Id");
            AddForeignKey("dbo.Rooms", "RoomType_Id", "dbo.RoomTypes", "Id");
        }
    }
}
