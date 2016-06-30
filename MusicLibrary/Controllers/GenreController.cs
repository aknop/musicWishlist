using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MusicLibrary.Models;
using MusicLibrary.Filter;

namespace MusicLibrary.Controllers
{
    public class GenreController : Controller
    {
        
        private TrueEntities db = new TrueEntities();
        // GET: Genre
        public ActionResult Index()
        {
            var genreList = (from t in db.genres select new GenreViewModel { GenreID = t.id, GenreName = t.genreName });
            return View(genreList.ToList());
        }

        
        // GET: songs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GenreViewModel genre)
        {
            genre g = genre.FromModel();
            if (ModelState.IsValid)
            {
                db.genres.Add(g);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            //var jsonData = _jsonResponseFactory.CreateSuccessResponse("Added!");
            //return Json(jsonData, JsonRequestBehavior.AllowGet);
            return View(genre);
        }

        // GET: songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            genre genre = db.genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            GenreViewModel gm = new GenreViewModel();
            gm.ToModel(genre);
            return View(gm);
        }

        // POST: songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            genre genre = db.genres.Find(id);
            db.genres.Remove(genre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Genre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            genre genre = db.genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            GenreViewModel gm = new GenreViewModel();
            gm.ToModel(genre);
            return View(gm);
        }

        // POST: Genre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,genreName")] genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }
        // GET: songs/Create
        public ActionResult NewSongCreate()
        {
            return View();
        }

        // POST: songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewSongCreate(GenreViewModel genre)
        {
            genre g = genre.FromModel();
            if (ModelState.IsValid)
            {
                db.genres.Add(g);
                db.SaveChanges();
                return RedirectToAction("Create","songs", new { GenreID = g.id });
            }

            //var jsonData = _jsonResponseFactory.CreateSuccessResponse("Added!");
            //return Json(jsonData, JsonRequestBehavior.AllowGet);
            return View(genre);
        }
    }
}