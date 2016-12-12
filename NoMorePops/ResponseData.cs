using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoMorePops
{
    public class ResponseData
    {
        public string FirstUrl = "";
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public string body = "";
        public string ResponseErrorMessage = "";
        public bool HasError = false;
        public int errorcode = 0;
        public ResponseData()
        {
            this.ref_ = System.DateTime.Now.ToString();
        }

        public ResponseData(string p):this()
        {
            this.FirstUrl = p;
        }
        public void SetResponseError(string p)
        {

            //The remote server returned an error: (402) Payment Required.
            this.ResponseErrorMessage = p;
            if (p.Contains("error: ("))
            {

                string tmp = p.Split(new string[] { "error: (", ")" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                try { this.errorcode = int.Parse(tmp); }
                catch { this.errorcode = 0; }


            }

        }


        public bool success = false;
        public string ErrorUrl = "";
        public string redirectedTo = "";

        public string ResponseHeaders = "";


        internal string GetResponseMessage()
        {
            return  ResponseHeaders+"\n\n"+body;
            
        }

        public string ref_ = "";

        internal bool IsTimeOutError()
        {
            return this.ResponseErrorMessage.ToLower().Contains("timeout") || this.ResponseErrorMessage.Contains("time out");
        }

        public bool RequiresReplay = false;
    }
}