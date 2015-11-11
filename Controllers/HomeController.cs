using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.DynamicData;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using BookProject.Models;
using BookProject.Models.ViewModels;
using Microsoft.AspNet.Identity;


namespace BookProject.Controllers
{
    public class HomeController : Controller

    {
        private BookDBContext db = new BookDBContext();

        public ActionResult Index()
        {

            //var books = db.Books;

            //var addressesq = from b in db.Books
            //                 select b.AuthorPlaceOfBirth;

            //var AddressList = new List<string>();
            //AddressList.AddRange(addressesq);

            //ViewBag.places = String.Join(",", AddressList.Select(s => "'" + s + "'"));

            //var titlesq = from b in db.Books
            //              select b.Title;

            //var TitleList = new List<string>();
            //AddressList.AddRange(titlesq);

            //ViewBag.titles = String.Join(",", TitleList.Select(s => "'" + s + "'"));

            //return View("MapView");


            var books = db.Books;

            var TopGenreList = new List<string>();

            var TopGenreQry = from d in db.Books
                           orderby d.Genre
                           select d.Genre;

            TopGenreList.AddRange(TopGenreQry);

            var TopGenreDict = new Dictionary<string, int>();

            foreach (var item in TopGenreList)
            {
                if (!TopGenreDict.ContainsKey(item))
                {
                    TopGenreDict.Add(item, 1);
                }
                else
                {
                    TopGenreDict[item] = TopGenreDict[item] + 1;
                }
            }

            ViewBag.topGenre = TopGenreDict;

            return View(books);
        }


        public ActionResult TopGenres()
        {
            var topGenres = new TopGenresViewModel();

            foreach (var item in db.Books)
            {
                if (!topGenres.TopGenresDictionary.ContainsKey(item.Genre))
                {
                    topGenres.TopGenresDictionary.Add(item.Genre, 1);
                }
                else
                {
                    topGenres.TopGenresDictionary[item.Genre] = topGenres.TopGenresDictionary[item.Genre] + 1;
                }
            }

            return View(topGenres);
        }











        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        



    }
}

//You restrict access to a view by using the AuthorizeAttribute attribute to mark the action method that creates the view. 
//You can restrict access to all views of a controller by using the AuthorizeAttribute attribute to mark the controller itself.