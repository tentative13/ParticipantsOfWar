namespace ParticipantsOfWar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        DocumentId = c.Guid(nullable: false, identity: true),
                        DocumentBytes = c.Binary(),
                        Extension = c.String(),
                        type_DocumentTypeId = c.Guid(),
                        Participant_ParticipantId = c.Guid(),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.DocumentType", t => t.type_DocumentTypeId)
                .ForeignKey("dbo.Participant", t => t.Participant_ParticipantId)
                .Index(t => t.type_DocumentTypeId)
                .Index(t => t.Participant_ParticipantId);
            
            CreateTable(
                "dbo.DocumentType",
                c => new
                    {
                        DocumentTypeId = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.Participant",
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
                .ForeignKey("dbo.ParticipantType", t => t.type_ParticipantTypeId)
                .Index(t => t.type_ParticipantTypeId);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        PhotoId = c.Guid(nullable: false, identity: true),
                        PhotoBytes = c.Binary(),
                        Extension = c.String(),
                        Description = c.String(),
                        Participant_ParticipantId = c.Guid(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.Participant", t => t.Participant_ParticipantId)
                .Index(t => t.Participant_ParticipantId);
            
            CreateTable(
                "dbo.ParticipantType",
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
            DropForeignKey("dbo.Participant", "type_ParticipantTypeId", "dbo.ParticipantType");
            DropForeignKey("dbo.Photo", "Participant_ParticipantId", "dbo.Participant");
            DropForeignKey("dbo.Document", "Participant_ParticipantId", "dbo.Participant");
            DropForeignKey("dbo.Document", "type_DocumentTypeId", "dbo.DocumentType");
            DropIndex("dbo.Photo", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.Participant", new[] { "type_ParticipantTypeId" });
            DropIndex("dbo.Document", new[] { "Participant_ParticipantId" });
            DropIndex("dbo.Document", new[] { "type_DocumentTypeId" });
            DropTable("dbo.ParticipantType");
            DropTable("dbo.Photo");
            DropTable("dbo.Participant");
            DropTable("dbo.DocumentType");
            DropTable("dbo.Document");
        }
    }
}
