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
    //public class CoverImage
    //{
    //    [Key, ForeignKey("Book")]
    //    public int ImageId { get; set; }

    //    public string Name { get; set; }
   
    //    public long Size { get; set; }
        
    //    public string Type { get; set; }

    //    public byte[] FileContent { get; set; }

    //    public bool IsDeleted { get; set; }

    //    public DateTime CreatedOn { get; set; }

    //    public Nullable<DateTime> ModifiedOn { get; set; }

    //    public virtual Book Book { get; set; }

    //}

    //public class BookDBContext : DbContext
    //{
    //    public DbSet<Book> Books { get; set; }
    //}

}