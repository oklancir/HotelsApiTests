namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredFromRoomProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Name", c => c.String(nullable: false));
        }
    }
}
