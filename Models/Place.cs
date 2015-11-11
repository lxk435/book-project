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
    public class Place
    {
        public int Id { get; set; }

        public string PlaceName { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string LatLong { get; set; }

        //navigation property
        public virtual ICollection<Author> Authors { get; set; }
    }

}