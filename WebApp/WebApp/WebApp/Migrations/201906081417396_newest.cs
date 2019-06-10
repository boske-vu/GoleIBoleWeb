namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TypeOfUser", c => c.String());
            DropColumn("dbo.AspNetUsers", "Tip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Tip", c => c.String());
            DropColumn("dbo.AspNetUsers", "TypeOfUser");
        }
    }
}
