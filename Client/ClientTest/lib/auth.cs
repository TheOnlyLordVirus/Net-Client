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
using System.Security.Cryptography;
using System.IO;

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

            if(!sendCommand(user, password, "login", "").Equals("1"))
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

            string Json = JsonConvert.SerializeObject(values);
            Dictionary<string, string> post = new Dictionary<string, string>{ { "bluecheese", Json } };

            var runPostRequestTask = Task.Run(() => PostURI(new Uri("http://159.223.114.162/index.php"), new FormUrlEncodedContent(post)));
            runPostRequestTask.Wait();

            return DecryptString(runPostRequestTask.Result);
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
        /// Encrypt a Key
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        private string EncryptString(string plainText)
        {
            string password = (DateTime.Now.DayOfYear + DateTime.Now.Year).ToString();

            // Create sha256 hash
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));

            // Create secret IV
            byte[] iv = new byte[16] { 0x0, 0xf, 0x0, 0xf, 0x0, 0xf, 0x0, 0xf, 0x0, 0xf, 0x0, 0x0, 0x0, 0x0, 0xe, 0x0 };

            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = key;
            encryptor.IV = iv;

            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);

            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

            return cipherText;
        }

        private string DecryptString(string cipherText)
        {
            string password = (DateTime.Now.DayOfYear - DateTime.Now.Year).ToString();

            // Create sha256 hash
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));

            // Create secret IV
            byte[] iv = new byte[16] { 0x0, 0xf, 0x0, 0xf, 0x0, 0xf, 0x0, 0xf, 0x0, 0xf, 0x0, 0x0, 0x0, 0x0, 0xe, 0x0 };

            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = key;
            encryptor.IV = iv;

            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);
            string plainText = String.Empty;

            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                // Complete the decryption process
                cryptoStream.FlushFinalBlock();

                // Convert the decrypted data from a MemoryStream to a byte array
                byte[] plainBytes = memoryStream.ToArray();

                // Convert the decrypted byte array to string
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }

            finally
            {
                // Close both the MemoryStream and the CryptoStream
                memoryStream.Close();
                cryptoStream.Close();
            }

            // Return the decrypted data as a string
            return plainText;
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