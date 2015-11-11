namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AuthorLastName = c.String(),
                        AuthorFirstName = c.String(),
                        PublicationYear = c.Int(),
                        AuthorId = c.Int(),
                        AuthorPlaceOfBirth = c.String(),
                        Genre = c.String(),
                        CoverImageFile = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
