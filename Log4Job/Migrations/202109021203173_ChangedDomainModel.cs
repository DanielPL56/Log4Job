namespace Log4Job.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDomainModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Employee_EmployeeId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Employee_EmployeeId");
            AddForeignKey("dbo.AspNetUsers", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.AspNetUsers", new[] { "Employee_EmployeeId" });
            DropColumn("dbo.AspNetUsers", "Employee_EmployeeId");
        }
    }
}
