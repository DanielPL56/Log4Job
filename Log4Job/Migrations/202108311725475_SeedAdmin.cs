namespace Log4Job.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdmin : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ac87b79f-b4ab-4a05-b964-52bafe4fc43b', N'admin@Log4Net.com', 0, N'AF/pi+8Pjcopa9Ph77oQI0xw/WPndAqJ1+s7A3vGNJo/atVXQU6Yz2Zc+moH6HfNmg==', N'95a0daf4-2a73-4eef-98fd-f96091ba7633', NULL, 0, 0, NULL, 1, 0, N'admin@Log4Net.com')");

            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b7405e31-bfff-4226-b024-e4d0c7b52fdf', N'Log4JobAdmin')");

            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ac87b79f-b4ab-4a05-b964-52bafe4fc43b', N'b7405e31-bfff-4226-b024-e4d0c7b52fdf')");
        }
        
        public override void Down()
        {
        }
    }
}
