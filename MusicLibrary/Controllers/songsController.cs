using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicLibrary;

namespace MusicLibrary.Controllers
{
    public class songsController : Controller
    {
        private masterEntities db = new masterEntities();

        // GET: songs
        public ActionResult Index()
        {
            var songs = db.songs.Include(s => s.album).Include(s => s.artist);
            return View(songs.ToList());
        }

        // GET: songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            song song = db.songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: songs/Create
        public ActionResult Create()
        {
            ViewBag.album_id = new SelectList(db.albums, "id", "name");
            ViewBag.artist_id = new SelectList(db.artists, "id", "name");
            return View();
        }

        // POST: songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,artist_id,album_id,track_number")] song song)
        {
            if (ModelState.IsValid)
            {
                db.songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.album_id = new SelectList(db.albums, "id", "name", song.album_id);
            ViewBag.artist_id = new SelectList(db.artists, "id", "name", song.artist_id);
            return View(song);
        }

        // GET: songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            song song = db.songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.album_id = new SelectList(db.albums, "id", "name", song.album_id);
            ViewBag.artist_id = new SelectList(db.artists, "id", "name", song.artist_id);
            return View(song);
        }

        // POST: songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,artist_id,album_id,track_number")] song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.album_id = new SelectList(db.albums, "id", "name", song.album_id);
            ViewBag.artist_id = new SelectList(db.artists, "id", "name", song.artist_id);
            return View(song);
        }

        // GET: songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            song song = db.songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            song song = db.songs.Find(id);
            db.songs.Remove(song);
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
