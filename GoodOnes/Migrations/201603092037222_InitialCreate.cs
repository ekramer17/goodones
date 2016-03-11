namespace GoodOnes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Person = c.Int(nullable: false),
                        Question = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => new { t.Person, t.Question })
                .ForeignKey("dbo.Person", t => t.Person, cascadeDelete: true)
                .ForeignKey("dbo.Question", t => t.Question, cascadeDelete: true)
                .Index(t => t.Person)
                .Index(t => t.Question);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 128),
                        LastName = c.String(maxLength: 128),
                        BirthDate = c.DateTime(storeType: "date"),
                        Email = c.String(maxLength: 256),
                        Gender = c.String(maxLength: 8),
                        StripeID = c.String(maxLength: 64),
                        LastLoginTime = c.DateTime(),
                        RegistrationDate = c.DateTime(),
                        RelationshipStatus = c.String(maxLength: 32),
                        HomeZipcode = c.String(maxLength: 32),
                        WorkZipcode = c.String(maxLength: 32),
                        Photo = c.Binary(),
                        Password = c.String(),
                        PersonType = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Live = c.Boolean(nullable: false),
                        Text = c.String(nullable: false),
                        Sequence = c.Int(nullable: false),
                        ControlType = c.String(maxLength: 64),
                        PossibleAnswers = c.String(nullable: false),
                        CanBeBlank = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 128),
                        Date = c.String(maxLength: 10),
                        Line1 = c.String(nullable: false),
                        Line2 = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        MaxCapacity = c.Int(nullable: false),
                        MinCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Event = c.Int(nullable: false),
                        Person = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Event", t => t.Event, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.Person, cascadeDelete: true)
                .Index(t => t.Event)
                .Index(t => t.Person);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "Person", "dbo.Person");
            DropForeignKey("dbo.Reservation", "Event", "dbo.Event");
            DropForeignKey("dbo.Answer", "Question", "dbo.Question");
            DropForeignKey("dbo.Answer", "Person", "dbo.Person");
            DropIndex("dbo.Reservation", new[] { "Person" });
            DropIndex("dbo.Reservation", new[] { "Event" });
            DropIndex("dbo.Answer", new[] { "Question" });
            DropIndex("dbo.Answer", new[] { "Person" });
            DropTable("dbo.Reservation");
            DropTable("dbo.Event");
            DropTable("dbo.Question");
            DropTable("dbo.Person");
            DropTable("dbo.Answer");
        }
    }
}
