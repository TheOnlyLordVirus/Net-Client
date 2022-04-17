using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClientAuth
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

        private int heartRate = 0;

        private string dkey = string.Empty;

        private string ekey = string.Empty;

        #endregion

        #region Response Structs

        private struct DkeyResponse
        {
            public string dkey;
            public int heartrate;
            public int heartrhythm;
        }
        private struct LoginResponse
        {
            public bool loggedin;
        }

        private struct TimeResponse
        {
            public int timeleft;
        }

        private struct KeyResponse
        {
            public bool keyres;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Class constructor
        /// </summary>
        public Auth()
        {
            GetEncryptionKey();
        }

        /// <summary>
        /// Attempt to log in to the server.
        /// </summary>
        /// <returns></returns>
        public bool login(string user, string password)
        {
            if(dkey.Equals(string.Empty))
            {
                this.username = user;
                this.password = password;
                return GetDecryptionKey(user, password);
            }

            else
            {
                this.username = user;
                this.password = password;
                string commandResponse = sendCommand(user, password, "login", string.Empty);

                if (!commandResponse.Equals(string.Empty))
                {
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(commandResponse);
                    this.authorized = loginResponse.loggedin;
                    return loginResponse.loggedin;
                }

                this.authorized = false;
                return false;
            }
        }

        /// <summary>
        /// Attempt to log in to the server.
        /// </summary>
        /// <returns>The seconds left from the server as a string</returns>
        private int getTimeLeft()
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "username", this.username }
            };

            if (Authorized)
            {
                string commandResponse = sendCommand(this.username, this.password, "time_check", JsonConvert.SerializeObject(values));

                if(!commandResponse.Equals(string.Empty))
                {
                    TimeResponse timeResponse = JsonConvert.DeserializeObject<TimeResponse>(commandResponse);
                    return timeResponse.timeleft;
                }
            }

            return 0;
        }

        /// <summary>
        /// Checks if the user is logged in every 5 seconds.
        /// </summary>
        /// <returns></returns>
        private Task heartbeat()
        {
            while(this.login(this.Username, this.Password))
            {
                incrementor = 0;
                Thread.Sleep(5000);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Increments a int to coenside with the heartbeat.
        /// </summary>
        /// <param name="heartRhythm">Milliseconds to increment from between heart beats.</param>
        /// <returns></returns>
        private Task heartrate(int heartRhythm)
        {
            while (true)
            {
                incrementor++;
                Thread.Sleep(heartRhythm);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Attempt to redeem a key, return boolean result of attempt.
        /// </summary>
        /// <param name="timeKey"></param>
        /// <returns></returns>
        public bool redeemKey(string timeKey)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "key", timeKey }
            };

            if (Authorized)
            {
                string commandResponse = sendCommand(this.username, this.password, "redeem_key", JsonConvert.SerializeObject(values));

                if (!commandResponse.Equals(string.Empty))
                {
                    KeyResponse keyResponse = JsonConvert.DeserializeObject<KeyResponse>(commandResponse);
                    return keyResponse.keyres;
                }
            }

            return false;
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
            if (!(ekey.Equals(string.Empty) || ekey.Equals("0")) && !(dkey.Equals(string.Empty) || dkey.Equals("0")) && IsBase64String(ekey) && IsBase64String(dkey))
            {
                Dictionary<string, string> values = new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password },
                    { "cheese", command },
                    { "parms", parameters }
                };

                string Json = JsonConvert.SerializeObject(values);
                Task<string> runPostRequestTask = Task.Run(() => PostURI(new Uri("http://159.223.114.162/index.php"), new FormUrlEncodedContent(new Dictionary<string, string> { { "bluecheese", EncryptString(Json) } })));
                runPostRequestTask.Wait();

                return !runPostRequestTask.Result.Equals(string.Empty) ? DecryptString(runPostRequestTask.Result) : string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the Encryption Key
        /// </summary>
        private void GetEncryptionKey()
        {
            if (ekey.Equals(string.Empty))
            {
                var getEncryptionKey = Task.Run(() => PostURI(new Uri("http://159.223.114.162/index.php"), new FormUrlEncodedContent(new Dictionary<string, string> { { "cheese", "90kGPILHd22/yQ3bctAPwxzEPq+BEA4og3Wqh+hSRFQ=" } })));
                getEncryptionKey.Wait();
                this.ekey = getEncryptionKey.Result;
            }
        }

        /// <summary>
        /// Get the Decryption key
        /// </summary>
        private bool GetDecryptionKey(string username, string password)
        {
            if (dkey.Equals(string.Empty) && !(ekey.Equals(string.Empty) || ekey.Equals("0") || !IsBase64String(ekey)))
            {
                Dictionary<string, string> values = new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password },
                    { "cheese", "get_dkey" },
                    { "parms", string.Empty }
                };

                string Json = JsonConvert.SerializeObject(values);
                string EncryptedJson = EncryptString(Json);
                Task<string> response = Task.Run(() => PostURI(new Uri("http://159.223.114.162/index.php"), new FormUrlEncodedContent(new Dictionary<string, string> { { "bluecheese", EncryptedJson } })));
                response.Wait();

                if(!response.Result.Equals(string.Empty))
                {
                    DkeyResponse dkeyResponse = JsonConvert.DeserializeObject<DkeyResponse>(response.Result);

                    if (!(dkeyResponse.dkey.Equals(string.Empty) || dkeyResponse.dkey.Equals("0")) && IsBase64String(dkeyResponse.dkey))
                    {
                        this.heartRate = dkeyResponse.heartrate;
                        this.dkey = dkeyResponse.dkey;
                        Task.Run(() => heartbeat());
                        Task.Run(() => heartrate(dkeyResponse.heartrhythm));
                        this.authorized = true;
                        return true;
                    }
                }
            }

            this.authorized = false;
            return false;
        }

        /// <summary>
        /// Is this string a Base64 value?
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
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

                    else
                    {
                        response = string.Empty;
                    }
                }
                return response;
            }

            catch (HttpRequestException e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Encrypt a Key
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string EncryptString(string plainText)
        {
            string password = ekey;

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
            encryptor.Padding = PaddingMode.PKCS7;

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

        public string DecryptString(string cipherText)
        {
            string password = dkey;

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
            encryptor.Padding = PaddingMode.PKCS7;

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

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        /// Gets the Years this person has authed left.
        /// </summary>
        public int YearsLeft
        {
            get
            {
                return TimeSpan.FromSeconds(Convert.ToDouble(getTimeLeft())).Days / 365;
            }
        }

        /// <summary>
        /// Gets the Days this person has authed left.
        /// </summary>
        public int DaysLeft
        {
            get
            {
                return TimeSpan.FromSeconds(Convert.ToDouble(getTimeLeft())).Days;
            }
        }

        /// <summary>
        /// Gets the Hours this person has authed left.
        /// </summary>
        public int HoursLeft
        {
            get
            {
                return TimeSpan.FromSeconds(Convert.ToDouble(getTimeLeft())).Hours;
            }
        }

        /// <summary>
        /// Gets the Minutes this person has authed left.
        /// </summary>
        public int MinutesLeft
        {
            get
            {
                return TimeSpan.FromSeconds(Convert.ToDouble(getTimeLeft())).Minutes;
            }
        }

        /// <summary>
        /// Gets the seconds this person has authed left.
        /// </summary>
        public int SecondsLeft
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
                return !getTimeLeft().Equals(0);
            }
        }

        /// <summary>
        /// Veryifys the integrity of the heart beat.
        /// </summary>
        public bool HeartRate
        {
            get { return this.incrementor <= this.heartRate; }
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