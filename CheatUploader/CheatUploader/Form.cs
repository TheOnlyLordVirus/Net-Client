namespace CheatUploader
{
    using Renci.SshNet;
    using System.Text;
    using Newtonsoft.Json;

    public partial class MainForm : Form
    {
        private struct CheatItems
        {
            public string shortname;
            public string classname;
            public string cheatname;
            public string description;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void uploadFileButton_Click(object sender, EventArgs e)
        {
            byte[] fileContent;
            string fileName;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Filter = "dll files (*.dll)|*.dll";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog().Equals(DialogResult.OK))
                {
                    //Get the path of specified file
                    fileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);

                    fileContent = File.ReadAllBytes(openFileDialog.FileName);

                    var host = "159.223.114.162";
                    var port = 22;
                    var username = "root";
                    var password = "@Skank42Hunt";

                    using (var client = new SftpClient(host, port, username, password))
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            List<CheatItems> json = new List<CheatItems>();

                            if (client.Exists("/var/www/cheats/external/x64games.json"))
                            {
                                byte[] jsonBytes = client.ReadAllBytes("/var/www/cheats/external/x64games.json");
                                json = JsonConvert.DeserializeObject<List<CheatItems>>(Encoding.UTF8.GetString(jsonBytes));

                                int iJson = 0;
                                CheatItems cheat = new CheatItems();
                                foreach (CheatItems cheatx in json)
                                {
                                    if (cheatx.shortname.Equals(fileName))
                                    {
                                        cheat = cheatx;
                                        client.DeleteFile($"/var/www/cheats/external/{fileName}/{fileName}.dll");
                                        client.DeleteDirectory($"/var/www/cheats/external/{fileName}");
                                    }
                                }

                                json.Remove(cheat);
                                client.DeleteFile("/var/www/cheats/external/x64games.json");
                            }

                            json.Add
                            (
                                new CheatItems
                                {
                                    shortname = fileName,
                                    classname = fileName + ".MainForm",
                                    cheatname = nameTextbox.Text,
                                    description = descriptionTextbox.Text
                                }
                            );

                            client.AppendAllText("/var/www/cheats/external/x64games.json", JsonConvert.SerializeObject(json));

                            using (MemoryStream ms = new MemoryStream(fileContent))
                            {
                                client.CreateDirectory("/var/www/cheats/external/" + fileName);
                                client.ChangeDirectory("/var/www/cheats/external/" + fileName);
                                client.UploadFile(ms, fileName + ".dll");
                            }

                            MessageBox.Show("Cheat Uploaded!");
                        }
                    }
                }
            }
        }
    }
}