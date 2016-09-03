using System;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace ContosoUniversity.DAL
{
    public class APIcalls
    {
        public static string API_KEY = "FECFA52826DCE092D6752CA087B111F8";

        public static StreamReader APIcallfromURL(string URL)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(URL) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/json";

                HttpWebResponse MatchHistory = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(MatchHistory.GetResponseStream());
                return reader;
            }
            catch (Exception e)
            {
                //need some sort of error handling function here
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }

        }

        public static long[] getMatchIDs()
        {
                //key FECFA52826DCE092D6752CA087B111F8
                string account_id = "90935174";
                string URL_Domain = "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/";
                string Url = URL_Domain + "?key=" + API_KEY + "&account_id=" + account_id;

                var serializer = new JavaScriptSerializer();
                var matchHistoryObject = JsonConvert.DeserializeObject<APIModels.MatchHistory>(APIcallfromURL(Url).ReadToEnd());
                long[] IDs = new long[100];
                for (int i = 0; i < 99; i++)
                {
                    IDs[i] = matchHistoryObject.result.matches[i].match_id;

                }
                return IDs;
        }

        public static APIModels.MatchDetails getMatchDetails(long match_id)
        {
                //gets the details of the match id supplied. returns a match details object
                string URL_Domain = "https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/";
                string Url = URL_Domain + "?key=" + API_KEY + "&match_id=" + match_id;

                var serializer = new JavaScriptSerializer();
                var jsonObject = JsonConvert.DeserializeObject<APIModels.MatchDetails>(APIcallfromURL(Url).ReadToEnd());

                if (jsonObject.result.players.Count > 0)
                {
                    for (int i = 0; i < jsonObject.result.players.Count; i++)
                    {
                        jsonObject.result.players[i].steam_name = getSteamName(jsonObject.result.players[i].account_id);
                    }
                    
                }             
                return jsonObject;
        }

        public static string getSteamName(Int64 account_id)
        {
            string steam_name = "Anonymous";
            Int64 SteamID = account_id + 76561197960265728;

            if (SteamID == 76561202255233023) //a little arbitrary but this defines an anon account. TODO: Improve this
            {
                return steam_name;
            }
            else
            {
                string URL = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=FECFA52826DCE092D6752CA087B111F8&steamids=" + SteamID;

                var serializer = new JavaScriptSerializer();
                var steamObject = JsonConvert.DeserializeObject<APIModels.RootObject>(APIcallfromURL(URL).ReadToEnd());
                steam_name = steamObject.response.players[0].personaname;
                return steam_name;
            };
        }
        
                   
     }
}


    