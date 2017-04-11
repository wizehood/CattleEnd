namespace CattleEnd.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WarriorId = c.Int(nullable: false),
                        GuardDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Warriors", t => t.WarriorId, cascadeDelete: true)
                .Index(t => t.WarriorId);
            
            CreateTable(
                "dbo.Warriors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpeciesId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WarriorSpecies", t => t.SpeciesId, cascadeDelete: true)
                .Index(t => t.SpeciesId);
            
            CreateTable(
                "dbo.WarriorSpecies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "WarriorId", "dbo.Warriors");
            DropForeignKey("dbo.Warriors", "SpeciesId", "dbo.WarriorSpecies");
            DropIndex("dbo.Warriors", new[] { "SpeciesId" });
            DropIndex("dbo.Schedules", new[] { "WarriorId" });
            DropTable("dbo.WarriorSpecies");
            DropTable("dbo.Warriors");
            DropTable("dbo.Schedules");
        }
    }
}
