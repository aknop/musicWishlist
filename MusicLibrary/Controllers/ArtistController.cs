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

namespace MusicLibrary.Controllers
{
    public class ArtistController : Controller
    {
        private TrueEntities db = new TrueEntities();

        // GET: Artist
        public ActionResult Index()
        {
            var artistList = (from t in db.artists select new ArtistViewModel { ArtistID = t.id, ArtistName = t.artistName });
            return View(artistList.ToList());
        }
        
        // GET: Artist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtistViewModel artist)
        {
            artist ar = artist.FromModel();
            if (ModelState.IsValid)
            {
                db.artists.Add(ar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(artist);
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artist artist = db.artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            ArtistViewModel ar = new ArtistViewModel();
            ar.ToModel(artist);
            return View(ar);
        }

        // POST: Artist/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArtistViewModel artist)
        {
            artist ar = artist.FromModel();
            if (ModelState.IsValid)
            {
                db.Entry(ar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        // GET: Artist/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artist artist = db.artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            ArtistViewModel ar = new ArtistViewModel();
            ar.ToModel(artist);

            return View(ar);
        }

        // POST: Artist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var songs = db.songs.Where(s => s.artist_id == id).ToList();
            foreach (var s in songs)
            {
                db.songs.Remove(s);
            }
            var albums = db.albums.Where(s => s.artist_id == id).ToList();
            foreach (var a in albums)
            {
                db.albums.Remove(a);
            }
            
            artist artist = db.artists.Find(id);
            db.artists.Remove(artist);
            db.SaveChanges();
            return RedirectToAction("Index");
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
