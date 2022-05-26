namespace KeyAuthorization
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
     
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

        private struct BanUserResponse
        {
            public bool banres;
        }

        private struct UserIpResetResponse
        {
            public bool resetres;
        }

        private struct IsAdminResponse
        {
            public bool isadmin;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Class constructor
        /// </summary>
        public AdminApi() : base(true) { Console.WriteLine("Debug: AdminApi instance Created!"); }

        /// <summary>
        /// Is the another user an admin?
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin(string user, string pass)
        {
            if (Authorized)
            {
                string commandResponse = SendCommand(user, pass, "is_admin", "");

                if (!commandResponse.Equals(string.Empty))
                {
                    IsAdminResponse AdminResponse = JsonConvert.DeserializeObject<IsAdminResponse>(commandResponse);

                    return AdminResponse.isadmin;
                }
            }
            return false;
        }

        /// <summary>
        /// Is the current user an admin?
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {
            if (Authorized)
            {
                string commandResponse = SendCommand(this.username, this.password, "is_admin", "");

                if (!commandResponse.Equals(string.Empty))
                {
                    IsAdminResponse AdminResponse = JsonConvert.DeserializeObject<IsAdminResponse>(commandResponse);

                    return AdminResponse.isadmin;
                }
            }
            return false;
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

            if (Authorized && IsAdmin())
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

            if (Authorized && IsAdmin())
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

            if (Authorized && IsAdmin())
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

            if (Authorized && IsAdmin())
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
        public bool BanUser(string username)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "username", username }
            };

            if (Authorized && IsAdmin())
            {
                string commandResponse = SendCommand(this.username, this.password, "ban_user", JsonConvert.SerializeObject(values));

                if (!commandResponse.Equals(string.Empty))
                {
                    BanUserResponse delUserResponse = JsonConvert.DeserializeObject<BanUserResponse>(commandResponse);
                    return delUserResponse.banres;
                }
            }

            return false;
        }

        /// <summary>
        /// Reset a user ip.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ResetIpUser(string username)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "username", username }
            };

            if (Authorized && IsAdmin())
            {
                string commandResponse = SendCommand(this.username, this.password, "reset_ip", JsonConvert.SerializeObject(values));

                if (!commandResponse.Equals(string.Empty))
                {
                    UserIpResetResponse delUserResponse = JsonConvert.DeserializeObject<UserIpResetResponse>(commandResponse);
                    return delUserResponse.resetres;
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

            if (Authorized && IsAdmin())
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
