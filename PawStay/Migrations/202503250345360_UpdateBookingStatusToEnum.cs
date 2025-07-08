namespace WebAppTemplate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookingStatusToEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookingModels", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookingModels", "Status", c => c.String(nullable: false));
        }
    }
}
