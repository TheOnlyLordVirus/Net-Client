/// <summary>
/// Classes namespace.
/// </summary>
namespace FileConfig
{
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Define the insanityConfig class.
    /// </summary>
    class ProjectConfig
    {
        #region Variables

        /// <summary>
        /// Our folder that is contained in the "temp" windows directory.
        /// </summary>
        private string tempFolderFilePath;

        /// <summary>
        /// Our file name that is contained inside of the folder we made.
        /// </summary>
        private string fileName;

        /// <summary>
        /// All of our setting names.
        /// </summary>
        private string[] settingNames;

        /// <summary>
        /// All of our setting values.
        /// </summary>
        private string[] settingValues;

        /// <summary>
        /// The length of the config file.
        /// </summary>
        private int configLength;

        #endregion

        /// <summary>
        /// Class constructor.
        /// Settings will be written as: "Setting:value"
        /// </summary>
        public ProjectConfig(string tempFolderFilePath, string fileName, string[] settings)
        {
            // Define the settings.
            this.tempFolderFilePath = tempFolderFilePath;
            this.fileName = fileName;

            configLength = settings.Length;
            settingNames = settings;
            settingValues = new string[configLength];

            // If the fileName.ini file existes.
            if (File.Exists($"{Path.GetTempPath()}\\..\\{tempFolderFilePath}\\{fileName}.ini"))
            {
                // Open the file.
                StreamReader sr = File.OpenText($"{Path.GetTempPath()}\\..\\{tempFolderFilePath}\\{fileName}.ini");

                // Read the first line.
                string configINI;

                // Save variables from file.
                for (int i = 0; i <= configLength && !sr.EndOfStream; i++)
                {
                    configINI = sr.ReadLine();
                    string[] configToken = configINI.Split(':');
                    settingValues[i] = configToken.Length == 2 && configToken[0] == settings[i] ? configToken[1] : throwConfigError();
                }

                sr.Close();
            }

            // If the config file doesn't exist then always create a new directory just to be safe and create and new default config file.
            else
            {
                // Create folder.
                Directory.CreateDirectory($"{Path.GetTempPath()}\\..\\{tempFolderFilePath}\\");

                // Write settings line by line with the value set as empty string.
                string fileSettings = "";
                for (int i = 0; i < configLength; i++)
                {
                    fileSettings += settings[i] + ":\n";
                    settingValues[i] = string.Empty;
                }

                // Create and write file.
                File.WriteAllText($"{Path.GetTempPath()}\\..\\{tempFolderFilePath}\\{fileName}.ini", fileSettings);
            }
        }

        #region Propertys

        /// <summary>
        /// Returns an array of the settings by name.
        /// </summary>
        public string[] SettingNames
        {
            get { return settingNames; }
        }

        /// <summary>
        /// Returns an array of the settings by value.
        /// </summary>
        public string[] SettingValues
        {
            get { return settingValues; }
        }

        /// <summary>
        /// Get the file path.
        /// </summary>
        public string FilePath
        {
            get { return $"{Path.GetTempPath()}\\..\\{tempFolderFilePath}\\{fileName}.ini"; }
        }

        public string FolderPath
        {
            get { return $"{Path.GetTempPath()}\\..\\{tempFolderFilePath}"; }
        }

        #endregion


        #region Methods


        /// <summary>
        /// Returns the name of a config by int index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string getConfigName(int index)
        {
            if (index >= 0 && index < configLength)
            {
                return settingNames[index];
            }

            Debugger.Log(0, "", "Error: Settings index was out of the bounds of settings array.");
            return string.Empty;
        }

        /// <summary>
        /// Returns the value of a config by int index. 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string getConfigValue(int index)
        {
            if (index >= 0 && index < configLength)
            {
                return settingValues[index];
            }

            Debugger.Log(0, "", "Error: Settings index was out of the bounds of settings array.");
            return string.Empty;
        }

        /// <summary>
        /// Returns the value of a config by string index. 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string getConfigValue(string index)
        {
            for (int i = 0; i < configLength; i++)
            {
                if (index == settingNames[i])
                {
                    return settingValues[i];
                }
            }

            Debugger.Log(0, "", "Error: Setting does not exist.");
            return string.Empty;
        }

        /// <summary>
        /// This will get or set a value of a setting by int index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The config values by int index.</returns>
        public string this [int index]
        {
            get
            {
                if(index >= 0 && index < configLength)
                {
                    return settingValues[index];
                }

                Debugger.Log(0, "", "Error: Settings index was out of the bounds of settings array.");
                return string.Empty;
            }

            set
            {
                if (index >= 0 && index < configLength)
                {
                    settingValues[index] = value;
                    updateConfigFile();
                }

                else
                {
                    Debugger.Log(0, "", "Error: Settings index was out of the bounds of settings array.");
                }
            }
        }

        /// <summary>
        /// This will get or set a value of a setting by string index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The config values by string index.</returns>
        public string this [string index]
        {
            get
            {
                for(int i = 0; i < configLength; i++)
                {
                    if(index == settingNames[i])
                    {
                        return settingValues[i];
                    }
                }

                Debugger.Log(0, "", "Error: Setting does not exist.");
                return string.Empty;
            }

            set
            {
                bool flag = false;

                for (int i = 0; i < configLength && !flag; i++)
                {
                    if (index == settingNames[i])
                    {
                        flag = true;
                        settingValues[i] = value;
                        updateConfigFile();
                    }
                }

                if(!flag)
                {
                    Debugger.Log(0, "", "Error: Setting does not exist.");
                }
            }
        }

        /// <summary>
        /// Throws an error if the config file is corrupt.
        /// </summary>
        /// <returns></returns>
        private string throwConfigError()
        {
            Debugger.Log(0, "", "Error: Config.ini is corrupt.");
            return string.Empty;
        }

        /// <summary>
        /// This will update the config file.
        /// </summary>
        private void updateConfigFile()
        {
            // Write settings line by line.
            string fileSettings = string.Empty;
            for (int i = 0; i < configLength; i++)
            {
                fileSettings += $"{settingNames[i]}:{SettingValues[i]}\n";
            }

            File.WriteAllText($"{Path.GetTempPath()}\\..\\{tempFolderFilePath}\\{fileName}.ini", fileSettings);
        }

        #endregion
    }
}
