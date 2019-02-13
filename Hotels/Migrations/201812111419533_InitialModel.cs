namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalAmount = c.Double(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        Reservation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id)
                .Index(t => t.Reservation_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discount = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Guest_Id = c.Int(),
                        ReservationStatus_Id = c.Int(),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guests", t => t.Guest_Id)
                .ForeignKey("dbo.ReservationStatus", t => t.ReservationStatus_Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Guest_Id)
                .Index(t => t.ReservationStatus_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.ReservationStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoomType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomType_Id)
                .Index(t => t.RoomType_Id);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Invoice_Id = c.Int(),
                        ServiceProduct_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id)
                .ForeignKey("dbo.ServiceProducts", t => t.ServiceProduct_Id)
                .Index(t => t.Invoice_Id)
                .Index(t => t.ServiceProduct_Id);
            
            CreateTable(
                "dbo.ServiceProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ServiceProduct_Id", "dbo.ServiceProducts");
            DropForeignKey("dbo.Items", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "RoomType_Id", "dbo.RoomTypes");
            DropForeignKey("dbo.Reservations", "ReservationStatus_Id", "dbo.ReservationStatus");
            DropForeignKey("dbo.Reservations", "Guest_Id", "dbo.Guests");
            DropIndex("dbo.Items", new[] { "ServiceProduct_Id" });
            DropIndex("dbo.Items", new[] { "Invoice_Id" });
            DropIndex("dbo.Rooms", new[] { "RoomType_Id" });
            DropIndex("dbo.Reservations", new[] { "Room_Id" });
            DropIndex("dbo.Reservations", new[] { "ReservationStatus_Id" });
            DropIndex("dbo.Reservations", new[] { "Guest_Id" });
            DropIndex("dbo.Invoices", new[] { "Reservation_Id" });
            DropTable("dbo.ServiceProducts");
            DropTable("dbo.Items");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.Rooms");
            DropTable("dbo.ReservationStatus");
            DropTable("dbo.Reservations");
            DropTable("dbo.Invoices");
            DropTable("dbo.Guests");
        }
    }
}
