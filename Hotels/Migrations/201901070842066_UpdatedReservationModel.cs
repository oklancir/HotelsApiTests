namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedReservationModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Guest_Id", "dbo.Guests");
            DropForeignKey("dbo.Reservations", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "Guest_Id" });
            DropIndex("dbo.Reservations", new[] { "Room_Id" });
            RenameColumn(table: "dbo.Reservations", name: "Guest_Id", newName: "GuestId");
            RenameColumn(table: "dbo.Reservations", name: "Room_Id", newName: "RoomId");
            AlterColumn("dbo.Reservations", "GuestId", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "GuestId");
            CreateIndex("dbo.Reservations", "RoomId");
            AddForeignKey("dbo.Reservations", "GuestId", "dbo.Guests", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Reservations", "GuestId", "dbo.Guests");
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            DropIndex("dbo.Reservations", new[] { "GuestId" });
            AlterColumn("dbo.Reservations", "RoomId", c => c.Int());
            AlterColumn("dbo.Reservations", "GuestId", c => c.Int());
            RenameColumn(table: "dbo.Reservations", name: "RoomId", newName: "Room_Id");
            RenameColumn(table: "dbo.Reservations", name: "GuestId", newName: "Guest_Id");
            CreateIndex("dbo.Reservations", "Room_Id");
            CreateIndex("dbo.Reservations", "Guest_Id");
            AddForeignKey("dbo.Reservations", "Room_Id", "dbo.Rooms", "Id");
            AddForeignKey("dbo.Reservations", "Guest_Id", "dbo.Guests", "Id");
        }
    }
}
