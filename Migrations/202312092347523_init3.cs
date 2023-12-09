namespace SystemyBazDanychP1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SupportChatModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SupportChatModels", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.SupportChatModels", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.SupportChatModels", "ApplicationUser_Id", c => c.String());
            CreateIndex("dbo.SupportChatModels", "ApplicationUser_Id1");
            AddForeignKey("dbo.SupportChatModels", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupportChatModels", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.SupportChatModels", new[] { "ApplicationUser_Id1" });
            AlterColumn("dbo.SupportChatModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.SupportChatModels", "ApplicationUser_Id1");
            CreateIndex("dbo.SupportChatModels", "ApplicationUser_Id");
            AddForeignKey("dbo.SupportChatModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
