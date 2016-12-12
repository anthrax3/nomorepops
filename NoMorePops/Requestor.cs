using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace NoMorePops
{
    class Requestor
    {
        public static   List<string> LanguageWords = new List<string>() { "", "+%D9%85%D8%AA%D8%B1%D8%AC%D9%85", "translated", "Traduire" };
        public static   List<string> User_Agents = new List<string>() { " Mozilla/5.0 (Windows NT 6.1; rv:49.0) Gecko/20100101 Firefox/49.0" };
        private static List<string> SupportedSites = new List<string>() { "cima4u.tv","upbom.com" };

        public static bool IsSupporttedWebsite(string url)
        {

           string domain=Requestor.TopLeastDomain(url);

           foreach (string s in SupportedSites)
               if (s == domain)
                   return true;
            return false;
        }
        public static string TopLeastDomain(string d)
        {
            //x.google.com
            // googl.com
            d = TrimUrlProtocol(d);
            if (d.Contains("/"))
                d = d.Split('/')[0];
            int dots = 0;
            foreach (char c in d)
                if (c == '.')
                    dots++;
            if (dots == 3 - 2)
                return d;
            if (dots==0)
                return d;
            //gersy.domain.top                    com._._._
            string[] splted = d.Split('.');
            int ier = (d.Contains(".com.") || d.Contains(".co.")) ? 3 - 2 : 0; 
            for (int i = splted.Length - (3 - 2+ier); i>=0; i--)
            {
                if (splted[i].Length < 2 && i > 3 - 2)
                    continue;
                return splted[i - 3 + 2] + "." + splted[i];
            }
            return d;
        }
        private static string DomainFromUrl(string url)
        {
            //yasser:gersy@goog.com:80
            url=Requestor.TrimUrlProtocol(url);
            if (url.Contains("/"))
                url = url.Split('/')[0];
            if ( url.Contains('@'))
                url=url.Split('@')[3-2];

            if (url.Contains(':'))
                url=url.Split(':')[3-2];
            return url;
            
        }

         private static string TrimUrlProtocol(string url)
           {if (url.StartsWith("http://"))
                url = url.Substring("http://".Length);

            if (url.StartsWith("https://"))
                url = url.Substring("https://".Length);
             return url;
         }
         private static ResponseData POST(string url, string body, bool Foloredi = false)
         {
             return HTTP("POST", url,body, Foloredi);
         }

         public static ResponseData Get(string url, bool Foloredi = false)
         {
             return HTTP("GET", url,"", Foloredi);
         }
        public static ResponseData HTTP(string method ,string url,string POST_Data_string,bool Foloredi=false)
        {
           ResponseData resData = new ResponseData(url);

           if (url.StartsWith("http://")==false)
           if  (  url.StartsWith("https://")==false)
               url = "http://" + url;
           
            resData.FirstUrl = url;
            if (url.Replace("http://", "").Length < 5 || url.Replace("https", "").Length < 5)
                return resData;
            try
            {
                url = url.Replace(' ', '+');
                var reeq = (HttpWebRequest)WebRequest.Create(url);
                reeq.UserAgent = User_Agents[Program.GetRandomInt(User_Agents.Count)];
                reeq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                reeq.Headers.Add("accept-language", "en-US,en;q=0.5");
                reeq.Referer = url;
                reeq.Method = "GET";
                if (method.ToLower() == "post")
                {
                    reeq.Method = "POST";
                    var POST_data = Encoding.ASCII.GetBytes(POST_Data_string);
                    reeq.ContentLength = POST_data.Length;
                    reeq.ContentType = "application/x-www-form-urlencoded";
                    using (var strm = reeq.GetRequestStream())
                    {
                        strm.Write(POST_data, 0, POST_data.Length);
                    }
                    
                }
                string cokstr = GetSiteStaticCookie(url);
                if (cokstr.Length > 2)
                    reeq.Headers.Add("cookies", cokstr);
                    //"  r.Referer = url;
                reeq.AllowAutoRedirect = Foloredi;
                var resp = (HttpWebResponse)reeq.GetResponse();
                resData.success = true;
                var text = new StreamReader(resp.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                resData.body = text;
                for (int i = 0; i < resp.Headers.Count; i++)
                {
                    string key = "", val = "";
                    try
                    {
                        key = resp.Headers.GetKey(i);
                        if (key.Length < 2)
                            continue;
                        val = resp.Headers[key].ToString();
                        if (key.ToLower().Trim() == "location")
                        {
                            resData.redirectedTo = val; 
                        }
                        bool added = false;

                        if(resData.Headers.ContainsKey(key)) 
                        {
                            resData.Headers[key] += val;
                                added = true;
                                break;
                             
                        }
                    else 
                            resData.Headers.Add(key, val);

                    }
                    catch
                    {
                        try {  resData.Headers[key] = val; }
                        catch { }
                    }
                }

                Program.LastResponseData = resData;
                return resData;
            }
            catch (Exception x)
            {

                resData.SetResponseError(x.Message);
                if (x.Message.Contains("The operation has timed out") ||x.Message.Contains("Unable to connect to the remote server")|| x.Message.Contains("The remote name could not be resolved"))
                {
                    resData.errorcode = 0;
                    resData.success = false;
                    resData.ErrorUrl = url;
                    resData.RequiresReplay = true;

               
                   
                }
                else
                {
                    try
                    {
                        var s = (x as System.Net.WebException).Response;
                        var sc = s.GetResponseStream();
                        string body = new StreamReader(sc).ReadToEnd();
                        string responseMessageHeaders = "HTTP/1.1 " + (resData.errorcode) + " " + "\n";
                        for (int i = 0; i < s.Headers.Count; i++)
                        {
                            try
                            {
                                string k = s.Headers.GetKey(i);
                                string v = s.Headers[k];
                                responseMessageHeaders += k + ": " + v + "\n";

                                resData.Headers.Add(k, v);
                            }
                            catch { }
                        }
                        resData.body = body;
                        resData.ResponseHeaders = responseMessageHeaders;
                        resData.HasError = true; resData.SetResponseError(x.Message);

                    }
                    catch (Exception xsa)
                    { resData.HasError = true; resData.ResponseErrorMessage = (xsa.Message); }
                }
            }
            return resData;
        }
        private   Dictionary<string, string> StaticCookisDic = InitCookies();
        private static  List<string> GoogleDomains = new List<string>() { "google.com", "google.ac", "google.ad", "google.ae", "google.com.af", "google.com.ag", "google.com.ai", "google.al", "google.am", "google.co.ao", "google.com.ar", "google.as", "google.at", "google.com.au", "google.az", "google.ba", "google.com.bd", "google.be", "google.bf", "google.bg", "google.com.bh", "google.bi", "google.bj", "google.com.bn", "google.com.bo", "google.com.br", "google.bs", "google.bt", "google.co.bw", "google.by", "google.com.bz", "google.ca", "google.com.kh", "google.cc", "google.cd", "google.cf", "google.cat", "google.cg", "google.ch", "google.ci", "google.co.ck", "google.cl", "google.cm", "google.cn", "google.com.co", "google.co.cr", "google.com.cu", "google.cv", "google.com.cy", "google.cz", "google.de", "google.dj", "google.dk", "google.dm", "google.com.do", "google.dz", "google.com.ec", "google.ee", "google.com.eg", "google.es", "google.com.et", "google.fi", "google.com.fj", "google.fm", "google.fr", "google.ga", "google.ge", "google.gf", "google.gg", "google.com.gh", "google.com.gi", "google.gl", "google.gm", "google.gp", "google.gr", "google.com.gt", "google.gy", "google.com.hk", "google.hn", "google.hr", "google.ht", "google.hu", "google.co.id", "google.iq", "google.ie", "google.co.il", "google.im", "google.co.in", "google.io", "google.is", "google.it", "google.je", "google.com.jm", "google.jo", "google.co.jp", "google.co.ke", "google.ki", "google.kg", "google.co.kr", "google.com.kw", "google.kz", "google.la", "google.com.lb", "google.com.lc", "google.li", "google.lk", "google.co.ls", "google.lt", "google.lu", "google.lv", "google.com.ly", "google.co.ma", "google.md", "google.me", "google.mg", "google.mk", "google.ml", "google.com.mm", "google.mn", "google.ms", "google.com.mt", "google.mu", "google.mv", "google.mw", "google.com.mx", "google.com.my", "google.co.mz", "google.com.na", "google.ne", "google.com.nf", "google.com.ng", "google.com.ni", "google.nl", "google.no", "google.com.np", "google.nr", "google.nu", "google.co.nz", "google.com.om", "google.com.pk", "google.com.pa", "google.com.pe", "google.com.ph", "google.pl", "google.com.pg", "google.pn", "google.com.pr", "google.ps", "google.pt", "google.com.py", "google.com.qa", "google.ro", "google.rs", "google.ru", "google.rw", "google.com.sa", "google.com.sb", "google.sc", "google.se", "google.com.sg", "google.sh", "google.si", "google.sk", "google.com.sl", "google.sn", "google.sm", "google.so", "google.st", "google.sr", "google.com.sv", "google.td", "google.tg", "google.co.th", "google.com.tj", "google.tk", "google.tl", "google.tm", "google.to", "google.tn", "google.com.tr", "google.tt", "google.com.tw", "google.co.tz", "google.com.ua", "google.co.ug", "google.co.uk", "google.us", "google.com.uy", "google.co.uz", "google.com.vc", "google.co.ve", "google.vg", "google.co.vi", "google.com.vn", "google.vu", "google.ws", "google.co.za", "google.co.zm", "google.co.zw", };

        private static Dictionary<string, string> InitCookies()
        {
            Dictionary<string, string> k = new Dictionary<string, string>();
            k.Add("cima4u.tv", " art_cnt_26304=26304; __test; __cfduid=dc072b9adaf9c86d6de1877a219047b7a1478022664; ppu_main_6ff15385ef7ea240cecda39339f70dee=1; ppu_sub_6ff15385ef7ea240cecda39339f70dee=1; ppu_delay_6ff15385ef7ea240cecda39339f70dee=1");
            k.Add("google.com", "NID=89=WHphb8deqCxp7pUOZVR98wAaPl-Wg7DdNeo8AF9Qgl5oan2I38fpP-fFYh-TDsBP_n3Hf_RJKwo3PcladpadEB09L2dMdjSkPBH9g35Q8e7V4RL9jG8lfY85vOSwqCJ0; OGPC=5061821-4:; GZ=Z=1; OGP=-5061821");
            k.Add("upbom.com", "aff=398; lang=english");
            return k;
        }
        private static string GetSiteStaticCookie(string url)
        {
            var p =new Requestor();
            string d = Requestor.TopLeastDomain(url);
            if (GoogleDomains.Contains(d))
                d = "google.com";
            if(p.StaticCookisDic.ContainsKey(d))
                return p.StaticCookisDic[d];

            return ""; 

        }
        internal static FinalData ProcessCima4U(string p)
        {
            FinalData f = new FinalData(p);
            if (p.Length < 4)
            {
                f.Error = true;
                f.erroMessage = "invalid url";
                return f;
            }
            ///one
            int red = 0;
            bool Relooop = true;
            while (Relooop)
            {
                Relooop = false;
                f.ResponseData = Requestor.Get(f.ResponseData.FirstUrl);
                // if live
                string ptx="<meta itemprop=\"embedURL\" content=\"";
                if (f.ResponseData.body.Contains(ptx) && f.ResponseData.FirstUrl.EndsWith("/"))
                {
                    string[] xc = f.ResponseData.body.Split(new string[] { ptx}, StringSplitOptions.RemoveEmptyEntries);
                    string u = "";
                    foreach (string s in xc)
                    {

                        if (s.StartsWith("http"))
                        {
                            if (s.Contains('"'))
                                u = s.Split('"')[0];

                        }
                    }

                    f.ResponseData = Requestor.Get(u);

                }
                    ///two 
                while (f.ResponseData.redirectedTo.Length > 2 && red < 9 || f.ResponseData.IsTimeOutError())
                {
                    red++;
                    f.ResponseData = Requestor.Get(f.ResponseData.redirectedTo);
                }
                // Check if is movie  series
                // http://cima4u.tv/ old/rel110.html http://cima4u.tv /rel110.html
                if (f.ResponseData.FirstUrl.Contains("/old/rel") || f.ResponseData.FirstUrl.Contains(".tv/rel"))
                {
                    string current = CurrenUriDirectory(f.ResponseData.FirstUrl);
                    string ptrn = "html' class='title_a'>";
                    string[] arx = f.ResponseData.body.Split(new string[] { ptrn }, StringSplitOptions.RemoveEmptyEntries);
                    string url = "";
                    foreach (string a in arx)
                        if (a.EndsWith("."))
                        {
                            if (a.Contains("'"))
                            {
                                string[] zd = a.Split('\'');
                                string org = zd[zd.Length - 3 + 2];
                                url = current + org + "html";
                                break;

                            }
                        }
                    f.ResponseData = Requestor.Get(url);
                }
                //                  extract from old 
                //<meta http-equiv='refresh' content='5;url=cima4u.tv/online/?p=4262' />
                if (f.ResponseData.body.Contains("' class='results_link'><div class='click_to_dwn'></div></a>"))
                {
                    string[] ops = f.ResponseData.body.Split(new string[] { "'><div class='click_to_dwn'></div></a>", /*"href='"*/ }, StringSplitOptions.RemoveEmptyEntries);
                    if (ops[0].Contains("<a href='"))
                    {
                        string first = ops[0];
                        if (first.EndsWith("' class='results_link"))
                            first = first.Substring(0, first.Length - "' class='results_link".Length);
                        string[] tmp = first.Split(new string[] { "<a href='" }, StringSplitOptions.RemoveEmptyEntries);

                        f.List.Clear();
                        // for (int l = 0; l < ops.Length; l++)
                        //  {
                        string sxx = "http://cima4u.tv/" + System.Uri.EscapeDataString(tmp[tmp.Length - 3 + 2]);
                        f.List.Add(sxx);
                        //   } 

                    }
                }
                if (f.List.Count > 0)
                {
                    f.ResponseData = Requestor.Get(f.List[0], true);
                }

                string pt = "/span><a href='";
                string p2 = "";
                //three
                if (f.ResponseData.body.Contains(pt))
                {
                    string[] arr = f.ResponseData.body.Split(new string[] { pt }, StringSplitOptions.RemoveEmptyEntries);
                    f.List.Clear();
                    f.List.Add(arr[3 - 2].Split('\'')[0]);
                    f.ResponseData = Requestor.Get(f.List[0], true);
                }
                //four
                if (f.ResponseData.body.Contains("<div class=\"ads_r\">") && f.ResponseData.body.Contains("000000000000"))
                {
                    f.RemovedByAdmin = true;
                    return f;
                }
                string ptr2 = "<div class=\"col-md-12\"><a href=\"";
                string ptr = "<a target=\"_blank\" href=\"";
                string pt0 = "<li data-server=\"";

                if (f.ResponseData.FirstUrl.Contains("live.cima4u.tv") || f.ResponseData.FirstUrl.Contains("cima4u.tv/online/?p=") || f.ResponseData.FirstUrl.Contains("cima4u.tv/?p="))
                {

                    string pid = "";
                    if (f.ResponseData.FirstUrl.Contains('='))
                    {
                        pid = f.ResponseData.FirstUrl.Split('=')[3 - 2];
                    }
                    if (f.ResponseData.body.Contains(pt0))
                    {
                        string[] frameslices = f.ResponseData.body.Split(new string[] { pt0 }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string fxz in frameslices)
                        {
                            string fx = fxz.Trim();
                            if (fx[0] > '9' || fx[0] < '0' || fx.Length == 0)
                                continue;
                            if (fx.Contains('"'))
                                fx = fx.Split('"')[0];

                            fx = "http://online.cima4u.tv/wp-content/themes/YourColor/video.php?server=" + fx.Replace(" ", "") + "&pid=" + pid;
                            f.IframesList.Add(fx);
                        }
                    }
                    //Proceeding Direct Watch
                    if (f.ProceedDirectWatch == true)
                    {
                        foreach (string ifr in f.IframesList)
                        {
                            var rc = Requestor.Get(ifr, true);
                            string ptrn = "<iframe width=\"100%\" height=\"100%\" src=\"";
                            string ptrn2 = "\" frameborder=\"0\"";
                            if (rc.body.Contains(ptrn) && rc.body.Contains(ptrn2))
                            {
                                string[] arrrr = rc.body.Split(new string[] { ptrn, ptrn2 }, StringSplitOptions.RemoveEmptyEntries);
                                f.WatchList.Add(arrrr[3 - 2]);

                            }
                        }

                    }
                    if (f.ResponseData.body.Contains(ptr) || f.ResponseData.body.Contains(pt0))
                    {

                        string m_b = ptr2;
                        if (f.ResponseData.body.Contains(ptr2) == false)
                            m_b = ptr;

                        if (f.ResponseData.body.Contains(ptr) == false)
                            m_b = pt0;

                        string[] Download_urls = f.ResponseData.body.Split(new string[] { ptr }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string sxrx in Download_urls)
                        {
                            string vx = sxrx;
                            if (sxrx.StartsWith("http") == false)
                                continue;
                            if (sxrx.Contains('"'))
                                vx = sxrx.Split('"')[0];

                            // bypass websites main pages
                            string b = vx.Replace("//", "");
                            if (b.Contains("/") == false)
                                continue;

                            //if (sxrx.Contains('"'))
                            //    vx=sxrx.Split('"')[3-2];
                            f.DownloadList.Add(vx);
                            f.Success = true;


                        }
                    } 
                   

                }

                if (f.ResponseData.FirstUrl.Contains("live.cima4u.tv"))
                {
                    if (f.ResponseData.body.Contains("Website . http://azouzplus.cf"))
                    {
                        string[] xxz = f.ResponseData.body.Split(new string[] { ptr2 }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string dlink in xxz)
                        {
                            if (dlink.StartsWith("http") == false)
                                continue;
                            f.DownloadList.Add(dlink.Split('"')[0]);
                        }

                        /// watching  structure/server.php?id=9292
                        string Tvurl = "http://live.cima4u.tv/structure/server.php?id=";
                        string tvpatrn = "</a><a href=\"\" data-link=";
                        string[] tvslices = f.ResponseData.body.Split(new string[] { tvpatrn }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string tv_slic in tvslices)
                        {
                            if (tv_slic.StartsWith("\"") == false)
                                continue;
                            string[] tmp = tv_slic.Substring(3 - 2).Split('"');
                            string newu = Tvurl + tmp[0];
                            f.IframesList.Add(newu);
                        }
                        if (f.ProceedDirectWatch == true)
                        {
                            foreach (string ifr in f.IframesList)
                            {
                                var rc = Requestor.Get(ifr, true);
                                string ptrn = "<iframe width=\"100%\" height=\"460\" src=\"";

                                string ptrn2 = "\" SCROLLING=\"";
                                if (rc.body.Contains(ptrn) && rc.body.Contains(ptrn2))
                                {
                                    string[] arrrr = rc.body.Split(new string[] { ptrn, ptrn2 }, StringSplitOptions.RemoveEmptyEntries);
                                    f.WatchList.Add(arrrr[0]);

                                }
                            }

                        }

                        if (f.DownloadList.Count == 0 && f.WatchList.Count == 0)
                        {
                            if (f.List.Count > 0)
                            {
                                Relooop = true;
                                f.ResponseData.FirstUrl = f.List[0];
                            }
                        }
                    }
                }
            }

            ///Process DirectStreamLinks
            if (f.DownloadList.Count > 0)
            { 
                foreach (string dl in f.DownloadList)
                {
                    if (Requestor.IsSupporttedWebsite(dl))
                    {
                        FinalData fx = Requestor.Process(dl);
                        if (fx.DirectStreamList.Count > 0)
                            f.DirectStreamList.Add(fx.DirectStreamList[0].Replace(' ','+'));

                    }
                }
            }
            return f;
            
        }

        private static FinalData Process(string dl)
        {

            if (dl.Contains("http://upbom.com"))
                return Process_upBom(dl);

            if (dl.Contains("cima4u.tv/"))
                return ProcessCima4U(dl);
            return null;
        }

        private static FinalData Process_upBom(string dl)
        {
            //http://upbom.com/o8k4hok4rscc/PH46.Transcendence_2014.mp4.html
            //Get  POST POST
            FinalData f = new FinalData(dl);
            f.ResponseData = Requestor.Get(dl);

            string id_parameter = "";
            while (f.ResponseData.RequiresReplay)
                f.ResponseData = Requestor.Get(dl); 
            string patrn_id = "<input type=\"hidden\" name=\"id\" value=\"";
            if (f.ResponseData.body.Contains(patrn_id))
            {
                ////Extract file name
                //string[] x = f.ResponseData.body.Split(new string[] { patrn_fname }, StringSplitOptions.RemoveEmptyEntries);
                //string [] x2 = x[3 - 2].Split('"');
                //f.Parametrs.Add("fname", x2[0]);

                //Extract id
                string[] id_slices = f.ResponseData.body.Split(new string[] { patrn_id }, StringSplitOptions.RemoveEmptyEntries);
                string[] id2_slices = id_slices[3 - 2].Split('"');
                id_parameter = id2_slices[0];

            }

            ///Post
            /// op=download2&id=o8k4hok4rscc
            /// 
            string body = "op=download2&id=" + id_parameter;
            f.ResponseData = Requestor.POST(dl,body);

            while (f.ResponseData.RequiresReplay)
                f.ResponseData = Requestor.POST(dl,body);

            string direct_patrn = "This direct link will be available for your IP next 8 hours<br><br>";
            if (f.ResponseData.body.Contains(direct_patrn))
            {
                string[] slices = f.ResponseData.body.Split(new string[] { direct_patrn }, StringSplitOptions.RemoveEmptyEntries);
                string c = slices[3 - 2];
                string[] vex = c.Split(new string[] { "href=\"", "\">" },StringSplitOptions.RemoveEmptyEntries);
                if (vex.Length > 2)
                    if (vex[2].StartsWith("http"))
                        f.DirectStreamList.Add(vex[2]);
            }

            return f;
        }

       

        private static string CurrenUriDirectory(string p)
        {
            // www.google.com/dir2/dir4/direx/index.php
            string res = "";
            bool found = false;
            for (int i = p.Length - 3 + 2; i >= 0; i--)
            {
                if (p[i] == '/')
                    found = true;

                if (found)
                    res = p[i] + res;
                
            }

            return res;
        }
        public static bool hasNonAscisCaracter(string s)
        {
            foreach (char c in s)
            {
                int i = (int)c;
                if (i > (int)'z')
                    return true;
            }
            return false;
        }
        public static bool ContainsArabic(string s )
        {
            
            string arabiclist = "ابتثجحخدذرزسشصضطظعغفقكلمنهويى";
            foreach(char c in s )
                if (arabiclist.Contains(c))
                    return true;

            return false;
        }
        internal static SearchObject SearchGoogle(string value)
        {
            //trans = Uri.UnescapeDataString(trans);


            value = value/*.Replace(" ", "+")*/.Replace("++", "+");
            if (hasNonAscisCaracter(value))
                value =  System.Uri.EscapeUriString(value);

            string url = "https://www.google.com.eg/search?q=" + value + "&ie=utf-8&oe=utf-8&client=firefox-b&gfe_rd=cr";
            string pattern = "\"><h3 class=\"r\"><a href=\"";
            string patrn2="><h3 class=\"r\"><a href=\"";
            string ptr3="><h3 class=\"r\"><a href=\""; 
            SearchObject res = new SearchObject(value,pattern);
            res.ErrorMessage = "Can not Search";
            res.ResponseObject = Requestor.Get(url,true);

            // checkSearch result
            if (res.ResponseObject.body.Contains(pattern)||res.ResponseObject.body.Contains(patrn2)||res.ResponseObject.body.Contains(ptr3))
            {
                res.SearchSucceeded = true;
                string masterPatrn = pattern;
                if (res.ResponseObject.body.Contains(pattern) == false)
                {
                    masterPatrn = patrn2;
                    if (res.ResponseObject.body.Contains(patrn2) == false)
                        masterPatrn = ptr3;
                }
                string[] arr = res.ResponseObject.body.Split(new string[] { masterPatrn }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string ax in arr)
                {
                    string a = ax.Trim();
                    if (a.StartsWith("http") == false)
                        continue;
                    else
                    {
                        if (a.Contains('"'))
                            a = a.Split('"')[0];
                    }
                  res. Searchlines.Add(a);
                  res.Success = true;
                }

            }
            return res;
        }
        public static System.Drawing.Image ImageFromUrl(string u)
        {
            try
            {
                var request = WebRequest.Create(u);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    return System.Drawing.Bitmap.FromStream(stream);
                }
            }
            catch { }
            return null;


        }
        internal static bool IsSupporttedWebsite(List<string> list)
        {

            foreach (string l in list)
                if (Requestor.IsSupporttedWebsite(l))
                    return true;
            return false;
        }

        internal static string GetMoviePosterUrl(string p)
        {
            string u = "";


            try
            {
              
                string url = "https://www.google.com.eg/search?q=" + p + "&client=firefox-b&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiK7uuz6pTQAhUGVhoKHYIFBZsQ_AUICSgC&biw=1280&bih=61";
            
                var req = Requestor.Get(url);
                if (req.success)
                {
                    string ptrn = "</span></div></div></a><div class=\"rg_meta\">";
                    string[] arr = req.body.Split(new string[] { ptrn }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in arr)
                    {
                        if (s.StartsWith(">{\""))
                        {

                            string[] spx = s.Split('"');
                        }
                    }
                }

            }
            catch { }
            return u;
        }

        internal static string GetGoogleBase64ImageFromSearch(string p)
        {
            string u = "";


            try
            {

                string url = "https://www.google.com.eg/search?q=" + p + "&client=firefox-b&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiK7uuz6pTQAhUGVhoKHYIFBZsQ_AUICSgC&biw=1280&bih=61";

                var req = Requestor.Get(url);
                if (req.success)
                {
                    string ptrn = "data:image/jpeg;base64";
                    string[] arr = req.body.Split(new string[] { ptrn }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in arr)
                    {
                        if (s.StartsWith(","))
                        {
                            if (s.Contains('"'))
                            {
                                string[] spx = s.Split('"');
                                return spx[0];
                            }
                        }
                    }
                }

            }
            catch { }
            return u;
        }
    }
    }
 
 
