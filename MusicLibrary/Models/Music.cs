using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicLibrary.Models
{
    public class Music
    {
        public int ID { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Song { get; set; }
        public int TrackNumber { get; set; }
        public string Genre { get; set; }
    }
}