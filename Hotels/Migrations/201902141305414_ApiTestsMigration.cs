namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiTestsMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Guest", newName: "Guests");
            RenameTable(name: "dbo.Invoice", newName: "Invoices");
            RenameTable(name: "dbo.Item", newName: "Items");
            RenameTable(name: "dbo.ServiceProduct", newName: "ServiceProducts");
            RenameTable(name: "dbo.Reservation", newName: "Reservations");
            RenameTable(name: "dbo.Room", newName: "Rooms");
            RenameTable(name: "dbo.RoomType", newName: "RoomTypes");
            AlterColumn("dbo.Rooms", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Name", c => c.String(nullable: false, maxLength: 50));
            RenameTable(name: "dbo.RoomTypes", newName: "RoomType");
            RenameTable(name: "dbo.Rooms", newName: "Room");
            RenameTable(name: "dbo.Reservations", newName: "Reservation");
            RenameTable(name: "dbo.ServiceProducts", newName: "ServiceProduct");
            RenameTable(name: "dbo.Items", newName: "Item");
            RenameTable(name: "dbo.Invoices", newName: "Invoice");
            RenameTable(name: "dbo.Guests", newName: "Guest");
        }
    }
}
