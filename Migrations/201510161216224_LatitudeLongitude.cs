namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatitudeLongitude : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Latitude", c => c.String());
            AddColumn("dbo.Books", "Longitude", c => c.String());
            AddColumn("dbo.Books", "LatLong", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "LatLong");
            DropColumn("dbo.Books", "Longitude");
            DropColumn("dbo.Books", "Latitude");
        }
    }
}
