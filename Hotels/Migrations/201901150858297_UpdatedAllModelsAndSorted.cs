namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAllModelsAndSorted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.Items", "ServiceProduct_Id", "dbo.ServiceProducts");
            DropIndex("dbo.Items", new[] { "Invoice_Id" });
            DropIndex("dbo.Items", new[] { "ServiceProduct_Id" });
            RenameColumn(table: "dbo.Items", name: "Invoice_Id", newName: "InvoiceId");
            RenameColumn(table: "dbo.Items", name: "ServiceProduct_Id", newName: "ServiceProductId");
            RenameColumn(table: "dbo.Rooms", name: "RoomType_Id", newName: "RoomTypeId");
            RenameIndex(table: "dbo.Rooms", name: "IX_RoomType_Id", newName: "IX_RoomTypeId");
            AddColumn("dbo.Invoices", "ItemId", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Items", "InvoiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Items", "ServiceProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Items", "InvoiceId");
            CreateIndex("dbo.Items", "ServiceProductId");
            AddForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Items", "ServiceProductId", "dbo.ServiceProducts", "Id", cascadeDelete: true);
            DropColumn("dbo.Items", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Amount", c => c.Int(nullable: false));
            DropForeignKey("dbo.Items", "ServiceProductId", "dbo.ServiceProducts");
            DropForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "ServiceProductId" });
            DropIndex("dbo.Items", new[] { "InvoiceId" });
            AlterColumn("dbo.Items", "ServiceProductId", c => c.Int());
            AlterColumn("dbo.Items", "InvoiceId", c => c.Int());
            DropColumn("dbo.Items", "Quantity");
            DropColumn("dbo.Invoices", "ItemId");
            RenameIndex(table: "dbo.Rooms", name: "IX_RoomTypeId", newName: "IX_RoomType_Id");
            RenameColumn(table: "dbo.Rooms", name: "RoomTypeId", newName: "RoomType_Id");
            RenameColumn(table: "dbo.Items", name: "ServiceProductId", newName: "ServiceProduct_Id");
            RenameColumn(table: "dbo.Items", name: "InvoiceId", newName: "Invoice_Id");
            CreateIndex("dbo.Items", "ServiceProduct_Id");
            CreateIndex("dbo.Items", "Invoice_Id");
            AddForeignKey("dbo.Items", "ServiceProduct_Id", "dbo.ServiceProducts", "Id");
            AddForeignKey("dbo.Items", "Invoice_Id", "dbo.Invoices", "Id");
        }
    }
}
