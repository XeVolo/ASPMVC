namespace ASPMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderModels", "PaymentMethodId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderModels", "DeliveryMethodId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderModels", "PaymentMethodId");
            CreateIndex("dbo.OrderModels", "DeliveryMethodId");
            AddForeignKey("dbo.OrderModels", "DeliveryMethodId", "dbo.DeliveryMethodsModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderModels", "PaymentMethodId", "dbo.PaymentMethodsModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderModels", "PaymentMethodId", "dbo.PaymentMethodsModels");
            DropForeignKey("dbo.OrderModels", "DeliveryMethodId", "dbo.DeliveryMethodsModels");
            DropIndex("dbo.OrderModels", new[] { "DeliveryMethodId" });
            DropIndex("dbo.OrderModels", new[] { "PaymentMethodId" });
            DropColumn("dbo.OrderModels", "DeliveryMethodId");
            DropColumn("dbo.OrderModels", "PaymentMethodId");
        }
    }
}
