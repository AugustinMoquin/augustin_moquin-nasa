﻿using System;
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

            string request = year + "-" + month + "-" + day;
            Root root = getAsteroid.Get(request).Result;

            int arrLength = root.near_earth_objects[request].Length;
            get_Asteroide(root, arrLength, request);

            try
            {
                Console.WriteLine(root.near_earth_objects[request][0].name);

            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                MessageBox.Show("nothing seems to have happened this month");
            }
        }

        private void get_Asteroide(Root root, int arrLength, string request)
        {
            List<Ind_asteroid> Name_List = new List<Ind_asteroid>();
            for (int j = 0; j < arrLength; j++)
            {
                Ind_asteroid name_List = new Ind_asteroid();
                name_List.name = root.near_earth_objects[request][j].name;
                name_List.self = root.near_earth_objects[request][j].links.self;
                Name_List.Add(name_List);
            }
            this.lbxAsteroid.ItemsSource = Name_List;
        }

        private void Click_toIndAsteroid(object sender, RoutedEventArgs e)
        {
            /*asteroid windowTwo = new asteroid();
            windowTwo.Show();
            this.Close();*/

            string url = (string)button_IndAsteroid.Tag;
            OrbitalData orbitalData = getAsteroid.Get_indAsteroid(url).Result;
            MessageBox.Show(orbitalData.equinox);
            
        }

        //asteroides du jour precedent
        private void Click_toPrevious(object sender, RoutedEventArgs e)
        {
            asteroid windowTwo = new asteroid();
            windowTwo.Show();
            this.Close();

            int tampon = 0;

            string request = year + "-" + month + "-" + day;
            Root root = getAsteroid.Get(request).Result;
            string next = root.links.next;
            foreach (char c in next)
            {
                tampon++;
                if (tampon >= 49 && tampon <= 58)
                {
                    prevDate += c;
                }
            }

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
            prevDate = "";
        }

        private void Click_toNext(object sender, RoutedEventArgs e)
        {
            asteroid windowTwo = new asteroid();
            windowTwo.Show();
            this.Close();

            int tampon = 0;

            string request = year + "-" + month + "-" + day;
            Root root = getAsteroid.Get(request).Result;
            string next = root.links.previous;
            foreach (char c in next)
            {
                tampon++;
                if (tampon >= 49 && tampon <= 58)
                {
                    nextDate += c;
                }
            }

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

        private void Click_todayDate(object sender, RoutedEventArgs e)
        {
            string request = DateTime.Now.ToString("yyyy-dd-MM");
            Root root = getAsteroid.Get(request).Result;
            int arrLength = root.near_earth_objects[request].Length;
            get_Asteroide(root, arrLength, request);
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
