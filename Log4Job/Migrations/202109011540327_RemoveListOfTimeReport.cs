namespace Log4Job.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveListOfTimeReport : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeReportEmployees", "TimeReport_Id", "dbo.TimeReports");
            DropForeignKey("dbo.TimeReportEmployees", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.TimeReportProjects", "TimeReport_Id", "dbo.TimeReports");
            DropForeignKey("dbo.TimeReportProjects", "Project_ProjectId", "dbo.Projects");
            DropIndex("dbo.TimeReportEmployees", new[] { "TimeReport_Id" });
            DropIndex("dbo.TimeReportEmployees", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.TimeReportProjects", new[] { "TimeReport_Id" });
            DropIndex("dbo.TimeReportProjects", new[] { "Project_ProjectId" });
            AddColumn("dbo.TimeReports", "Employee_EmployeeId", c => c.Int());
            AddColumn("dbo.TimeReports", "Project_ProjectId", c => c.Int());
            CreateIndex("dbo.TimeReports", "Employee_EmployeeId");
            CreateIndex("dbo.TimeReports", "Project_ProjectId");
            AddForeignKey("dbo.TimeReports", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
            AddForeignKey("dbo.TimeReports", "Project_ProjectId", "dbo.Projects", "ProjectId");
            DropTable("dbo.TimeReportEmployees");
            DropTable("dbo.TimeReportProjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TimeReportProjects",
                c => new
                    {
                        TimeReport_Id = c.Int(nullable: false),
                        Project_ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimeReport_Id, t.Project_ProjectId });
            
            CreateTable(
                "dbo.TimeReportEmployees",
                c => new
                    {
                        TimeReport_Id = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimeReport_Id, t.Employee_EmployeeId });
            
            DropForeignKey("dbo.TimeReports", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.TimeReports", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.TimeReports", new[] { "Project_ProjectId" });
            DropIndex("dbo.TimeReports", new[] { "Employee_EmployeeId" });
            DropColumn("dbo.TimeReports", "Project_ProjectId");
            DropColumn("dbo.TimeReports", "Employee_EmployeeId");
            CreateIndex("dbo.TimeReportProjects", "Project_ProjectId");
            CreateIndex("dbo.TimeReportProjects", "TimeReport_Id");
            CreateIndex("dbo.TimeReportEmployees", "Employee_EmployeeId");
            CreateIndex("dbo.TimeReportEmployees", "TimeReport_Id");
            AddForeignKey("dbo.TimeReportProjects", "Project_ProjectId", "dbo.Projects", "ProjectId", cascadeDelete: true);
            AddForeignKey("dbo.TimeReportProjects", "TimeReport_Id", "dbo.TimeReports", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TimeReportEmployees", "Employee_EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.TimeReportEmployees", "TimeReport_Id", "dbo.TimeReports", "Id", cascadeDelete: true);
        }
    }
}
