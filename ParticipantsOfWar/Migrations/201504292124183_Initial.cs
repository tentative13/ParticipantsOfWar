namespace ParticipantsOfWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Guid(nullable: false, identity: true),
                        DocumentBytes = c.Binary(),
                        Extension = c.String(),
                        type_DocumentTypeId = c.Guid(),
                        Participant_ParticipantId = c.Guid(),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.DocumentTypes", t => t.type_DocumentTypeId)
                .ForeignKey("dbo.Participants", t => t.Participant_ParticipantId)
                .Index(t => t.type_DocumentTypeId)
                .Index(t => t.Participant_ParticipantId);
            
            CreateTable(
                "dbo.DocumentTypes",
                c => new
                    {
                        DocumentTypeId = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ParticipantId = c.Guid(nullable: false, identity: true),
                        Surname = c.String(),
                        Firstname = c.String(),
                        Middlename = c.String(),
                        ShortName = c.String(),
                        Birthday = c.DateTime(),
                        Deathday = c.DateTime(),
                        Description = c.String(),
                        type_ParticipantTypeId = c.Guid(),
                    })
                .PrimaryKey(t => t.ParticipantId)
                .ForeignKey("dbo.ParticipantTypes", t => t.type_ParticipantTypeId)
                .Index(t => t.type_ParticipantTypeId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Guid(nullable: false, identity: true),
                        PhotoBytes = c.Binary(),
                        Extension = c.String(),
                        Description = c.String(),
                        Participant_ParticipantId = c.Guid(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.Participants", t => t.Participant_ParticipantId)
                .Index(t => t.Participant_ParticipantId);
            
            CreateTable(
                "dbo.ParticipantTypes",
                c => new
                    {
                        ParticipantTypeId = c.Guid(nullable: false, identity: true),
                        Priority = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ParticipantTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participants", "type_ParticipantTypeId", "dbo.ParticipantTypes");
            DropForeignKey("dbo.Photos", "Participant_ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Documents", "Participant_ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Documents", "type_DocumentTypeId", "dbo.DocumentTypes");
            DropIndex("dbo.Photos", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.Participants", new[] { "type_ParticipantTypeId" });
            DropIndex("dbo.Documents", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.Documents", new[] { "type_DocumentTypeId" });
            DropTable("dbo.ParticipantTypes");
            DropTable("dbo.Photos");
            DropTable("dbo.Participants");
            DropTable("dbo.DocumentTypes");
            DropTable("dbo.Documents");
        }
    }
}
