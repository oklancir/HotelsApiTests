namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedInvoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "Reservation_Id", "dbo.Reservations");
            DropIndex("dbo.Invoices", new[] { "Reservation_Id" });
            RenameColumn(table: "dbo.Invoices", name: "Reservation_Id", newName: "ReservationId");
            AlterColumn("dbo.Invoices", "ReservationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoices", "ReservationId");
            AddForeignKey("dbo.Invoices", "ReservationId", "dbo.Reservations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "ReservationId", "dbo.Reservations");
            DropIndex("dbo.Invoices", new[] { "ReservationId" });
            AlterColumn("dbo.Invoices", "ReservationId", c => c.Int());
            RenameColumn(table: "dbo.Invoices", name: "ReservationId", newName: "Reservation_Id");
            CreateIndex("dbo.Invoices", "Reservation_Id");
            AddForeignKey("dbo.Invoices", "Reservation_Id", "dbo.Reservations", "Id");
        }
    }
}
