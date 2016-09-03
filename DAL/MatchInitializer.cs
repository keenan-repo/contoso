using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ContosoUniversity.Models;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace ContosoUniversity.DAL
{

    public class MatchInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GamesContext>
    {
        protected override void Seed(GamesContext context)
        {


            long[] matchIDs = APIcalls.getMatchIDs();

            APIModels.MatchDetails matchDetails = new APIModels.MatchDetails();
            var matches = new List<Match>();
            var players = new List<Player>();


            //Write to match details table. This uses the matchIDs from above, itterates through them, grabs the details and creates a match
            for (int i = 0; i < matchIDs.Length-1; i++)
            {
                System.Diagnostics.Debug.WriteLine("Match #" + i);
                matchDetails = APIcalls.getMatchDetails(matchIDs[i]);

                Match match = new Match { 
                    MatchID = i,
                    match_id = matchIDs[i],
                    match_seq_num = matchIDs[i],
                    start_time = matchDetails.result.start_time,
                    lobby_type = matchDetails.result.lobby_type,
                    radiant_win = matchDetails.result.radiant_win, 
                    duration = matchDetails.result.duration,
                    tower_status_dire = matchDetails.result.tower_status_dire,
                    tower_status_radiant = matchDetails.result.tower_status_radiant,
                    barracks_status_dire = matchDetails.result.tower_status_dire,
                    barracks_status_radiant = matchDetails.result.barracks_status_radiant,
                    first_blood_time = matchDetails.result.first_blood_time,
                    radiant_score = matchDetails.result.radiant_score,
                    dire_score = matchDetails.result.dire_score
                };


                //we need to itterate over each player in the game. Usually 10 players but sometimes less
                for (int j = 0; j < matchDetails.result.players.Count(); j++)
                {
                    Player player = new Player
                    {
                        MatchID = i,
                        account_id = matchDetails.result.players[j].account_id,
                        player_slot = matchDetails.result.players[j].player_slot,
                        hero_id = matchDetails.result.players[j].hero_id,
                        item_0 = matchDetails.result.players[j].item_0,
                        item_1 = matchDetails.result.players[j].item_1,
                        item_2 = matchDetails.result.players[j].item_2,
                        item_3 = matchDetails.result.players[j].item_3,
                        item_4 = matchDetails.result.players[j].item_4,
                        item_5 = matchDetails.result.players[j].item_5,
                        kills = matchDetails.result.players[j].kills,
                        deaths = matchDetails.result.players[j].deaths,
                        assists = matchDetails.result.players[j].assists,
                        leaver_status = matchDetails.result.players[j].leaver_status,
                        last_hits = matchDetails.result.players[j].last_hits,
                        denies = matchDetails.result.players[j].denies,
                        gold_per_min = matchDetails.result.players[j].gold_per_min,
                        xp_per_min = matchDetails.result.players[j].xp_per_min,
                        level = matchDetails.result.players[j].level,
                        gold = matchDetails.result.players[j].gold,
                        gold_spent = matchDetails.result.players[j].gold_spent,
                        hero_damage = matchDetails.result.players[j].hero_damage,
                        hero_healing = matchDetails.result.players[j].hero_healing,
                        tower_damage = matchDetails.result.players[j].tower_damage,
                        steam_name = matchDetails.result.players[j].steam_name               
                    };
                    players.Add(player);
                }

                matches.Add(match);
                //on to the next game!
            };
            
            matches.ForEach(s => context.Matches.Add(s));
            players.ForEach(s => context.Players.Add(s));
            context.SaveChanges();

        }
    }
}
