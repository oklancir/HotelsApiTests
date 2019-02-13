namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRoomIsAvailable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "IsAvailable");
        }
    }
}
