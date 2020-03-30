namespace Astec.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeStatistic : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetEmployeeStatistic",
                p => new
                {
                    fromDate = p.String(),
                    toDate = p.String()
                },

                @"select  e.Name as 'FullName', e.Gender as 'Gender',
                            e.DateOfBirth as 'DateOfBirth', e.Address as 'Address' 
                            from Employees e 
                            where e.CreatedDate <=CAST(@toDate as date) and e.CreatedDate >= CAST(@fromDate as date)"
                );
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.GetEmployeeStatistic");
        }
    }
}
