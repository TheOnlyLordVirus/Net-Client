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
        /// The http client we use to send commands to our API.
        /// </summary>
        private static readonly HttpClient client = new HttpClient();

        private bool authorized = false;

        private string username = null;

        private string password = null;
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
                Task.Run(() => heartbeat());
                this.authorized = true;
                return true;
            }

            this.authorized = false;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Task heartbeat()
        {
            while(this.login(this.Username, this.Password))
            {
                Debugger.Log(1, "", "heart beat");
                Thread.Sleep(5000);
            }

            Debugger.Log(1, "", "heart failed");

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
            try
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

            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return null;
        }

        /// <summary>
        /// Sends a post request.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="postContent"></param>
        /// <returns>http response body content</returns>
        private static async Task<string> PostURI(Uri uri, HttpContent postContent)
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
        #endregion

        #region Propertys
        /// <summary>
        /// Is the client currently authorized?
        /// This is fequently updated by the heartbeat.
        /// </summary>
        public bool Authorized
        {
            get { return authorized; }
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