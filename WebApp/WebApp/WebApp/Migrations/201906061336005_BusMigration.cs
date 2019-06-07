namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BusMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Timetables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimetableTypeId = c.Int(nullable: false),
                        DayTypeId = c.Int(nullable: false),
                        BusLine_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayTypes", t => t.DayTypeId, cascadeDelete: true)
                .ForeignKey("dbo.TimetableTypes", t => t.TimetableTypeId, cascadeDelete: true)
                .ForeignKey("dbo.BusLines", t => t.BusLine_Id)
                .Index(t => t.TimetableTypeId)
                .Index(t => t.DayTypeId)
                .Index(t => t.BusLine_Id);
            
            CreateTable(
                "dbo.TimetableTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        LineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusLines", t => t.LineId, cascadeDelete: true)
                .Index(t => t.LineId);
            
            CreateTable(
                "dbo.PriceLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FinalPrice = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        PricelistId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceLists", t => t.PricelistId, cascadeDelete: true)
                .ForeignKey("dbo.TicketTypes", t => t.TicketTypeId, cascadeDelete: true)
                .Index(t => t.PricelistId)
                .Index(t => t.TicketTypeId);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StationBusLines",
                c => new
                    {
                        Station_Id = c.Int(nullable: false),
                        BusLine_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Station_Id, t.BusLine_Id })
                .ForeignKey("dbo.Stations", t => t.Station_Id, cascadeDelete: true)
                .ForeignKey("dbo.BusLines", t => t.BusLine_Id, cascadeDelete: true)
                .Index(t => t.Station_Id)
                .Index(t => t.BusLine_Id);
            
            AddColumn("dbo.AspNetUsers", "TypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "TypeId");
            AddForeignKey("dbo.AspNetUsers", "TypeId", "dbo.UserTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketPrices", "TicketTypeId", "dbo.TicketTypes");
            DropForeignKey("dbo.TicketPrices", "PricelistId", "dbo.PriceLists");
            DropForeignKey("dbo.AspNetUsers", "TypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Tickets", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vehicles", "LineId", "dbo.BusLines");
            DropForeignKey("dbo.Timetables", "BusLine_Id", "dbo.BusLines");
            DropForeignKey("dbo.StationBusLines", "BusLine_Id", "dbo.BusLines");
            DropForeignKey("dbo.StationBusLines", "Station_Id", "dbo.Stations");
            DropForeignKey("dbo.Timetables", "TimetableTypeId", "dbo.TimetableTypes");
            DropForeignKey("dbo.Timetables", "DayTypeId", "dbo.DayTypes");
            DropIndex("dbo.StationBusLines", new[] { "BusLine_Id" });
            DropIndex("dbo.StationBusLines", new[] { "Station_Id" });
            DropIndex("dbo.TicketPrices", new[] { "TicketTypeId" });
            DropIndex("dbo.TicketPrices", new[] { "PricelistId" });
            DropIndex("dbo.AspNetUsers", new[] { "TypeId" });
            DropIndex("dbo.Tickets", new[] { "User_Id" });
            DropIndex("dbo.Vehicles", new[] { "LineId" });
            DropIndex("dbo.Timetables", new[] { "BusLine_Id" });
            DropIndex("dbo.Timetables", new[] { "DayTypeId" });
            DropIndex("dbo.Timetables", new[] { "TimetableTypeId" });
            DropColumn("dbo.AspNetUsers", "TypeId");
            DropTable("dbo.StationBusLines");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.TicketPrices");
            DropTable("dbo.UserTypes");
            DropTable("dbo.Tickets");
            DropTable("dbo.PriceLists");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Stations");
            DropTable("dbo.BusLines");
            DropTable("dbo.TimetableTypes");
            DropTable("dbo.Timetables");
            DropTable("dbo.DayTypes");
        }
    }
}
