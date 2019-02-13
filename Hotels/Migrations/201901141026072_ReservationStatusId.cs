namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationStatusId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "ReservationStatus_Id", "dbo.ReservationStatus");
            DropIndex("dbo.Reservations", new[] { "ReservationStatus_Id" });
            RenameColumn(table: "dbo.Reservations", name: "ReservationStatus_Id", newName: "ReservationStatusId");
            AlterColumn("dbo.Reservations", "ReservationStatusId", c => c.Int(nullable: true));
            CreateIndex("dbo.Reservations", "ReservationStatusId");
            AddForeignKey("dbo.Reservations", "ReservationStatusId", "dbo.ReservationStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ReservationStatusId", "dbo.ReservationStatus");
            DropIndex("dbo.Reservations", new[] { "ReservationStatusId" });
            AlterColumn("dbo.Reservations", "ReservationStatusId", c => c.Int());
            RenameColumn(table: "dbo.Reservations", name: "ReservationStatusId", newName: "ReservationStatus_Id");
            CreateIndex("dbo.Reservations", "ReservationStatus_Id");
            AddForeignKey("dbo.Reservations", "ReservationStatus_Id", "dbo.ReservationStatus", "Id");
        }
    }
}
