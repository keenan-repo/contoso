using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Result
    {
        public string status { get; set; }
        public string num_results { get; set; }
        public string total_results { get; set; }
        public string results_remaining { get; set; }

        public List<Match> matches { get; set; }
        public List<Player> players { get; set; }
    }
}