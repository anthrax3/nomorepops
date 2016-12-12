using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace NoMorePops
{
    public delegate void Callback_response(string s);
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Program.AppName();
               
           // this.Size = new Size(896, 252);
            this.MinimumSize = this.ClientSize;
            comboBox_tanslation.SelectedIndex = 0; comboBox_tanslation.SelectedIndex++;//one button is damaged and i'm lazy to open on-scr key
          
            comboBox_tanslation.Enabled = (Program.Debuging);
            this.SearchedMovies = Program.GetSearchedMovies();
            if (SearchedMovies.Count > 0)
                textBoxInput.Text = SearchedMovies[0].Trim();
        }

        private void rtxbx_Result_TextChanged(object sender, EventArgs e)
        {
       

        }

      //  public List<string> SearchMovies = new List<string>();

        public List<string> SearchedMovies = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            if (SearchingNow)
                return; 
            rtxbx_Result.Text = "";
            progressBar1.Value = 0;
            this.linkLabelCopyDownloadLink.Visible = false;
            labelStatue.Text = "";

            string n = textBoxInput.Text.Trim().Replace("فيلم", "").ToLower();
            this.currentMovie = n;
                
            if (n.Length < 2)
                return;
            labelStatue.Text = "Loading "+n;

            Program.StoreSearched(n);

            this.SearchedMovies = Program.GetSearchedMovies(); 
            string value = n;
            if (Requestor.ContainsArabic(n))
                value += " تحميل فيلم ";
            else 
            value +=Requestor.LanguageWords[comboBox_tanslation.SelectedIndex]; 
            System.IO.File.WriteAllText(Application.StartupPath + "\\last", value);
            System.Threading.Thread t = new System.Threading.Thread( this.Get);
            button1.Enabled = false;
            t.Start();
            this.SearchingNow = true; 
          
         }
        
        private void Get()
        {
            /*

             * 
            this.Invoke(new Loaddel(LoadResult), p);
             * */
             string value = System.IO.File.ReadAllText(Application.StartupPath + "\\last");
            SearchObject obj = Requestor.SearchGoogle(value );
            while (obj.SearchSucceeded == false && obj.Success)
                obj = Requestor.SearchGoogle(value);
            if (obj.Searchlines.Count < 3 - 2 || obj.Success==false)
                MessageBox.Show(obj.ErrorMessageFriendly(),"Error");
            else
            {
                if (obj.Searchlines.Count < 3 - 2)
                    MessageBox.Show("search returned 0 result");

                else if (Requestor.IsSupporttedWebsite(obj.Searchlines) == false)
                { 
                    MessageBox.Show("Can not proceed with received websites from " + obj.SearchMethod + " result");

                    this.Invoke(new Loaddel(LoadResult), obj.SearchlinesTostring());
                }
                else 
                {
                    FinalData result = new FinalData(""); 
                    bool got = false;
                    string fox = "";

                    fox = "Searching links\n";
                    foreach (string g in obj.Searchlines)
                        fox += g + "\n";
                    this.Invoke(new Loaddel(LoadResult), fox);

                    foreach (string l in obj.Searchlines)
                    {
                        if (got) 
                            break;
                        if (Requestor.IsSupporttedWebsite(l) == false)
                            continue;
                          result = Requestor.ProcessCima4U(l);

                        if (result.HasData())
                        {
                           
                            if (result.DownloadList.Count > 0)
                            {
                                  fox= "\nDownload Links \n";
                                foreach (string xl in result.DownloadList)
                                    fox += xl + "\n";
                                this.Invoke(new Loaddel(LoadResult), fox);
                                System.Threading.Thread.Sleep(999);

                            }
                            if (result.WatchList.Count > 0)
                            {
                                fox = "\nWatching Links \n";

                                foreach (string xl in result.WatchList)
                                   fox += xl + "\n";
                                if (result.WatchList.Count > 0)
                                    this.Invoke(new Loaddel(LoadResult), fox);

                            }
                            if (result.DirectStreamList.Count > 0)
                            {
                                fox = "\nDirect Download Links \n";

                                foreach (string xl in result.DirectStreamList)
                                    fox += xl + "\n";

                                this.Invoke(new Loaddel(LoadResult), fox);
                                System.Threading.Thread.Sleep(999);

                            }
                            got = true;
                        }
                        else if (result.Success == false)
                        {
                            fox = result.ResponseData.ResponseErrorMessage + " " + result.ResponseData.ErrorUrl;
                        }
                    }
                     

                    //if (got)
                    //    this.Invoke(new Loaddel(LoadResult), result);
                }
                
            }
            Load_Poster(true);
            try{this.Invoke(new ActiveDel(ActivateButton));}catch{}
        }
        public delegate void ActiveDel();
        private void ActivateButton()
        {
            button1.Enabled = true;
            this.SearchingNow = false;
            this.progressBar1.Value = 0;
            labelStatue.Text = "Finished";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented");
            if (this.CurrentWatchLink.Length < 5)
                return;
            FrmWatcher f = new FrmWatcher(this.CurrentWatchLink);
            f.Text = this.currentMovie;
            f.Icon = this.Icon;
            f.Show();
        }
        public delegate void Loaddel(object b);
        public void LoadResult(object o)
        {
            if (o is string)
            {
                string p = o.ToString();
                if (p.StartsWith("\nDownload") || (rtxbx_Result.Text.Contains("searching")))
                    this.rtxbx_Result.Text = p;
                

                else
                    this.rtxbx_Result.Text += p;


                string[] spl = p.Split('\n');
                string target = "";                     
                for (int i = spl.Length - 3 + 2; i >= 0; i--)
                    if (spl[i].Trim().StartsWith("http"))
                    {
                        target=spl[i].Trim();
                    }

                if (p.StartsWith("Searching links"))
                    progressBar1.Value = 25;
                if (p.StartsWith("\nDownload Links"))
                    progressBar1.Value = 50;
                if (p.StartsWith("\nWatching Links \n"))
                {
                    progressBar1.Value = 75;
                    this.CurrentWatchLink = target;
                
                } if (p.StartsWith("\nDirect Download Links"))
                {
                    this.CurrentDirectLink = target;
                    this.linkLabelCopyDownloadLink.Visible = true;
                    progressBar1.Value = 99;
                }
            }
             if(this.Size.Height<=252)
            this.Size = new System.Drawing.Size(896, 528);


        }

        private void linkLabelDebug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRaw f = new FrmRaw(Program.LastResponseData.FirstUrl+"   @"+Program.LastResponseData.ref_, Program.LastResponseData.GetResponseMessage()+(Program.LastResponseData.HasError?Program.LastResponseData.ResponseErrorMessage:"")); f.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            labelAutoComplete.Visible = false;
            labelArabic.Visible=Requestor.ContainsArabic(textBoxInput.Text);
            if(textBoxInput.TextLength>=2)
            foreach (string s in this.SearchedMovies)
            {
                string x = textBoxInput.Text.Trim();
                if (s.Contains(x) && s!=x)
                {
                    labelAutoComplete.Text = s;
                    labelAutoComplete.Visible = true;
                }
            }

            timerPoster.Start();
            timer_poster_ticks = 0;
           
        }
        public int timer_poster_ticks = 0;
        private void Load_Poster(bool Search = false)
        {
            bool found = false;
            string xb = Application.StartupPath + "\\posters\\" + textBoxInput.Text.Trim() + "_.b64";
            this.pictureBox1.Image = null;
            try
            {
                if (System.IO.File.Exists(xb))
                {

                    string b64 = System.IO.File.ReadAllText(xb);
                    if (b64.Contains(","))
                        b64 = b64.Split(',')[0];

                  found=  setImageFromBase64(b64);
                }
            }
            catch { }

            
           if(Search  && found==false)
            {
                var t = new System.Threading.Thread(LoadPosterThreadPoc);
                t.Start();
            }
        }

        public delegate void UpdatePicBox();
        public void LoadPicBox()
        {
            Load_Poster();
        }

        public void LoadPosterThreadPoc()
        {
            GetPosterOnline();
            this.Invoke(new UpdatePicBox(LoadPicBox));
        }


        private void GetPosterOnline()
        {
            string nx = textBoxInput.Text.Replace("film", "");
            Program.StoreSearched(nx);
            nx+="+film";
            string px = Requestor.GetGoogleBase64ImageFromSearch(nx);
            try
            {
                if (px.StartsWith(","))
                    px = px.Substring(3 - 2);
                if (px.Contains('"'))
                    px = px.Split('"')[3 - 2];
                if (setImageFromBase64(px))
                {
                    pictureBox1.Tag = textBoxInput.Text;
                    System.IO.File.WriteAllText(Application.StartupPath + "\\posters\\" + pictureBox1.Tag.ToString() + "_.b64", px);
                }

            }
            catch { }
        }

        private bool setImageFromBase64(string px)
        {
            try
            {
                px = px.Replace("\\u003d", "=");
                byte[] bytes = Convert.FromBase64String(px);
                Image image = null;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                if (image != null)
                {
                    pictureBox1.Tag = textBoxInput.Text;
                    pictureBox1.Image = image;
                    this.loadingPosterNow = false;
                    return true;
                }
            }
            catch { }
            return false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.loadingPosterNow == false)
            {
                Load_Poster(true);
                this.loadingPosterNow = true;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormLoadRecents f = new FormLoadRecents(this.SearchedMovies);
            f.ShowInTaskbar = false;
            f.Icon = this.Icon;
            if (f.ShowDialog() == DialogResult.OK)
            {  string it = f.GetSelectedItem();
                if(it.Length>=2)
                    this.textBoxInput.Text = it;
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Get();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("done");
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void labelAutoComplete_Click(object sender, EventArgs e)
        {
            if (labelAutoComplete.Text.Length >= 2)
                textBoxInput.Text = labelAutoComplete.Text;
        }

        public bool SearchingNow { get; set; }

        public bool loadingPosterNow = false;

        private void timerPoster_Tick(object sender, EventArgs e)
        {
            timer_poster_ticks++;
            if (timer_poster_ticks == 3)
            {
                timer_poster_ticks = 0;
                timerPoster.Stop();
                Load_Poster(true);   
            }
        }

        public string CurrentDirectLink ="";

        private void linkLabelCopyDownloadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.CurrentDirectLink.Length > 0)
            {
                Clipboard.SetText(this.CurrentDirectLink);
                labelStatue.Text = "copied ";
            } 
        
        }
        FormAbout f;
        private void AboutLnkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
             f= new FormAbout();
            f.Icon = this.Icon;
            f.Hide();
            f.ShowDialog();
        }

        public string CurrentWatchLink="";

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            MessageBox.Show("downloader not implemented");
        }

        public string currentMovie { get; set; }
    }
}
