using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookProject.Models.ViewModels
{
    public class StatsSummaryViewModel
    {
        public StatsSummaryViewModel() 
        {
            StatsSummaryDictionary = new Dictionary<string, int>();
        }

        public Dictionary<string, int> StatsSummaryDictionary { get; set; }
}

    
}