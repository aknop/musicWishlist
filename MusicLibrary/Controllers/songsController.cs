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
using MusicLibrary.Reports;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data.SqlClient;

namespace MusicLibrary.Controllers
{
    public class songsController : Controller
    {
        private TrueEntities db = new TrueEntities();

        // GET: songs
        public ActionResult Index()
        {
            var songList = (from t in db.songs
                            join art in db.artists on t.artist_id equals art.id
                            join al in db.albums on t.album_id equals al.id
                            join gen in db.genres on t.genre_id equals gen.id
                            select new SongsViewModel{ SongID = t.id, TrackName=t.name, TrackNumber = t.track_number, ArtistName = art.artistName, AlbumName = al.name, GenreName= gen.genreName });
            
            return View(songList.ToList());
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
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName");
            ViewBag.genre_id = new SelectList(db.genres, "id", "genreName");
            return View();
        }

        // POST: songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,artist_id,album_id,track_number,genre_id")] song song)
        {
            if (ModelState.IsValid)
            {
                db.songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.album_id = new SelectList(db.albums, "id", "name", song.album_id);
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName", song.artist_id);
            ViewBag.genre_id = new SelectList(db.genres, "id", "genreName", song.genre_id);
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
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName", song.artist_id);
            ViewBag.genre_id = new SelectList(db.genres, "id", "genreName", song.genre_id);
            return View(song);
        }

        // POST: songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,artist_id,album_id,track_number,genre_id")] song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.album_id = new SelectList(db.albums, "id", "name", song.album_id);
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName", song.artist_id);
            ViewBag.genre_id = new SelectList(db.genres, "id", "genreName", song.genre_id);
            return View(song);
        }

        // GET: delete song
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

        // POST: delete song
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            song song = db.songs.Find(id);
            db.songs.Remove(song);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // sort songs by artist
        public ActionResult ArtistIndex(string ArtistName)
        {
            var ArtistList = new ArtistViewModel();
            ArtistList.SongList = (from t in db.songs
                            join art in db.artists on t.artist_id equals art.id where art.artistName == ArtistName
                            join al in db.albums on t.album_id equals al.id
                            join gen in db.genres on t.genre_id equals gen.id
                            select new SongsViewModel { SongID = t.id, TrackName = t.name, TrackNumber = t.track_number, AlbumName = al.name, GenreName = gen.genreName });
            ArtistList.ArtistName = ArtistName;
            return View("ArtistIndex",ArtistList);
        }

        // ArtistIndex edit
        public ActionResult ArtistEdit(int? id)
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
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName", song.artist_id);
            ViewBag.genre_id = new SelectList(db.genres, "id", "genreName", song.genre_id);
            return View(song);
        }

        // ArtistIndex edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArtistEdit([Bind(Include = "id,name,artist_id,album_id,track_number,genre_id")] song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                var ArtistNameID = db.artists.Find(song.artist_id).artistName;
                return RedirectToAction("ArtistIndex", new { ArtistName = ArtistNameID });
            }
            return View(song);
        }

        //View a sorted list of albums
        public ActionResult AlbumIndex(string AlbumName)
        {
            var AlbumList = new AlbumViewModel();
            AlbumList.SongList = (from t in db.songs
                                   join art in db.artists on t.artist_id equals art.id
                                   join al in db.albums on t.album_id equals al.id where al.name == AlbumName
                                   join gen in db.genres on t.genre_id equals gen.id orderby t.track_number
                                   select new SongsViewModel { SongID = t.id, TrackName = t.name, TrackNumber = t.track_number, AlbumName = al.name, GenreName = gen.genreName });
            AlbumList.AlbumName = AlbumName;
            return View("AlbumIndex", AlbumList);
        }

        //AlbumIndex edit 
        public ActionResult AlbumEdit(int? id)
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
            ViewBag.artist_id = new SelectList(db.artists, "id", "artistName", song.artist_id);
            ViewBag.genre_id = new SelectList(db.genres, "id", "genreName", song.genre_id);

            return View(song);
        }

        //AlbumIndex edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlbumEdit([Bind(Include = "id,name,artist_id,album_id,track_number,genre_id")] song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                var AlbumNameID = db.albums.Find(song.album_id).name;
                return RedirectToAction("AlbumIndex", new { AlbumName = AlbumNameID });
            }

            return View(song);
        }
        // AlbumIndex details
        public ActionResult AlbumDetails(int? id)
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
        // AlbumIndex delete
        public ActionResult AlbumDelete(int? id)
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

        // AlbumIndex delete
        [HttpPost, ActionName("AlbumDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AlbumDeleteConfirmed(int id)
        {
            song song = db.songs.Find(id);
            var AlbumNameID = db.albums.Find(song.album_id).name;
            db.songs.Remove(song);
            db.SaveChanges();
            
            return RedirectToAction("AlbumIndex", new { AlbumName = AlbumNameID });
        }

        // ArtistIndex details
        public ActionResult ArtistDetails(int? id)
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
        // ArtistIndex delete
        public ActionResult ArtistDelete(int? id)
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

        // ArtistIndex delete
        [HttpPost, ActionName("ArtistDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ArtistDeleteConfirmed(int id)
        {
            song song = db.songs.Find(id);
            var ArtistNameID = db.artists.Find(song.album_id).artistName ;
            db.songs.Remove(song);
            db.SaveChanges();

            return RedirectToAction("ArtistIndex", new { ArtistName = ArtistNameID });
        }
        


        public ActionResult Report()
        {   ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/SongReport.rpt")));
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "SongList.pdf");
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
