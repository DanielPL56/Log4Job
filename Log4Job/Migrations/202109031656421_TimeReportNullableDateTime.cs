namespace Log4Job.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeReportNullableDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeReports", "StartDate", c => c.DateTime());
            AlterColumn("dbo.TimeReports", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeReports", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TimeReports", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
