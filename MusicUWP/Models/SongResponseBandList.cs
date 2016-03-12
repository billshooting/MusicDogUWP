using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicUWP.Models
{
    public class WebRequestSong
    {
        public int albumid { get; set; }
        public string albummid { get; set; }
        public string albumpic_big { get; set; }
        public string albumpic_small { get; set; }
        public string downUrl { get; set; }
        public int seconds { get; set; }
        public int singerid { get; set; }
        public string singername { get; set; }
        public int songid { get; set; }
        public string songname { get; set; }
        public string url { get; set; }
    }

    public class BandListPagebean
    {
        public int currentPage { get; set; }
        public int song_begin { get; set; }
        public List<WebRequestSong> songlist { get; set; }
        public int total_song_num { get; set; }
    }

    public class BandListRes
    {
        public BandListPagebean pagebean { get; set; }
        public int ret_code { get; set; }
    }

    public class SongResponseBandList
    {
        public int showapi_res_code { get; set; }
        public string showapi_res_error { get; set; }
        public BandListRes showapi_res_body { get; set; }
    }
}
