namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paypal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "TransactionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "TransactionId");
        }
    }
}
