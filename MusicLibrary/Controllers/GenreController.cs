using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicLibrary;
using MusicLibrary.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace MusicLibrary.Controllers
{
    
    public class GenreController : Controller
    {
        
        private TrueEntities db = new TrueEntities();
        // GET: Genre
        public ActionResult Index()
        {
            var genreList = (from t in db.genres select new SongsViewModel { SongID = t.id, GenreName = t.genreName });
            return View(genreList.ToList());
        }

        
        // GET: songs/Create
        public ActionResult Create()
        {
            ViewBag.genre_id = new SelectList(db.genres, "id", "genreName");
            return View();
        }

        // POST: songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,genreName")] genre genre)
        {
            
            if (ModelState.IsValid)
            {
                db.genres.Add(genre);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            
            ViewBag.genre_id = new SelectList(db.genres, "id", "genreName", genre.genreName);
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
            return View(genre);
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


        // GET: Artist/Edit/5
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
            return View(genre);
        }

        // POST: Artist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
    }
}