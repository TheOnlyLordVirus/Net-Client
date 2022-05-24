using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    class Program
    {
        public static string name;
        static void Main(string[] args)
        {
            WebClient web = new WebClient();

            Console.Title = "Net Client Updater";

            name = web.DownloadString("http://159.223.114.162/update/name.txt");

            if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}{name}"))
            {
                File.Delete($"{AppDomain.CurrentDomain.BaseDirectory}{name}");
            }

            web = new WebClient();
            web.DownloadProgressChanged += Web_DownloadProgressChanged;
            web.DownloadDataCompleted += Web_DownloadDataCompleted;
            Task<byte[]> getVersion = Task.Run(() => web.DownloadDataTaskAsync($"http://159.223.114.162/update/{name}"));
            while (!getVersion.IsCompleted) ;
        }

        private static void Web_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                File.WriteAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}{name}", e.Result);
                System.Threading.Thread.Sleep(1000);
                Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}{name}");
            }

            catch (Exception Ex)
            {
                Console.WriteLine($"{Ex.Message}");
            }
        }

        private static void Web_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine($"Downloaded {e.BytesReceived} - {e.TotalBytesToReceive} (%{e.ProgressPercentage})");
        }
    }
}
