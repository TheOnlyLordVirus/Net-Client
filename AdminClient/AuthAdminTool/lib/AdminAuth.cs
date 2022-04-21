namespace KeyAuthorization
{
    using System;
    using System.Text;
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Cryptography;
    using System.IO;
    using System.Text.RegularExpressions;
     
    class AdminApi : ClientAuth
    {
        #region Response Structs

        private struct KeyResponse
        {
            public bool keyres;
        }

        private struct GenKeyResponse
        {
            public string key;
        }

        private struct AddUserResponse
        {
            public bool addres;
        }

        private struct DeleteUserResponse
        {
            public bool deleteres;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Class constructor
        /// </summary>
        public AdminApi()
        {
            GetEncryptionKey();
        }

        /// <summary>
        /// Generates a key if the user is logged in and is an admin.
        /// </summary>
        /// <returns></returns>
        public string GenerateKey(int dayValue, int keyAmount)
        {
            Dictionary<string, int> values = new Dictionary<string, int>
            {
                { "time_value", dayValue },
                { "key_amount", keyAmount }
            };

            if (Authorized)
            {
                string commandResponse = SendCommand(this.username, this.password, "add_key_bulk", JsonConvert.SerializeObject(values));

                if (!commandResponse.Equals(string.Empty))
                {
                    GenKeyResponse KeyResponse = JsonConvert.DeserializeObject<GenKeyResponse>(commandResponse);

                    return KeyResponse.key;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Generates a key if the user is logged in and is an admin.
        /// </summary>
        /// <returns></returns>
        public string GenerateKey(int dayValue)
        {
            Dictionary<string, int> values = new Dictionary<string, int>
            {
                { "time_value", dayValue }
            };

            if (Authorized)
            {
                string commandResponse = SendCommand(this.username, this.password, "add_key", JsonConvert.SerializeObject(values));

                if (!commandResponse.Equals(string.Empty))
                {
                    GenKeyResponse KeyResponse = JsonConvert.DeserializeObject<GenKeyResponse>(commandResponse);

                    return KeyResponse.key;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Attempt to redeem a key, return boolean result of attempt.
        /// </summary>
        /// <param name="timeKey"></param>
        /// /// <param name="username"></param>
        /// <returns></returns>
        public bool RedeemKey(string timeKey, string username)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "key", timeKey },
                { "username", username }
            };

            if (Authorized)
            {
                string commandResponse = SendCommand(this.username, this.password, "redeem_key", JsonConvert.SerializeObject(values));

                if (!commandResponse.Equals(string.Empty))
                {
                    KeyResponse keyResponse = JsonConvert.DeserializeObject<KeyResponse>(commandResponse);
                    return keyResponse.keyres;
                }
            }

            return false;
        }

        /// <summary>
        /// Attempt to add a user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public bool AddUser(string email, string username, string password, bool admin)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "email", email },
                { "username", username },
                { "password", password },
                { "admin", admin ? "1" : "0" }
            };

            if (Authorized)
            {
                string commandResponse = SendCommand(this.username, this.password, "add_user", JsonConvert.SerializeObject(values));

                if (!commandResponse.Equals(string.Empty))
                {
                    AddUserResponse userResponse = JsonConvert.DeserializeObject<AddUserResponse>(commandResponse);
                    return userResponse.addres;
                }
            }

            return false;
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeleteUser(string username)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "username", username }
            };

            if (Authorized)
            {
                string commandResponse = SendCommand(this.username, this.password, "delete_user", JsonConvert.SerializeObject(values));

                if (!commandResponse.Equals(string.Empty))
                {
                    DeleteUserResponse delUserResponse = JsonConvert.DeserializeObject<DeleteUserResponse>(commandResponse);
                    return delUserResponse.deleteres;
                }
            }

            return false;
        }

        /// <summary>
        /// Get a users time left
        /// </summary>
        /// <param name="username"></param>
        /// <returns>The seconds left until auth end date.</returns>
        public int GetTimeLeft(string username)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "username", username }
            };

            if (Authorized)
            {
                string commandResponse = SendCommand(this.username, this.password, "time_check", JsonConvert.SerializeObject(values));

                if (!commandResponse.Equals(string.Empty))
                {
                    TimeResponse timeResponse = JsonConvert.DeserializeObject<TimeResponse>(commandResponse);
                    return timeResponse.timeleft;
                }
            }

            return 0;
        }

        #endregion
    }
}
