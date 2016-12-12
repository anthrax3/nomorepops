using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoMorePops
{
    class SearchObject
    {
        public bool Success = false;
        public bool HasError = false;
        public string SearchWord = "";
        public List<string> Searchlines = new List<string>();
        private string errorMessage = "";

        public string ErrorMessage
        {
            get {
                if (errorMessage.Length > 0)
                    return errorMessage;
                else
                    return this.ResponseObject.ResponseErrorMessage;
            }
            set { errorMessage = value; }
        }
        
        public SearchObject()
        {
        }

        public SearchObject(string value)
        {
            this.SearchWord = value;
        }

        public SearchObject(string value, string pattern)
        { 
            this.SearchWord = value;
            this.Searchpattern = pattern;
        }

        public ResponseData ResponseObject  = new ResponseData();
        
        private string Searchpattern;

        public string SearchMethod ="google"; 

        public bool SearchSucceeded =false;

        internal string ErrorMessageFriendly()
        {
            string e = ErrorMessage;
            if (e.Contains("Unable to connect to the remote server") ||e.Contains("Can not Search")|| e.Contains("The operation has timed out") || e.Contains("unable to connect") || e.Contains("unable to connect") || e.Contains("The remote name could not be resolved"))
                return e + Environment.NewLine+"check your internet connection";
                 
             

            return e;
        }

        internal string SearchlinesTostring()
        {
            string g = "";
            foreach (string s in this.Searchlines)
                g = g + s + "\n";
            return g;
        }
    }
}
