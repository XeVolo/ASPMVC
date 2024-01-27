namespace ASPMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZipCode = c.String(maxLength: 6),
                        City = c.String(),
                        StreetAndBuildingNumber = c.String(),
                        ApartmentNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryModels", t => t.ParentCategoryId)
                .Index(t => t.ParentCategoryId);
            
            CreateTable(
                "dbo.DeliveryMethodsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OpinionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.String(maxLength: 128),
                        SaleAnnouncementId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SaleAnnouncementModels", t => t.SaleAnnouncementId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId)
                .Index(t => t.ClientId)
                .Index(t => t.SaleAnnouncementId);
            
            CreateTable(
                "dbo.SaleAnnouncementModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SellerId = c.String(maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Quantity = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductModels", t => t.ProductId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.SellerId)
                .Index(t => t.SellerId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CategoryId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryModels", t => t.CategoryId, cascadeDelete: false)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Surrname = c.String(nullable: false),
                        AddressId = c.Int(nullable: false),
                        IndividualPromotion = c.Double(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AddressModels", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        ClientId = c.String(maxLength: 128),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderModels", t => t.OrderId, cascadeDelete: false)
                .ForeignKey("dbo.ProductModels", t => t.ProductId, cascadeDelete: false)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SupportChatModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.String(maxLength: 128),
                        AdminId = c.String(maxLength: 128),
                        Conversation = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AdminId)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ClientId)
                .Index(t => t.AdminId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.PaymentMethodsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SpecialOfferModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleAnnouncementId = c.Int(nullable: false),
                        PromotionValue = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SaleAnnouncementModels", t => t.SaleAnnouncementId, cascadeDelete: false)
                .Index(t => t.SaleAnnouncementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpecialOfferModels", "SaleAnnouncementId", "dbo.SaleAnnouncementModels");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SupportChatModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SupportChatModels", "ClientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SupportChatModels", "AdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SaleAnnouncementModels", "SellerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderModels", "ClientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.ProductModels");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.OrderModels");
            DropForeignKey("dbo.OpinionModels", "ClientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AddressId", "dbo.AddressModels");
            DropForeignKey("dbo.SaleAnnouncementModels", "ProductId", "dbo.ProductModels");
            DropForeignKey("dbo.ProductModels", "CategoryId", "dbo.CategoryModels");
            DropForeignKey("dbo.OpinionModels", "SaleAnnouncementId", "dbo.SaleAnnouncementModels");
            DropForeignKey("dbo.CategoryModels", "ParentCategoryId", "dbo.CategoryModels");
            DropIndex("dbo.SpecialOfferModels", new[] { "SaleAnnouncementId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SupportChatModels", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.SupportChatModels", new[] { "AdminId" });
            DropIndex("dbo.SupportChatModels", new[] { "ClientId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderModels", new[] { "ClientId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "AddressId" });
            DropIndex("dbo.ProductModels", new[] { "CategoryId" });
            DropIndex("dbo.SaleAnnouncementModels", new[] { "ProductId" });
            DropIndex("dbo.SaleAnnouncementModels", new[] { "SellerId" });
            DropIndex("dbo.OpinionModels", new[] { "SaleAnnouncementId" });
            DropIndex("dbo.OpinionModels", new[] { "ClientId" });
            DropIndex("dbo.CategoryModels", new[] { "ParentCategoryId" });
            DropTable("dbo.SpecialOfferModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PaymentMethodsModels");
            DropTable("dbo.SupportChatModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.OrderModels");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ProductModels");
            DropTable("dbo.SaleAnnouncementModels");
            DropTable("dbo.OpinionModels");
            DropTable("dbo.DeliveryMethodsModels");
            DropTable("dbo.CategoryModels");
            DropTable("dbo.AddressModels");
        }
    }
}
