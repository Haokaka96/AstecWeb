namespace Astec.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addemployee1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Phone", c => c.String(maxLength: 50));
            AlterColumn("dbo.Employees", "Address", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Address", c => c.String());
            AlterColumn("dbo.Employees", "Phone", c => c.String());
        }
    }
}
