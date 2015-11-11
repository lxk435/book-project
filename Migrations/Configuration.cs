using BookProject.Models;

namespace BookProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookProject.Models.BookDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookProject.Models.BookDBContext context)
        {
            
            context.Books.AddOrUpdate( i => i.Title,
                new Book
                {
                    Title = "Harry Potter and the Philosopher's Stone",
                    AuthorFirstName = "J.K.",
                    AuthorId = 1,
                    AuthorLastName = "Rowling",
                    AuthorPlaceOfBirth = "Yate, England",
                    CoverImageFile = "r",
                    Genre = "F",
                    

                }
            );

            context.Authors.AddOrUpdate(i => i.Id,
                new Author
                {
                    FirstName = "Jane",
                    LastName = "Austen",
                    PlaceId = 1,
                    Id = 1,
                }
            );

            context.Places.AddOrUpdate(i => i.Id,
                new Place
                {
                    PlaceName = "York, England",
                    Latitude = "1",
                    Longitude = "1",
                    LatLong = "1,1",
                    Id = 1,
                }
            );
        }
    }
}
