using System;
using System.Net;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string file in Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}"))
            {
                if (file.Contains("Updater") && !file.Contains(".exe"))
                {
                    File.SetAttributes($"{file}", FileAttributes.Hidden);
                }
            }

            Console.Title = "Cheat Client Updater";

            if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}Cheat Client.exe"))
            {
                File.Delete($"{AppDomain.CurrentDomain.BaseDirectory}Cheat Client.exe");
            }

            WebClient web = new WebClient();
            web.DownloadProgressChanged += Web_DownloadProgressChanged;
            web.DownloadDataCompleted += Web_DownloadDataCompleted;
            Task<byte[]> getVersion = Task.Run(() => web.DownloadDataTaskAsync("http://159.223.114.162/update/Cheat Client.exe"));
            while (!getVersion.IsCompleted) ;
        }

        private static void Web_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            File.WriteAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}Cheat Client.exe", e.Result);
            Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}Cheat Client.exe");
        }

        private static void Web_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine($"Downloaded {e.BytesReceived} - {e.TotalBytesToReceive} (%{e.ProgressPercentage})");
        }
    }
}
