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
    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //foreign key
        public int PlaceId { get; set; }

        //navigation properties
        public virtual ICollection<Book> Books { get; set; }

        public virtual Place Place { get; set; }
    }

}