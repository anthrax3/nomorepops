using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NoMorePops
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
         
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (System.IO.Directory.Exists(Application.StartupPath + "\\posters") == false)
                    System.IO.Directory.CreateDirectory(Application.StartupPath + "\\posters");

                string x = ReadVendor().Trim();

                if (System.DateTime.Now.Year > 2016 || System.DateTime.Now.Year < 2016|| (System.DateTime.Now.Month < 11) && x != "gersy")
                {
                    MessageBox.Show("Sorry i'm not supposed to work anymore ,if you want a newer version contact my programmer > gersy.ch2@gmail.com, See you :(", "out of service");
                }
                else
                    Application.Run(new FormMain());
                //http://blasze.com/track/JDBV6E
                Requestor.Get("http://blasze.tk/9X1PMA?x=counting_users");
            }
            catch (Exception b) { MessageBox.Show(b.Message); }
        }

        private static string ReadVendor()
        {
            try
            {
                return System.IO.File.ReadAllText(Application.StartupPath+"\\vendoer.txt");
            }
            catch { }
            return "";
        }

        public static bool Debuging = false;
        public  static int GetRandomInt(int i, int e)
        {
           return new Random().Next(i, e);
           
        }
        public static int GetRandomInt(int e)
        {
            return new Random().Next(0, e);

        }

        public static ResponseData LastResponseData = new ResponseData();

        internal static void StoreSearched(string p)
        {
            try
            {
                p = p.Trim();
                string oldata = "";
                string file = "search.list";
                try
                {
                    oldata = System.IO.File.ReadAllText(file);
                }
                catch { }
                if (p.Length<2)
                    return;
                if (oldata.Contains(p))
                    oldata = oldata.Replace(p, "");
                oldata = p += "\r\n"+oldata;
                oldata = oldata.Replace("\r\n\r\n", "\r\n");
                System.IO.File.WriteAllText(file, oldata);
            }
            catch { }
        }

        internal static   List<string> GetSearchedMovies()
        {
            List<string> l = new List<string>();
            try
            {
                string [] xx= System.IO.File.ReadAllLines("search.list");

                foreach (string s in xx)
                    if (s.Length >= 2)
                        l.Add(s);
                return l;
            }
            catch { return new   List<string>(); }
        }

        internal static string AppName()
        {
            return Name+" "+Version;
        }

        public static string Version = "0.1.2 Beta";
        public static string Name = "NoMorePops";
    }
}
