using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
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
                             join art in db.artists on t.artist_id equals art.id orderby t.artist.artistName, t.albumName
                             join gen in db.genres on t.genre_id equals gen.id
                             select new AlbumViewModel { AlbumID = t.id, AlbumName = t.albumName, ArtistName = art.artistName, GenreID = gen.id });
            return View(albumList.ToList());
        }

        // GET: album/Create
        public ActionResult Create()
        {
            AlbumViewModel av = new AlbumViewModel();
            av.ArtistNames = new SelectList(db.artists.OrderBy(x=>x.artistName), "id", "artistName");
            av.GenreNames = new SelectList(db.genres.OrderBy(x => x.genreName), "id", "genreName");
            return View(av);
        }

        // POST: album/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumViewModel album)
        {
            album.GenreName = db.genres.Where(x => x.id == album.GenreID).First().genreName;
            album al = album.FromModel();
            if (ModelState.IsValid)
            {
                db.albums.Add(al);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            AlbumViewModel av = new AlbumViewModel();
            av.ArtistNames = new SelectList(db.artists.OrderBy(x => x.artistName), "id", "artistName");
            av.GenreNames = new SelectList(db.genres.OrderBy(x => x.genreName), "id", "genreName");
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
            al.ArtistNames = new SelectList(db.artists.OrderBy(x => x.artistName), "id", "artistName", al.ArtistID);
            al.GenreNames = new SelectList(db.genres.OrderBy(x => x.genreName), "id", "genreName", al.GenreID);

            return View(al);
        }

        // POST: album/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlbumViewModel album)
        {
            album.GenreName = db.genres.Where(x => x.id == album.GenreID).First().genreName;
            album al = album.FromModel();
            if (ModelState.IsValid)
            {
                db.Entry(al).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            album.ArtistNames = new SelectList(db.artists.OrderBy(x => x.artistName), "id", "artistName", al.id);
            album.GenreNames = new SelectList(db.genres.OrderBy(x => x.genreName), "id", "genreName", al.genre_id);
            return View(album);
        }

        // GET: newsong/Create
        public ActionResult NewSongCreate(int? defaultArtistID)
        {
            
            AlbumViewModel av = new AlbumViewModel();
            av.ArtistNames = new SelectList(db.artists.OrderBy(x => x.artistName), "id", "artistName");
            av.GenreNames = new SelectList(db.genres.OrderBy(x => x.genreName), "id", "genreName");
            if (defaultArtistID != null)
            {
                av.ArtistID = defaultArtistID ?? 0;
                av.importedArtist = true;
            }
            return View(av);
        }

        // POST: newsong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewSongCreate(AlbumViewModel album)
        {
            album.GenreName = db.genres.Where(x => x.id == album.GenreID).First().genreName;
            album al = album.FromModel();
            if (ModelState.IsValid)
            {
                db.albums.Add(al);
                db.SaveChanges();
                //If an artist isn't passed in from the new song page, then redirect to the new album in the song Index.
                if (album.importedArtist == true)
                    return RedirectToAction("Create", "songs", new { ArtistID = album.ArtistID, AlbumID = al.id });
                else
                    return RedirectToAction("AlbumIndex", "songs", new { AlbumID = al.id, ArtistID = album.ArtistID });
            }
            
            AlbumViewModel av = new AlbumViewModel();
            av.ArtistNames = new SelectList(db.artists.OrderBy(x => x.artistName), "id", "artistName");
            return View(av);
            
        }
    }
}