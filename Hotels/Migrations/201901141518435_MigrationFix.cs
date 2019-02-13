namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "ReservationStatus_Id", "dbo.ReservationStatus");
            DropIndex("dbo.Invoices", new[] { "Reservation_Id" });
            DropIndex("dbo.Reservations", new[] { "ReservationStatus_Id" });
            RenameColumn(table: "dbo.Invoices", name: "Reservation_Id", newName: "ReservationId");
            RenameColumn(table: "dbo.Reservations", name: "ReservationStatus_Id", newName: "ReservationStatusId");
            AlterColumn("dbo.Invoices", "ReservationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "ReservationStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoices", "ReservationId");
            CreateIndex("dbo.Reservations", "ReservationStatusId");
            AddForeignKey("dbo.Invoices", "ReservationId", "dbo.Reservations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "ReservationStatusId", "dbo.ReservationStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ReservationStatusId", "dbo.ReservationStatus");
            DropForeignKey("dbo.Invoices", "ReservationId", "dbo.Reservations");
            DropIndex("dbo.Reservations", new[] { "ReservationStatusId" });
            DropIndex("dbo.Invoices", new[] { "ReservationId" });
            AlterColumn("dbo.Reservations", "ReservationStatusId", c => c.Int());
            AlterColumn("dbo.Invoices", "ReservationId", c => c.Int());
            RenameColumn(table: "dbo.Reservations", name: "ReservationStatusId", newName: "ReservationStatus_Id");
            RenameColumn(table: "dbo.Invoices", name: "ReservationId", newName: "Reservation_Id");
            CreateIndex("dbo.Reservations", "ReservationStatus_Id");
            CreateIndex("dbo.Invoices", "Reservation_Id");
            AddForeignKey("dbo.Reservations", "ReservationStatus_Id", "dbo.ReservationStatus", "Id");
            AddForeignKey("dbo.Invoices", "Reservation_Id", "dbo.Reservations", "Id");
        }
    }
}
