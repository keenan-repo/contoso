using System.Collections.Generic;

namespace ContosoUniversity.APIModels
{
    public class MatchHistoryResult
    {

        public string status { get; set; }
        public string num_results { get; set; }
        public string total_results { get; set; }
        public string results_remaining { get; set; }

        public List<Match> matches { get; set; }
    }
}