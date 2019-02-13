namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedReservationModelWithInvoiceIdFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "InvoiceId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "InvoiceId");
        }
    }
}
