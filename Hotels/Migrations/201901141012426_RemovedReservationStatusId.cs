namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedReservationStatusId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "ReservationStatusId", "dbo.ReservationStatus");
            DropIndex("dbo.Reservations", new[] { "ReservationStatusId" });
            RenameColumn(table: "dbo.Reservations", name: "ReservationStatusId", newName: "ReservationStatus_Id");
            AlterColumn("dbo.Reservations", "ReservationStatus_Id", c => c.Int());
            CreateIndex("dbo.Reservations", "ReservationStatus_Id");
            AddForeignKey("dbo.Reservations", "ReservationStatus_Id", "dbo.ReservationStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ReservationStatus_Id", "dbo.ReservationStatus");
            DropIndex("dbo.Reservations", new[] { "ReservationStatus_Id" });
            AlterColumn("dbo.Reservations", "ReservationStatus_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Reservations", name: "ReservationStatus_Id", newName: "ReservationStatusId");
            CreateIndex("dbo.Reservations", "ReservationStatusId");
            AddForeignKey("dbo.Reservations", "ReservationStatusId", "dbo.ReservationStatus", "Id", cascadeDelete: true);
        }
    }
}
