using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ClientTest.lib
{
    class Auth
    {
        #region Variables

        /// <summary>
        /// The http client we use to send commands to our server.
        /// </summary>
        private static readonly HttpClient client = new HttpClient();

        private bool authorized = false;

        private string username = null;

        private string password = null;

        private int incrementor = 0;

        private const int heartRate = 15;

        #endregion

        #region Methods

        /// <summary>
        /// Attempt to log in to the server.
        /// </summary>
        /// <returns></returns>
        public bool login(string user, string password)
        {
            this.username = user;
            this.password = password;

            if(sendCommand(user, password, "login", "").Equals("1"))
            {
                if(!authorized)
                {
                    Task.Run(() => heartbeat());
                    Task.Run(() => heartrate());
                    this.authorized = true;
                }

                return true;
            }

            this.authorized = false;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string redeemKey()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "username", username }
            };

            if (Authorized && HasTimeLeft)
            {
                return sendCommand(this.username, this.password, "redeem_key", JsonConvert.SerializeObject(values));
            }

            return string.Empty;
        }

        /// <summary>
        /// Attempt to log in to the server.
        /// </summary>
        /// <returns>The seconds left from the server as a string</returns>
        private string getTimeLeft()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "username", username }
            };

            if (Authorized)
            {
                return sendCommand(this.username, this.password, "time_check", JsonConvert.SerializeObject(values));
            }

            return "0";
        }


        /// <summary>
        /// Checks if the user is logged in every 5 seconds.
        /// </summary>
        /// <returns></returns>
        private Task heartbeat()
        {
            while(this.login(this.Username, this.Password))
            {
                Debugger.Log(1, "", "\nheart beat");
                Debugger.Log(1, "", "\n" + incrementor);
                incrementor = 0;

                getTimeLeft();

                Thread.Sleep(5000);
            }

            Debugger.Log(1, "", "heart failed");

            return Task.CompletedTask;
        }

        /// <summary>
        /// Increments a int for every half a second to coenside with the heartbeat.
        /// </summary>
        /// <returns></returns>
        private Task heartrate()
        {
            while (true)
            {
                incrementor++;
                Thread.Sleep(500);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Send a command to our server.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string sendCommand(string username, string password, string command, string parameters) 
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password },
                { "cheese", command },
                { "parms", parameters }
            };

            var postContent = new FormUrlEncodedContent(values);
            var runPostRequestTask = Task.Run(() => PostURI(new Uri("http://159.223.114.162/index.php"), postContent));
            runPostRequestTask.Wait();

            return runPostRequestTask.Result;
        }

        /// <summary>
        /// Sends a post request.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="postContent"></param>
        /// <returns>http response body content</returns>
        private static async Task<string> PostURI(Uri uri, HttpContent postContent)
        {
            try
            {
                string response = string.Empty;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage result = await client.PostAsync(uri, postContent);
                    if (result.IsSuccessStatusCode)
                    {
                        response = await result.Content.ReadAsStringAsync();
                    }
                }
                return response;
            }

            catch (HttpRequestException e)
            {
                Debugger.Log(1, "", "\nException Caught!");
                Debugger.Log(1, "", "\nMessage : " + e.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the current key.
        /// </summary>
        /// <returns></returns>
        public string responseKey()
        {
            char[] carray = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'X', 'Y', 'Z' };
            long dayinyear = DateTime.Now.DayOfYear;
            long year = DateTime.Now.Year;
            long a = (int)((year + dayinyear) | (year * dayinyear) + (year / dayinyear));
            long x = (int)((year + dayinyear) ^ (year | dayinyear) + (year ^ dayinyear));
            string numberstring = Math.Abs(a * x * (1 - x)).ToString();
            string retVal = "@";

            foreach (char c in numberstring)
            {
                if(int.TryParse(c.ToString(), out int charpos))
                {
                    retVal += carray[charpos];
                }
            }

            return $"{retVal}";
        }

        #endregion

        #region Propertys

        /// <summary>
        /// Are we authorized with time left?
        /// </summary>
        public bool AuthorizedWithTimeLeft
        {
            get { return (Authorized && HasTimeLeft); }
        }

        /// <summary>
        /// Is the client currently authorized?
        /// This is fequently updated by the heartbeat.
        /// </summary>
        public bool Authorized
        {
            get { return authorized; }
        }

        /// <summary>
        /// Gets the seconds this person has authed left.
        /// </summary>
        public string SecondsLeft
        {
            get
            {
                return getTimeLeft();
            }
        }

        /// <summary>
        /// Is there time left on this users account?
        /// </summary>
        public bool HasTimeLeft
        {
            get
            {
                return !getTimeLeft().Equals("0");
            }
        }

        /// <summary>
        /// Veryifys the integrity of the heart beat.
        /// </summary>
        public bool HeartRate
        {
            get { return this.incrementor >= Auth.heartRate; }
        }

        /// <summary>
        /// Get username that is currently logged in.
        /// </summary>
        public string Username
        {
            get 
            {
                if (this.Authorized)
                    return username;
                else
                    return null;
            }
        }

        /// <summary>
        /// Get password that is currently logged in.
        /// </summary>
        public string Password
        {
            get 
            {
                if (this.Authorized)
                    return password;
                else
                    return null;
            }
        }

        #endregion
    }
}