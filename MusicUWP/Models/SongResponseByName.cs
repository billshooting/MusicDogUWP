using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicUWP.Models
{
    public class SongResponseByName
    {
        public int showapi_res_code { get; set; }
        public string showapi_res_error { get; set; }
        public SongNameRes showapi_res_body { get; set; }
    }

    public class Contentlist
    {
        public int albumid { get; set; }
        public string albummid { get; set; }
        public string albumname { get; set; }
        public string albumpic_big { get; set; }
        public string albumpic_small { get; set; }
        public string downUrl { get; set; }
        public string m4a { get; set; }
        public string media_mid { get; set; }
        public int singerid { get; set; }
        public string singername { get; set; }
        public int songid { get; set; }
        public string songname { get; set; }
        public string songmid { get; set; }
    }

    public class SongNamePagebean
    {
        public int allNum { get; set; }
        public int allPages { get; set; }
        public List<Contentlist> contentlist { get; set; }
        public int currentPage { get; set; }
        public int maxResult { get; set; }
    }

    public class SongNameRes
    {
        public SongNamePagebean pagebean { get; set; }
    }
}
