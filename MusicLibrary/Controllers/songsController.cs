﻿using System;
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
                            select new SongsViewModel{ SongID = t.id, TrackName=t.name, TrackNumber = t.track_number, ArtistName = art.artistName, AlbumName = al.albumName, GenreName= gen.genreName, AlbumID = al.id, ArtistID = art.id });
            
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
            SongsViewModel sv = new SongsViewModel();
            sv.ToModel(song);
            return View(sv);
        }

        // GET: songs/Create
        public ActionResult Create(int AlbumID = 0, int ArtistID = 0, int GenreID = 0)
        {
            //get initial list of albums populated
            List<AlbumViewModel> AlbumsList = UpdatedAlbumsList(ArtistID);

            //Apply attributes to our Song model.
            SongsViewModel sv = new SongsViewModel();
            sv.ArtistNames = new SelectList(db.artists.OrderBy(x => x.artistName), "id", "artistName");
            //selects album to be the one user just created
            sv.AlbumNames = new SelectList(AlbumsList, "AlbumID", "AlbumName", AlbumID);
            sv.GenreNames = new SelectList(db.genres, "id", "genreName", GenreID);
            sv.AlbumID = AlbumID;
            sv.ArtistID = ArtistID;
            return View(sv);
        }

        // POST: songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SongsViewModel song)
        {
            song s = song.FromModel();
            if (ModelState.IsValid)
            {
                db.songs.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            song.ArtistNames = new SelectList(db.artists.OrderBy(x => x.artistName), "id", "artistName");
            song.AlbumNames = new SelectList(db.albums, "id", "name");
            song.GenreNames = new SelectList(db.genres, "id", "genreName");
            return View(song);
        }

        // GET: songs/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            song ss = db.songs.Find(id);
            if (ss == null)
            {
                return HttpNotFound();
            }
            //get initial list of albums populated
            List<AlbumViewModel> AlbumsList = UpdatedAlbumsList(ss.artist_id);

            SongsViewModel sv = new SongsViewModel();
            sv.ToModel(ss);
            sv.ArtistNames = new SelectList(db.artists.OrderBy(x => x.artistName), "id", "artistName", ss.artist_id);
            sv.AlbumNames = new SelectList(AlbumsList, "AlbumID", "AlbumName", ss.album_id);
            sv.GenreNames = new SelectList(db.genres, "id", "genreName", ss.genre_id);
            return View(sv);
        }

        // POST: songs/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SongsViewModel song)
        {
            song s = song.FromModel();
            if (ModelState.IsValid)
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            SongsViewModel sv = new SongsViewModel();
            sv.ToModel(song);
            return View(sv);
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

       
        //View a sorted list of albums
        public ActionResult AlbumIndex(int AlbumID, int ArtistID)
        {
            var AlbumList = new AlbumViewModel();
            AlbumList.SongList = (from t in db.songs
                                   join art in db.artists on t.artist_id equals art.id where art.id == ArtistID
                                   join al in db.albums on t.album_id equals al.id where al.id == AlbumID
                                   join gen in db.genres on t.genre_id equals gen.id orderby t.track_number
                                   select new SongsViewModel { SongID = t.id, TrackName = t.name, TrackNumber = t.track_number, AlbumName = al.albumName, GenreName = gen.genreName, ArtistID = art.id, AlbumID = al.id });
            AlbumList.AlbumName = db.albums.Where(a => a.id == AlbumID).ToList()[0].albumName;
            AlbumList.ArtistName = db.artists.Where(a => a.id == ArtistID).ToList()[0].artistName;
            return View("AlbumIndex", AlbumList);
        }
        //AlbumIndex edit 
        public ActionResult AlbumEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            song ss = db.songs.Find(id);
            if (ss == null)
            {
                return HttpNotFound();
            }
            SongsViewModel sv = new SongsViewModel();
            sv.ToModel(ss);
            sv.ArtistNames = new SelectList(db.artists.OrderBy(x => x.artistName), "id", "artistName", ss.artist_id);
            sv.AlbumNames = new SelectList(db.albums, "id", "albumName", ss.album_id);
            sv.GenreNames = new SelectList(db.genres, "id", "genreName", ss.genre_id);

            return View(sv);
        }

        //AlbumIndex edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlbumEdit(SongsViewModel song)
        {
            song s = song.FromModel();
            if (ModelState.IsValid)
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AlbumIndex",new { AlbumID = song.AlbumID, ArtistID = song.ArtistID });
            }

            return View(s);
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
            SongsViewModel ss = new SongsViewModel();
            ss.ToModel(song);
            return View(ss);
        }

        // AlbumIndex delete
        [HttpPost, ActionName("AlbumDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AlbumDeleteConfirmed(int id)
        {
            song song = db.songs.Find(id);
            db.songs.Remove(song);
            db.SaveChanges();
            
            return RedirectToAction("AlbumIndex", new { AlbumID = song.album_id, ArtistID = song.artist_id });
        }
        // sort songs by artist
        public ActionResult ArtistIndex(int ArtistID)
        {
            var ArtistList = new ArtistViewModel();
            ArtistList.SongList = (from t in db.songs
                                   join art in db.artists on t.artist_id equals art.id
                                   where art.id == ArtistID
                                   join gen in db.genres on t.genre_id equals gen.id
                                   join al in db.albums on t.album_id equals al.id
                                   orderby al.albumName, t.track_number
                                   select new SongsViewModel { SongID = t.id, TrackName = t.name, TrackNumber = t.track_number, AlbumName = al.albumName, GenreName = gen.genreName });
            ArtistList.ArtistName = db.artists.Where(a=> a.id == ArtistID).ToList()[0].artistName;
            return View("ArtistIndex", ArtistList);
        }
        // ArtistIndex edit
        public ActionResult ArtistEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            song ss = db.songs.Find(id);
            if (ss == null)
            {
                return HttpNotFound();
            }
            SongsViewModel sv = new SongsViewModel();
            sv.ToModel(ss);
            sv.ArtistNames = new SelectList(db.artists.OrderBy(x => x.artistName), "id", "artistName", ss.artist_id);
            sv.AlbumNames = new SelectList(db.albums, "id", "albumName", ss.album_id);
            sv.GenreNames = new SelectList(db.genres, "id", "genreName", ss.genre_id);
            return View(sv);
        }

        // ArtistIndex edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArtistEdit(SongsViewModel song)
        {
            song s = song.FromModel();

            if (ModelState.IsValid)
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ArtistIndex", new { ArtistID = song.ArtistID });
            }
            return View(s);
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
            SongsViewModel ss = new SongsViewModel();
            ss.ToModel(song);
            return View(ss);
        }

        // ArtistIndex delete
        [HttpPost, ActionName("ArtistDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ArtistDeleteConfirmed(int id)
        {
            song song = db.songs.Find(id);
            db.songs.Remove(song);
            db.SaveChanges();

            return RedirectToAction("ArtistIndex", new { ArtistID = song.artist_id });
        }
        

        //Crystal report of song list.
        public ActionResult Report()
        {   ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/SongReport.rpt")));
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "SongList.pdf");
        }
        //returns a list of albumnames to the JavaScript so it can be updated
        public ActionResult UpdatedAlbums(int artistID=0)
        {
            List<AlbumViewModel> AlbumNames = new List<AlbumViewModel>();
            foreach (var z in db.albums)
            {
                //For every album, if the album's artist ID matches the parameter, add it to the list
                if(z.artist_id == artistID)
                {
                    AlbumViewModel av = new AlbumViewModel();
                    av.AlbumName = z.albumName;
                    av.AlbumID = z.id;
                    var ArtistNameList = db.artists.Where(a => a.id == artistID);
                    av.ArtistName = ArtistNameList.First().artistName;
                    AlbumNames.Add(av);
                }
            }
            return Json(new { AlbumNames},JsonRequestBehavior.AllowGet);
        }
        //returns list of albumnames by the artist
        private List<AlbumViewModel> UpdatedAlbumsList(int artistID)
        {
            List<AlbumViewModel> AlbumNames = new List<AlbumViewModel>();
            foreach (var z in db.albums)
            {
                if (z.artist_id == artistID)
                {
                    AlbumViewModel av = new AlbumViewModel();
                    av.AlbumName = z.albumName;
                    av.AlbumID = z.id;
                    AlbumNames.Add(av);
                }
            }
            return AlbumNames;
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
