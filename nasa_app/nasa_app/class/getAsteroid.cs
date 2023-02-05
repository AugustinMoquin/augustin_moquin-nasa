using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using nasa_app;
using System.IO;
using System.Net;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Collections;


//dans cette method on récupère les astéroides via leur date d'approche de la terre
namespace nasa_app
{
    internal class getAsteroid
    {
        public static async Task<Root> Get(string start)
        {
            string Url = "https://api.nasa.gov/neo/rest/v1/feed?start_date=" + start + "&end_date=" + start + "&api_key=Xz8qeWcjDFBG5aMguAUoKBxEpUB1FkMnCMkxqkz0";
            Root root= new Root();
            //try une requette pour l'api
            try
            {
                WebRequest resquest = HttpWebRequest.Create(Url);
                WebResponse response = resquest.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string root_json = reader.ReadToEnd();

                //converti en json et le deserialize dans l'objet Root
                root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(root_json);
                return root;
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("try something else");
            }

            return root;
        }

        public static async Task<OrbitalData> Get_indAsteroid(string Url)
        {
            //pour le moment ne sert pas mais peut etre dans le futur
            OrbitalData OrbitalData = new OrbitalData();
            try
            {
                WebRequest resquest = HttpWebRequest.Create(Url);
                WebResponse response = resquest.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string root_json = reader.ReadToEnd();

                OrbitalData = Newtonsoft.Json.JsonConvert.DeserializeObject<OrbitalData>(root_json);
                return OrbitalData;
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("try something else");
            }

            return OrbitalData;
        }

    }
}