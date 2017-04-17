namespace SimpleBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameChange : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BankAccounts", newName: "BankAccountModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.BankAccountModels", newName: "BankAccounts");
        }
    }
}
