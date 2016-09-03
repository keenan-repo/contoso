using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.APIModels
{
    public class Match
    {
        public Int64 match_id { get; set; }

        public Int64 match_seq_num { get; set; }
        public Int64 start_time { get; set; }
        public int lobby_type { get; set; }
        public int radiant_team_id { get; set; }
        public int dire_team_id { get; set; }

        public List<Player> players { get; set; }
    }
}