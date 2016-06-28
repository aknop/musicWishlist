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
                             select new AlbumViewModel { AlbumID = t.id, AlbumName = t.name, ArtistName = art.artistName });
            return View(albumList.ToList());
        }

        // GET: album/Create
        public ActionResult Create(int defaultArtistID =0)
        {
            AlbumViewModel av = new AlbumViewModel();
            av.ArtistNames = new SelectList(db.artists, "id", "artistName",defaultArtistID);
            return View(av);
        }

        // POST: album/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumViewModel album)
        {
            album al = album.FromModel();
            if (ModelState.IsValid)
            {
                db.albums.Add(al);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            AlbumViewModel av = new AlbumViewModel();
            av.ArtistNames = new SelectList(db.artists, "id", "artistName");
            return View(av);
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
            AlbumViewModel al = new AlbumViewModel();
            al.ToModel(album);
            return View(al);
        }

        // POST: songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            var songs = db.songs.Where(s =>s.album_id == id).ToList();
            foreach (var s in songs)
            {
                db.songs.Remove(s);
            }
                       
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

            AlbumViewModel al = new AlbumViewModel();
            al.ToModel(album);
            al.ArtistNames = new SelectList(db.artists, "id", "artistName", al.ArtistID);

            return View(al);
        }

        // POST: album/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlbumViewModel album)
        {
            album al = album.FromModel();
            if (ModelState.IsValid)
            {
                db.Entry(al).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(al);
        }
    }
}