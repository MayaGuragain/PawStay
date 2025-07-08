namespace PawStay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingModels",
                c => new
                    {
                        BookingID = c.Guid(nullable: false),
                        BookingStartTime = c.DateTime(nullable: false),
                        BookingEndTime = c.DateTime(nullable: false),
                        ActualBookingStartTime = c.DateTime(),
                        ActualBookingEndTime = c.DateTime(),
                        EmployeeCheckInID = c.Guid(),
                        EmployeeCheckOutID = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        CancellationDate = c.DateTime(),
                        CancellationReason = c.Int(),
                        Status = c.String(nullable: false),
                        PetID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.PetModels", t => t.PetID, cascadeDelete: true)
                .Index(t => t.PetID);
            
            CreateTable(
                "dbo.PetModels",
                c => new
                    {
                        PetID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Age = c.Int(nullable: false),
                        Breed = c.String(nullable: false),
                        DietaryRestrictions = c.String(maxLength: 500),
                        Medication = c.String(maxLength: 500),
                        SpecialInstructions = c.String(maxLength: 1000),
                        EmergencyContactName = c.String(maxLength: 100),
                        EmergencyContactNumber = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.PetID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Age = c.Int(),
                        DateOfBirth = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ContactUsModels",
                c => new
                    {
                        ContactUsID = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Message = c.String(nullable: false, maxLength: 2000),
                        SubmissionDate = c.DateTime(nullable: false),
                        OwnerID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ContactUsID)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "dbo.PetToOwnerModels",
                c => new
                    {
                        PetOwnerID = c.Guid(nullable: false),
                        PetID = c.Guid(nullable: false),
                        OwnerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PetOwnerID)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerID, cascadeDelete: true)
                .ForeignKey("dbo.PetModels", t => t.PetID, cascadeDelete: true)
                .Index(t => t.PetID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.OwnerModelPetModels",
                c => new
                    {
                        OwnerModel_Id = c.String(nullable: false, maxLength: 128),
                        PetModel_PetID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.OwnerModel_Id, t.PetModel_PetID })
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.PetModels", t => t.PetModel_PetID, cascadeDelete: true)
                .Index(t => t.OwnerModel_Id)
                .Index(t => t.PetModel_PetID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PetToOwnerModels", "PetID", "dbo.PetModels");
            DropForeignKey("dbo.PetToOwnerModels", "OwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContactUsModels", "OwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookingModels", "PetID", "dbo.PetModels");
            DropForeignKey("dbo.OwnerModelPetModels", "PetModel_PetID", "dbo.PetModels");
            DropForeignKey("dbo.OwnerModelPetModels", "OwnerModel_Id", "dbo.AspNetUsers");
            DropIndex("dbo.OwnerModelPetModels", new[] { "PetModel_PetID" });
            DropIndex("dbo.OwnerModelPetModels", new[] { "OwnerModel_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PetToOwnerModels", new[] { "OwnerID" });
            DropIndex("dbo.PetToOwnerModels", new[] { "PetID" });
            DropIndex("dbo.ContactUsModels", new[] { "OwnerID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BookingModels", new[] { "PetID" });
            DropTable("dbo.OwnerModelPetModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PetToOwnerModels");
            DropTable("dbo.ContactUsModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PetModels");
            DropTable("dbo.BookingModels");
        }
    }
}
