namespace ASPMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilePathsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        SaleAnnouncementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SaleAnnouncementModels", t => t.SaleAnnouncementId, cascadeDelete: true)
                .Index(t => t.SaleAnnouncementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilePathsModels", "SaleAnnouncementId", "dbo.SaleAnnouncementModels");
            DropIndex("dbo.FilePathsModels", new[] { "SaleAnnouncementId" });
            DropTable("dbo.FilePathsModels");
        }
    }
}
