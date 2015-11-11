using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BookProject.Models;
using BookProject.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Xml.Linq;
using BookProject.Migrations;

namespace BookProject.Controllers
{
    public class BooksController : Controller
    {
        private BookDBContext db = new BookDBContext();
        
        // GET: Books
        [Authorize]
        public ActionResult Index(string bookGenre, string searchString, string author)
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Books
                           orderby d.Genre
                           select d.Genre;

            

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.bookGenre = new SelectList(GenreLst);


            var userId = User.Identity.GetUserId();
            var books = from b in db.Books.Where(b => b.UserId == userId)
                        select b;


            var AuthorList = new List<string>();

            foreach (var book in books)
            {
                string authorFull = book.AuthorFirstName + " " + book.AuthorLastName;

                if (!AuthorList.Contains(authorFull))
                {
                    AuthorList.Add(authorFull);
                }
            }

            ViewBag.author = new SelectList(AuthorList);

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(x => (x.AuthorFirstName + " " + x.AuthorLastName) == author);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(bookGenre))
            {
                books = books.Where(x => x.Genre == bookGenre);
            }

            

            return View(books);
        }














        // GET: Books/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize]
        public ActionResult Create()
        {
            var GenreLst = new List<SelectListItem>
            {
                    new SelectListItem {Text = "Action", Value = "Action"},
                    new SelectListItem {Text = "Adventure", Value = "Adventure"},
                    new SelectListItem {Text = "Art and design", Value = "Art and design"},
                    new SelectListItem {Text = "Autobiography", Value = "Autobiography"},
                    new SelectListItem {Text = "Biography", Value = "Biography"},
                    new SelectListItem {Text = "Business", Value = "Business"},
                    new SelectListItem {Text = "Childrens", Value = "Childrens"},
                    new SelectListItem {Text = "Classic literature", Value = "Classic literature"},
                    new SelectListItem {Text = "Comedy", Value = "Comedy"},
                    new SelectListItem {Text = "Comics and graphic novels", Value = "Comics and graphic novels"},
                    new SelectListItem {Text = "Computing", Value = "Computing"},
                    new SelectListItem {Text = "Cookery", Value = "Cookery"},
                    new SelectListItem {Text = "Craft and hobbies", Value = "Craft and hobbies"},
                    new SelectListItem {Text = "Crime", Value = "Crime"},
                    new SelectListItem {Text = "Dystopian fiction ", Value = "Dystopian fiction "},
                    new SelectListItem {Text = "Epic", Value = "Epic"},
                    new SelectListItem {Text = "Fantasy", Value = "Fantasy"},
                    new SelectListItem {Text = "Food and drink", Value = "Food and drink"},
                    new SelectListItem {Text = "Health", Value = "Health"},
                    new SelectListItem {Text = "Historical", Value = "Historical"},
                    new SelectListItem {Text = "Historical fiction", Value = "Historical fiction"},
                    new SelectListItem {Text = "Horror", Value = "Horror"},
                    new SelectListItem {Text = "Magical realism", Value = "Magical realism"},
                    new SelectListItem {Text = "Mystery", Value = "Mystery"},
                    new SelectListItem {Text = "Philosophical", Value = "Philosophical"},
                    new SelectListItem {Text = "Photography", Value = "Photography"},
                    new SelectListItem {Text = "Poetry", Value = "Poetry"},
                    new SelectListItem {Text = "Political", Value = "Political"},
                    new SelectListItem {Text = "Romance", Value = "Romance"},
                    new SelectListItem {Text = "Satire", Value = "Satire"},
                    new SelectListItem {Text = "Science fiction", Value = "Science fiction"},
                    new SelectListItem {Text = "Short story", Value = "Short story"},
                    new SelectListItem {Text = "Thriller", Value = "Thriller"},
                    new SelectListItem {Text = "Travel", Value = "Travel"},
                    new SelectListItem {Text = "Young adult", Value = "Young adult"},
                    new SelectListItem {Text = "Other", Value = "Other"},
            };

            ViewBag.bookGenre = GenreLst.ToList();


            var AuthorLst = new List<SelectListItem>();

            //var AuthorQry = from b in db.Books
            //                join a in db.Authors on b.AuthorId equals a.Id
            //                select new
            //                {
            //                    firstName = a.FirstName,
            //                    lastName = a.LastName,
            //                    id = a.Id
            //                };

            var AuthorQry = from a in db.Authors
                            select new
                            {
                                firstName = a.FirstName,
                                lastName = a.LastName,
                                id = a.Id
                            };

            foreach (var s in AuthorQry)
            {
                AuthorLst.Add(new SelectListItem {Text = (s.firstName + " " + s.lastName), Value = s.id.ToString()});
            }

            //AuthorLst.Add(new SelectListItem { Text = "Add new author", Value = "new" });

            ViewBag.bookAuthor = AuthorLst.ToList();



















            var YearLst = new List<SelectListItem>();

            for (var x = 2015; x >= -850; x--) {
                YearLst.Add(new SelectListItem{Text = x.ToString(), Value = x.ToString()});
            }

            ViewBag.bookYear = YearLst.ToList();

            var model = new Book();
        
            model.UserId = User.Identity.GetUserId();

            return View(model);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,AuthorLastName,AuthorFirstName,PublicationYear,AuthorId,AuthorPlaceOfBirth,Genre,CoverImage,UserId,AlreadyRead")] Book book, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(path);

                    book.CoverImageFile = "/Images/" + fileName;
                }



                db.Books.Add(book);

                //coordinate setting
                //var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(book.AuthorPlaceOfBirth));

                //var request = WebRequest.Create(requestUri);
                //var response = request.GetResponse();
                //var xdoc = XDocument.Load(response.GetResponseStream());

                //var result = xdoc.Element("GeocodeResponse").Element("result");
                //var locationElement = result.Element("geometry").Element("location");
                //var lat = locationElement.Element("lat");
                //var lng = locationElement.Element("lng");

                //var latString = lat.ToString().Replace("<lat>", "").Replace("</lat>", "");

                //var lngString = lng.ToString().Replace("<lng>", "").Replace("</lng>", "");

                //book.Latitude = latString;
                //book.Longitude = lngString;

                db.SaveChanges();
      
                return RedirectToAction("Index");
            }

            return View(book);
        }




        // GET: Books/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var GenreLst = new List<SelectListItem>
            {
                    new SelectListItem {Text = "Action", Value = "Action"},
                    new SelectListItem {Text = "Adventure", Value = "Adventure"},
                    new SelectListItem {Text = "Art and design", Value = "Art and design"},
                    new SelectListItem {Text = "Autobiography", Value = "Autobiography"},
                    new SelectListItem {Text = "Biography", Value = "Biography"},
                    new SelectListItem {Text = "Business", Value = "Business"},
                    new SelectListItem {Text = "Childrens", Value = "Childrens"},
                    new SelectListItem {Text = "Classic literature", Value = "Classic literature"},
                    new SelectListItem {Text = "Comedy", Value = "Comedy"},
                    new SelectListItem {Text = "Comics and graphic novels", Value = "Comics and graphic novels"},
                    new SelectListItem {Text = "Computing", Value = "Computing"},
                    new SelectListItem {Text = "Cookery", Value = "Cookery"},
                    new SelectListItem {Text = "Craft and hobbies", Value = "Craft and hobbies"},
                    new SelectListItem {Text = "Crime", Value = "Crime"},
                    new SelectListItem {Text = "Dystopian fiction ", Value = "Dystopian fiction "},
                    new SelectListItem {Text = "Epic", Value = "Epic"},
                    new SelectListItem {Text = "Fantasy", Value = "Fantasy"},
                    new SelectListItem {Text = "Food and drink", Value = "Food and drink"},
                    new SelectListItem {Text = "Health", Value = "Health"},
                    new SelectListItem {Text = "Historical", Value = "Historical"},
                    new SelectListItem {Text = "Historical fiction", Value = "Historical fiction"},
                    new SelectListItem {Text = "Horror", Value = "Horror"},
                    new SelectListItem {Text = "Magical realism", Value = "Magical realism"},
                    new SelectListItem {Text = "Mystery", Value = "Mystery"},
                    new SelectListItem {Text = "Philosophical", Value = "Philosophical"},
                    new SelectListItem {Text = "Photography", Value = "Photography"},
                    new SelectListItem {Text = "Poetry", Value = "Poetry"},
                    new SelectListItem {Text = "Political", Value = "Political"},
                    new SelectListItem {Text = "Romance", Value = "Romance"},
                    new SelectListItem {Text = "Satire", Value = "Satire"},
                    new SelectListItem {Text = "Science fiction", Value = "Science fiction"},
                    new SelectListItem {Text = "Short story", Value = "Short story"},
                    new SelectListItem {Text = "Thriller", Value = "Thriller"},
                    new SelectListItem {Text = "Travel", Value = "Travel"},
                    new SelectListItem {Text = "Young adult", Value = "Young adult"},
                    new SelectListItem {Text = "Other", Value = "Other"},
            };

            ViewBag.bookGenre = GenreLst.ToList();

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Title,AuthorLastName,AuthorFirstName,PublicationYear,AuthorId,AuthorPlaceOfBirth,Genre,CoverImageFile,UserId,AlreadyRead")] Book book, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(book).State = EntityState.Modified;

                if (file != null && file.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(path);

                    book.CoverImageFile = "/Images/" + fileName;
                }

                //coordinate setting
                var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(book.AuthorPlaceOfBirth));

                var request = WebRequest.Create(requestUri);
                var response = request.GetResponse();
                var xdoc = XDocument.Load(response.GetResponseStream());

                var result = xdoc.Element("GeocodeResponse").Element("result");
                var locationElement = result.Element("geometry").Element("location");
                var lat = locationElement.Element("lat");
                var lng = locationElement.Element("lng");

                var latString = lat.ToString().Replace("<lat>", "").Replace("</lat>", "");

                var lngString = lng.ToString().Replace("<lng>", "").Replace("</lng>", "");

                book.Latitude = latString;
                book.Longitude = lngString;
                book.LatLong = latString + ", " + lngString;



                db.SaveChanges();
                return RedirectToAction("Details", new { id = book.Id });
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult MapView()
        {

            var userId = User.Identity.GetUserId();
            var books = db.Books.Where(b => b.UserId == userId);

            var addressesq = from b in db.Books.Where(b => b.UserId == userId)
                             select b.AuthorPlaceOfBirth;

            var AddressList = new List<string>();
            AddressList.AddRange(addressesq);

            ViewBag.places = String.Join(",", AddressList.Select(s => "'" + s + "'"));

            var titlesq = from b in db.Books.Where(b => b.UserId == userId)
                             select b.Title;

            var TitleList = new List<string>();
            TitleList.AddRange(titlesq);

            ViewBag.titles = String.Join(",", TitleList.Select(s => '"' + s + '"'));


            var coordsq = from b in db.Books.Where(b => b.UserId == userId)
                             select b.LatLong;

            var coordsList = new List<string>();
            coordsList.AddRange(coordsq);

            ViewBag.coords = String.Join(",", coordsList.Select(s => "[" + s + "]"));


            var idq = from b in db.Books.Where(b => b.UserId == userId)
                          select b.Id;

            var tempList = new List<int>();
            tempList.AddRange(idq);

            var IdList = new List<string>();
            foreach (var id in tempList)
            {
                IdList.Add(id.ToString());
            }

            ViewBag.ids = String.Join(",", IdList.Select(s => '"' + s + '"'));


            return View();
        }



        [Authorize]
        public ActionResult MyStatsView()
        {
            GetChart();
            GetTimeline();
            GetAlreadyReadPieChart();
            return View();
        }


        // GET: Books
        [Authorize]
        public ActionResult StatsSummary()
        {
            var userId = User.Identity.GetUserId();
            var books = db.Books.Where(b => b.UserId == userId);

            var statsSummary = new StatsSummaryViewModel();

            statsSummary.StatsSummaryDictionary.Add("bookTotal", books.Count());


            var GenreLst = new List<string>();
            var GenreQry = from d in books
                           orderby d.Genre
                           select d.Genre;
            GenreLst.AddRange(GenreQry.Distinct());

            statsSummary.StatsSummaryDictionary.Add("genreTotal", GenreLst.Count);

            return PartialView("_StatsSummary", statsSummary);
        }


        // GET: Books
        [Authorize]
        public ActionResult Stats()
        {
            var userId = User.Identity.GetUserId();
            var books = db.Books.Where(b => b.UserId == userId);

            var stats = new StatsViewModel();

            var tempAuthDict = new Dictionary<string, int>();
            foreach (var book in books)
            {
                string authorFull = book.AuthorFirstName + " " + book.AuthorLastName;

                if (!tempAuthDict.ContainsKey(authorFull))
                {
                    tempAuthDict.Add(authorFull, 1);
                }
                else
                {
                    tempAuthDict[authorFull] = tempAuthDict[authorFull] + 1;
                }
            }

            foreach (var b in tempAuthDict)
            {
                if (tempAuthDict[b.Key] == 1)
                {
                    stats.AuthorsDictionary[b.Key] = tempAuthDict[b.Key].ToString() + " book";
                }
                else
                {
                    stats.AuthorsDictionary[b.Key] = tempAuthDict[b.Key].ToString() + " books";
                }      
            }

            var tempGenreDict = new Dictionary<string, int>();
            foreach (var book in books)
            {
                if (!tempGenreDict.ContainsKey(book.Genre))
                {
                    tempGenreDict.Add(book.Genre, 1);
                }
                else
                {
                    tempGenreDict[book.Genre] = tempGenreDict[book.Genre] + 1;
                }
            }

            foreach (var b in tempGenreDict)
            {
                if (tempGenreDict[b.Key] == 1)
                {
                    stats.GenresDictionary[b.Key] = tempGenreDict[b.Key].ToString() + " book";
                }
                else
                {
                    stats.GenresDictionary[b.Key] = tempGenreDict[b.Key].ToString() + " books";
                }
            }



            return View("_Stats", stats);
        }



        // GET: Books
        [Authorize]
        public ActionResult Chart()
        {

            var model = new ChartViewModel();
            //{
            //    Chart = GetChart()
            //};

            model.Chart = GetChart();
            
            return View("ChartView", model);
        }

        private Chart GetChart()
        {
            var userId = User.Identity.GetUserId();
            var books = db.Books.Where(b => b.UserId == userId);

            var gDict = new Dictionary<string, int>();

            foreach (var book in books)
            {
                if (!gDict.ContainsKey(book.Genre))
                {
                    gDict.Add(book.Genre, 1);
                }
                else
                {
                    gDict[book.Genre] = gDict[book.Genre] + 1;
                }
            }

            string[] x = new string[gDict.Count];
            gDict.Keys.CopyTo(x, 0);

            string[] y = new string[gDict.Count];
            for (int i = 0; i < gDict.Count; i++)
            {
                y[i] = gDict[x[i]].ToString();
            }


            return new Chart(400, 400, ChartTheme.Vanilla)
                .AddTitle("Genres")
                .AddLegend()
                .AddSeries(
                    
                    chartType: "Pie",
                    xValue: x,
                    yValues: y).Save("C:/BookProject/BookProject/Images/GenreChart.jpg");
        }









        // GET: Books
        [Authorize]
        public ActionResult Timeline()
        {

            var model = new ChartViewModel();
            //{
            //    Chart = GetChart()
            //};

            model.Chart = GetTimeline();

            return View("TimelineView", model);
        }

        private Chart GetTimeline()
        {
            var userId = User.Identity.GetUserId();
            var books = db.Books.Where(b => b.UserId == userId);

            var tDict = new Dictionary<int, int>();

            foreach (var book in books)
            {
                if (!tDict.ContainsKey(book.PublicationYear))
                {
                    tDict.Add(book.PublicationYear, 1);
                }
                else
                {
                    tDict[book.PublicationYear] = tDict[book.PublicationYear] + 1;
                }
            }

            int[] x = new int[tDict.Count];
            tDict.Keys.CopyTo(x, 0);
            //var x = a.OrderBy(i => i);
            Array.Sort(x, (i, j) => j.CompareTo(i));


            string[] y = new string[tDict.Count];
            for (int i = 0; i < tDict.Count; i++)
            {
                y[i] = tDict[x[i]].ToString();
            }


            var myChart = new Chart(350, 1000, ChartTheme.Vanilla)
                
                .AddTitle("Timeline")
                .AddLegend()
                .AddSeries(
                    
                    chartType: "Bar",
                    xValue: x,
                    yValues: y).Save("C:/BookProject/BookProject/Images/TimelineChart.jpg");

           

            return myChart;
        }



        

        public Chart GetAlreadyReadPieChart()
        {
            var userId = User.Identity.GetUserId();
            var books = db.Books.Where(b => b.UserId == userId);

            var rDict = new Dictionary<string, int>();
            rDict["Already read"] = 0;
            rDict["Not read yet"] = 0;

            foreach (var book in books)
            {
                if (book.AlreadyRead == true)
                {
                    rDict["Already read"] = rDict["Already read"] + 1;
                }
                else
                {
                    rDict["Not read yet"] = rDict["Not read yet"] + 1;
                }
            }

            string[] x = new string[rDict.Count];
            rDict.Keys.CopyTo(x, 0);

            int[] y = new int[rDict.Count];
            for (int i = 0; i < rDict.Count; i++)
            {
                y[i] = rDict[x[i]];
            }

            x[0] = x[0] + " " + ((int)(((double)y[0] / (double)(y[0] + y[1])) * 100)).ToString() + "%";
            x[1] = x[1] + " " + ((int)(((double)y[1] /(double)(y[0] + y[1])) * 100)).ToString() + "%";

            var myChart = new Chart(400, 400, ChartTheme.Vanilla)

                .AddTitle("Read and not read yet")
                .AddLegend()
                .AddSeries(

                    chartType: "Pie",
                    xValue: x,
                    yValues: y).Save("C:/BookProject/BookProject/Images/ReadChart.jpg");

            return myChart;
        }




























        // GET: Books
        [Authorize]
        public ActionResult ImageGrid()
        {
            
            var userId = User.Identity.GetUserId();
            var books = from b in db.Books.Where(b => b.UserId == userId)
                        select b;

            return View(books);
        }






        public ActionResult TestPartial()
        {
            

            return PartialView("_TESTPARTIAL");
        }


        public ActionResult TestView()
        {
            return View();
        }
















        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
