namespace Astec.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addemployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        ImageName = c.String(maxLength: 50),
                        Image = c.Binary(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Phone = c.String(),
                        Address = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
