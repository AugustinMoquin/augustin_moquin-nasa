using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace nasa_app
{
    /// <summary>
    /// Logique d'interaction pour asteroid.xaml
    /// </summary>
    public partial class asteroid : Window
    {
        //declaration des variables
        private string year;
        private string month;
        private string day;
        string nextDate;
        string prevDate;
        string currentDate;

        //class d'asteroide pour les infos individuel
        public class Ind_asteroid
        {
            public string name { get; set; }
            public string self { get; set; }
            public override string ToString()
            {
                return name;
            }
        }

        public asteroid()
        {
            InitializeComponent();

            Apod Newapod = new Apod();
            Apod apod = Newapod.GetApod();

            ///on envoie ici l'image du jour pour changer le fond d'ecran
            string URLimage = String.Format(apod.hdurl);
            image.Source = new BitmapImage(new Uri(URLimage));
        }

        private void Click_sendDate(object sender, RoutedEventArgs e)
        {
            //verifier l'input
            //on récupère l'input de l'utilisateur
            year = inputYear.Text;
            month = inputMonth.Text;
            day = inputDay.Text;

            //recupère la date choisi par l'utilisateur
            string request = year + "-" + month + "-" + day;
            currentDate = request;
            Root root = getAsteroid.Get(request).Result;

            int arrLength = root.near_earth_objects[request].Length;
            get_Asteroide(root, arrLength, request);

            //on essaye d'executer ce bout de code pour passer outre une erreur
            try
            {
                Console.WriteLine(root.near_earth_objects[request][0].name);

            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                MessageBox.Show("nothing seems to have happened this month");
            }
        }

        //récupère les infos d l'asteroides et les stocks dans une liste d'objet Ind_asteroid
        private void get_Asteroide(Root root, int arrLength, string request)
        {
            List<Ind_asteroid> Name_List = new List<Ind_asteroid>();
            for (int j = 0; j < arrLength; j++)
            {
                Ind_asteroid name_List = new Ind_asteroid();
                name_List.name = root.near_earth_objects[request][j].name;
                name_List.self = j.ToString();
                Name_List.Add(name_List);
            }
            this.lbxAsteroid.ItemsSource = Name_List;
        }

        //permet d'acceder aux infos de l'asteroide dns une pop up
        private void Click_toIndAsteroid(object sender, RoutedEventArgs e)
        {
            string index = (string)button_IndAsteroid.Tag;
            Root root = getAsteroid.Get(currentDate).Result;
            MessageBox.Show("Close approach date: " + root.near_earth_objects[currentDate][int.Parse(index)].close_approach_data[0].close_approach_date_full + "\n" + "Vitesse : " + root.near_earth_objects[currentDate][int.Parse(index)].close_approach_data[0].relative_velocity.kilometers_per_hour + " km/h" + "\n" + "Miss distance : " + root.near_earth_objects[currentDate][int.Parse(index)].close_approach_data[0].miss_distance.kilometers + " km");

        }

        //asteroides du jour precedent
        private void Click_toPrevious(object sender, RoutedEventArgs e)
        {
            //recupère la date actuelle et récupère la lien vers le jour précédent
            int tampon = 0;
            Root root = getAsteroid.Get(currentDate).Result;
            string next = root.links.next;
            //un foreach pas tres beau mais vachement utile quand on veut récuperer la date dans une longue string
            foreach (char c in next)
            {
                tampon++;
                if (tampon >= 49 && tampon <= 58)
                {
                    prevDate += c;
                }
            }

            //envoie une requette de récupration des asteroides sur la date voulue
            Root NewRoot = getAsteroid.Get(prevDate).Result;
            int arrLength = NewRoot.near_earth_objects[prevDate].Length;
            get_Asteroide(NewRoot, arrLength, prevDate);

            try
            {
                Console.WriteLine(NewRoot.near_earth_objects[prevDate][0].name);

            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                MessageBox.Show("nothing seems to have happened this month");
            }
            //reset les dates pour rester dans la continuité
            currentDate= prevDate;
            prevDate = "";
        }

        private void Click_toNext(object sender, RoutedEventArgs e)
        {
            //recupère la date actuelle et récupère la lien vers le jour suivant
            int tampon = 0;
            Root root = getAsteroid.Get(currentDate).Result;
            string next = root.links.previous;
            //un foreach pas tres beau mais vachement utile quand on veut récuperer la date dans une longue string
            foreach (char c in next)
            {
                tampon++;
                if (tampon >= 49 && tampon <= 58)
                {
                    nextDate += c;
                }
            }
            //envoie une requette de récupration des asteroides sur la date voulue
            Root NewRoot = getAsteroid.Get(nextDate).Result;
            int arrLength = NewRoot.near_earth_objects[nextDate].Length;
            get_Asteroide(NewRoot, arrLength, nextDate);

            try
            {
                Console.WriteLine(NewRoot.near_earth_objects[nextDate][0].name);

            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                MessageBox.Show("nothing seems to have happened this month");
            }
            //reset les variables
            currentDate = nextDate;
            nextDate = "";
        }

        //renvoie au menu principal
        private void click_toMenu(object sender, RoutedEventArgs e)
        {
            MainWindow windowTwo = new MainWindow();
            //this will open your child window
            windowTwo.Show();
            //this will close parent window. windowOne in this case
            this.Close();
        }

        ///rend la fenetre dragable 
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        ///crée un bouton custom de sortie du programme
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //lien qui emene vers le site officiel de la nasa
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        private void click_toAsteroid(object sender, RoutedEventArgs e)
        {
            asteroid windowTwo = new asteroid();
            //this will open your child window
            windowTwo.Show();
            //this will close parent window. windowOne in this case
            this.Close();
        }

        //fonction qui prend la date d'aujourd'hui et recupère les asteroides
        private void Click_todayDate(object sender, RoutedEventArgs e)
        {
            string request = DateTime.Now.ToString("yyyy-dd-MM");
            Root root = getAsteroid.Get(request).Result;
            int arrLength = root.near_earth_objects[request].Length;
            get_Asteroide(root, arrLength, request);
            currentDate = request;
            try
            {
                Console.WriteLine(root.near_earth_objects[request][0].name);

            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                MessageBox.Show("nothing seems to have happened this month");
            }
        }
    }
}
