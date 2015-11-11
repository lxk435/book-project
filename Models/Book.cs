using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Web.Mvc;

namespace BookProject.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        
        public string AuthorLastName { get; set; }

        
        public string AuthorFirstName { get; set; }

        public int PublicationYear { get; set; } = 2015;
        
        
        public string AuthorPlaceOfBirth { get; set; }

        [Required]
        public string Genre { get; set; }

        public string CoverImageFile { get; set; }
       
        public string UserId { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string LatLong { get; set; }

        public bool AlreadyRead { get; set; }

        //foreign key
        public int AuthorId { get; set; }

        //navigation property
        public virtual Author Author { get; set; }


    }

    //public class BookDBContext : DbContext
    //{
    //    public DbSet<Book> Books { get; set; }
    //}

}