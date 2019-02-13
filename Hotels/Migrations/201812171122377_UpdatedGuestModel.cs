namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedGuestModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Guests", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Guests", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Guests", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Guests", "Address", c => c.String(nullable: false));
            DropColumn("dbo.Guests", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guests", "Name", c => c.String());
            AlterColumn("dbo.Guests", "Address", c => c.String());
            DropColumn("dbo.Guests", "PhoneNumber");
            DropColumn("dbo.Guests", "Email");
            DropColumn("dbo.Guests", "LastName");
            DropColumn("dbo.Guests", "FirstName");
        }
    }
}
