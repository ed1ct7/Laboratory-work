using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Models.Entity
{
    public class MyMusic
    {
        public Track[] Tracks { get; set; }
    }

    public class Track
    {
        public string Artist;
        public string Album;
        public string Title;
        public string Year;
    }
}
