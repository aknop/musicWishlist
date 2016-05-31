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
    public class AlbumController : Controller
    {
        private TrueEntities db = new TrueEntities();
        // GET: Album
        public ActionResult Index()
        {
            var albumList = (from t in db.albums
                             join art in db.artists on t.artist_id equals art.id
                             select new SongsViewModel { SongID = t.id, AlbumName = t.name, ArtistName = art.artistName });
            return View(albumList.ToList());
        }

        // GET: album/Create
        public ActionResult Create()
        {
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName");
            return View();
        }

        // POST: album/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,artist_id")] album album)
        {
            if (ModelState.IsValid)
            {
                db.albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName", album.artist_id);
            return View(album);
        }

        // GET: songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            album album = db.albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName", album.artist_id);

            return View(album);
        }

        // POST: songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            album album = db.albums.Find(id);
            db.albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: album/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            album album = db.albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName", album.artist_id);

            return View(album);
        }

        // POST: album/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,artist_id")] album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName", album.artist_id);

            return View(album);
        }
    }
}