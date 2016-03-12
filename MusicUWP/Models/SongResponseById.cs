using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicUWP.Models
{
    public class SongIdRes
    {
        public string lyric { get; set; }
        public string lyric_txt { get; set; }
        public int ret_code { get; set; }
    }

    public class SongResponseById
    {
        public int showapi_res_code { get; set; }
        public string showapi_res_error { get; set; }
        public SongIdRes showapi_res_body { get; set; }
    }

}
