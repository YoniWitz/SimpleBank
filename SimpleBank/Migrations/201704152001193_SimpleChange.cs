namespace SimpleBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SimpleChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankAccounts", "AccountNumber", c => c.String(maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BankAccounts", "AccountNumber", c => c.String());
        }
    }
}
