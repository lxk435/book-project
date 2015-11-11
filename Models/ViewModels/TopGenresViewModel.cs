using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookProject.Models.ViewModels
{
    public class TopGenresViewModel
    {
        public TopGenresViewModel()
        {
            TopGenresDictionary = new Dictionary<string, int>();
        }

        public Dictionary<string, int> TopGenresDictionary { get; set; } 
    }
}