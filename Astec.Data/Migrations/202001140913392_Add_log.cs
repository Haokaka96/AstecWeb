namespace Astec.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_log : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Date", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
           
            DropColumn("dbo.Logs", "Date");
        }
    }
}
