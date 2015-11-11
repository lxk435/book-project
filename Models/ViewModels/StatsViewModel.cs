using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookProject.Models.ViewModels
{
    public class StatsViewModel
    {
        public StatsViewModel()
        {
            AuthorsDictionary = new Dictionary<string, string>();

            GenresDictionary = new Dictionary<string, string>();
        }

        public Dictionary<string, string> AuthorsDictionary { get; set; }

        public Dictionary<string, string> GenresDictionary { get; set; }




    }


}