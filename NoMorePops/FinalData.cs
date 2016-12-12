using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoMorePops
{
    class FinalData
    {
        public ResponseData ResponseData  = new ResponseData("");
      
        public FinalData(string uri)
        { 
            ResponseData = new ResponseData(uri);
           
        }
        public List<string> List = new List<string>();
        public List<string> IframesList = new List<string>();
        public List<string> DownloadList = new List<string>();
        public List<string> WatchList = new List<string>();

        public List<string> DirectStreamList = new List<string>();

                
        public bool ListNeedProcess = true;
        public bool IFramesListNeedProcess = true;

        
        public bool Success = false;


        public string erroMessage =""; 
        public bool Error = false;

        internal bool HasData()
        {
            return DownloadList.Count > 0 || IframesList.Count > 0;
        }

        public bool ProceedDirectWatch =true; 

        public bool RemovedByAdmin=false;
         
    }
}
