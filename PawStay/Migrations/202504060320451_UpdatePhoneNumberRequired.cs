namespace PawStay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePhoneNumberRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(nullable: false));
        }
    }
}
