namespace Log4Job.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.TimeReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectEmployees",
                c => new
                    {
                        Project_ProjectId = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_ProjectId, t.Employee_EmployeeId })
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId, cascadeDelete: true)
                .Index(t => t.Project_ProjectId)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.TimeReportEmployees",
                c => new
                    {
                        TimeReport_Id = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimeReport_Id, t.Employee_EmployeeId })
                .ForeignKey("dbo.TimeReports", t => t.TimeReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId, cascadeDelete: true)
                .Index(t => t.TimeReport_Id)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.TimeReportProjects",
                c => new
                    {
                        TimeReport_Id = c.Int(nullable: false),
                        Project_ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimeReport_Id, t.Project_ProjectId })
                .ForeignKey("dbo.TimeReports", t => t.TimeReport_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId, cascadeDelete: true)
                .Index(t => t.TimeReport_Id)
                .Index(t => t.Project_ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeReportProjects", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.TimeReportProjects", "TimeReport_Id", "dbo.TimeReports");
            DropForeignKey("dbo.TimeReportEmployees", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.TimeReportEmployees", "TimeReport_Id", "dbo.TimeReports");
            DropForeignKey("dbo.ProjectEmployees", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ProjectEmployees", "Project_ProjectId", "dbo.Projects");
            DropIndex("dbo.TimeReportProjects", new[] { "Project_ProjectId" });
            DropIndex("dbo.TimeReportProjects", new[] { "TimeReport_Id" });
            DropIndex("dbo.TimeReportEmployees", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.TimeReportEmployees", new[] { "TimeReport_Id" });
            DropIndex("dbo.ProjectEmployees", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.ProjectEmployees", new[] { "Project_ProjectId" });
            DropTable("dbo.TimeReportProjects");
            DropTable("dbo.TimeReportEmployees");
            DropTable("dbo.ProjectEmployees");
            DropTable("dbo.TimeReports");
            DropTable("dbo.Projects");
            DropTable("dbo.Employees");
        }
    }
}
