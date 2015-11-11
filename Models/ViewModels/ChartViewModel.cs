using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace BookProject.Models.ViewModels
{
    public class ChartViewModel
    {
        public ChartViewModel()
        {
             Chart = new Chart(600, 400);
        }
        public Chart Chart { get; set; }
    }
}








//namespace BookProject.Models.ViewModels
//{
//    public class ChartViewModel
//    {
//        public Chart Chart { get; set; }
//    }


//}