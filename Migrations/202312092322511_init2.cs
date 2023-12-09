namespace SystemyBazDanychP1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
			AddColumn("dbo.SupportChatModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
		}
        
        public override void Down()
        {
        }
    }
}
