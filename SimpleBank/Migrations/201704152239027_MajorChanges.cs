namespace SimpleBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajorChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankAccountModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BankAccountModels", new[] { "ApplicationUserId" });
            AddColumn("dbo.BankAccountModels", "AccountName", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.BankAccountModels", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BankAccountModels", "ApplicationUserId");
            AddForeignKey("dbo.BankAccountModels", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.BankAccountModels", "AccountNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccountModels", "AccountNumber", c => c.String(maxLength: 10, unicode: false));
            DropForeignKey("dbo.BankAccountModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BankAccountModels", new[] { "ApplicationUserId" });
            AlterColumn("dbo.BankAccountModels", "ApplicationUserId", c => c.String(maxLength: 128));
            DropColumn("dbo.BankAccountModels", "AccountName");
            CreateIndex("dbo.BankAccountModels", "ApplicationUserId");
            AddForeignKey("dbo.BankAccountModels", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
