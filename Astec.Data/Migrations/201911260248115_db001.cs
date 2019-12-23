namespace Astec.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(maxLength: 50),
                        Description = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        DistrictName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.FingerPrints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResidentId = c.String(maxLength: 20, unicode: false),
                        Index = c.Int(nullable: false),
                        FingerImage = c.Binary(),
                        Description = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResidentId = c.String(maxLength: 20, unicode: false),
                        Image = c.Binary(),
                        Description = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Residents",
                c => new
                    {
                        ResidentId = c.String(nullable: false, maxLength: 20),
                        ApartmentId = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 50),
                        Age = c.Int(nullable: false),
                        IdCardNumber = c.String(maxLength: 20),
                        Phone = c.String(maxLength: 20),
                        Job = c.String(maxLength: 50),
                        Image = c.String(),
                        Ethnic = c.String(maxLength: 20),
                        Religion = c.String(maxLength: 20),
                        IsHeadOfHousehold = c.Boolean(nullable: false),
                        PermanentResidence = c.String(maxLength: 256),
                        HomeTown = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResidentId)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.Streets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WardId = c.Int(nullable: false),
                        StreetName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wards", t => t.WardId, cascadeDelete: true)
                .Index(t => t.WardId);
            
            CreateTable(
                "dbo.Wards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictId = c.Int(nullable: false),
                        WardName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .Index(t => t.DistrictId);
            
            AddColumn("dbo.Apartments", "Location", c => c.String(maxLength: 50));
            AlterColumn("dbo.Apartments", "ApartmentName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Apartments", "Description", c => c.String(maxLength: 200));
            DropColumn("dbo.Apartments", "InsideBuiding");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Apartments", "InsideBuiding", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Streets", "WardId", "dbo.Wards");
            DropForeignKey("dbo.Wards", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Residents", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.Districts", "CityId", "dbo.Cities");
            DropIndex("dbo.Wards", new[] { "DistrictId" });
            DropIndex("dbo.Streets", new[] { "WardId" });
            DropIndex("dbo.Residents", new[] { "ApartmentId" });
            DropIndex("dbo.Districts", new[] { "CityId" });
            AlterColumn("dbo.Apartments", "Description", c => c.String());
            AlterColumn("dbo.Apartments", "ApartmentName", c => c.String());
            DropColumn("dbo.Apartments", "Location");
            DropTable("dbo.Wards");
            DropTable("dbo.Streets");
            DropTable("dbo.Residents");
            DropTable("dbo.Images");
            DropTable("dbo.FingerPrints");
            DropTable("dbo.Districts");
            DropTable("dbo.Cities");
        }
    }
}
