namespace SimpleBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BankAccountModels", newName: "BankAccounts");
            DropColumn("dbo.BankAccounts", "FirstName");
            DropColumn("dbo.BankAccounts", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccounts", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.BankAccounts", "FirstName", c => c.String(nullable: false));
            RenameTable(name: "dbo.BankAccounts", newName: "BankAccountModels");
        }
    }
}
