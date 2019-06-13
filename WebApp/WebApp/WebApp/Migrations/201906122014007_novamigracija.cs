namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novamigracija : DbMigration
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
                        BusLineId = c.Int(nullable: false),
                        Times = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusLines", t => t.BusLineId, cascadeDelete: true)
                .ForeignKey("dbo.DayTypes", t => t.DayTypeId, cascadeDelete: true)
                .ForeignKey("dbo.TimetableTypes", t => t.TimetableTypeId, cascadeDelete: true)
                .Index(t => t.TimetableTypeId)
                .Index(t => t.DayTypeId)
                .Index(t => t.BusLineId);
            
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
                "dbo.TimetableTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pricelists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.String(),
                        To = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FinalPrice = c.Double(nullable: false),
                        PricelistId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pricelists", t => t.PricelistId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PricelistId)
                .Index(t => t.UserId);
            
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
                .ForeignKey("dbo.Pricelists", t => t.PricelistId, cascadeDelete: true)
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
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AddColumn("dbo.AspNetUsers", "VerificateAcc", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ConfirmPassword", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "ImageUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "Date", c => c.String());
            CreateIndex("dbo.AspNetUsers", "TypeId");
            AddForeignKey("dbo.AspNetUsers", "TypeId", "dbo.UserTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketPrices", "TicketTypeId", "dbo.TicketTypes");
            DropForeignKey("dbo.TicketPrices", "PricelistId", "dbo.Pricelists");
            DropForeignKey("dbo.AspNetUsers", "TypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Tickets", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "PricelistId", "dbo.Pricelists");
            DropForeignKey("dbo.Timetables", "TimetableTypeId", "dbo.TimetableTypes");
            DropForeignKey("dbo.Timetables", "DayTypeId", "dbo.DayTypes");
            DropForeignKey("dbo.Vehicles", "LineId", "dbo.BusLines");
            DropForeignKey("dbo.Timetables", "BusLineId", "dbo.BusLines");
            DropForeignKey("dbo.StationBusLines", "BusLine_Id", "dbo.BusLines");
            DropForeignKey("dbo.StationBusLines", "Station_Id", "dbo.Stations");
            DropIndex("dbo.StationBusLines", new[] { "BusLine_Id" });
            DropIndex("dbo.StationBusLines", new[] { "Station_Id" });
            DropIndex("dbo.TicketPrices", new[] { "TicketTypeId" });
            DropIndex("dbo.TicketPrices", new[] { "PricelistId" });
            DropIndex("dbo.AspNetUsers", new[] { "TypeId" });
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropIndex("dbo.Tickets", new[] { "PricelistId" });
            DropIndex("dbo.Vehicles", new[] { "LineId" });
            DropIndex("dbo.Timetables", new[] { "BusLineId" });
            DropIndex("dbo.Timetables", new[] { "DayTypeId" });
            DropIndex("dbo.Timetables", new[] { "TimetableTypeId" });
            DropColumn("dbo.AspNetUsers", "Date");
            DropColumn("dbo.AspNetUsers", "ImageUrl");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "ConfirmPassword");
            DropColumn("dbo.AspNetUsers", "VerificateAcc");
            DropColumn("dbo.AspNetUsers", "Password");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "TypeId");
            DropTable("dbo.StationBusLines");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.TicketPrices");
            DropTable("dbo.UserTypes");
            DropTable("dbo.Tickets");
            DropTable("dbo.Pricelists");
            DropTable("dbo.TimetableTypes");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Stations");
            DropTable("dbo.BusLines");
            DropTable("dbo.Timetables");
            DropTable("dbo.DayTypes");
        }
    }
}
