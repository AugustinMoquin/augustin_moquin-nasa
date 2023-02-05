using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace nasa_app
{

    internal class Apod
    {
        public string url = "https://api.nasa.gov/planetary/apod?api_key=Xz8qeWcjDFBG5aMguAUoKBxEpUB1FkMnCMkxqkz0";
        public string hdurl { get; set; }
        public string title { get; set; }
        public string explanation { get; set; }

        public Apod GetApod()
        {
            WebRequest resquest = HttpWebRequest.Create("https://api.nasa.gov/planetary/apod?api_key=Xz8qeWcjDFBG5aMguAUoKBxEpUB1FkMnCMkxqkz0");
            WebResponse response = resquest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string apod_json = reader.ReadToEnd();
            Apod apod = Newtonsoft.Json.JsonConvert.DeserializeObject<Apod>(apod_json);

            return apod;
        }
    }
}