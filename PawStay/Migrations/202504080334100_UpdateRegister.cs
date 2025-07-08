namespace PawStay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRegister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "OwnerFirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "OwnerLastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "OwnerAge", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "OwnerAge");
            DropColumn("dbo.AspNetUsers", "OwnerLastName");
            DropColumn("dbo.AspNetUsers", "OwnerFirstName");
        }
    }
}
