using nasa_app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Threading;
using System.Threading;

namespace nasa_app
{
    /// <summary>
    /// a installer sur nuget ==> Newton.json
    /// </summary>
    public partial class MainWindow : Window
    {
        bool stateClose = true;

        public MainWindow()
        {
            InitializeComponent();

            //crée une objet apod et stock le resultat
            Apod Newapod = new Apod();
            Apod apod = Newapod.GetApod();

            ///on envoie ici l'image du jour pour changer le fond d'ecran
            string URLimage = String.Format(apod.hdurl);
            image.Source = new BitmapImage(new Uri(URLimage));

            ///on crée un nouveau binding entre objet TexteBox de l'xaml et l'explication de l'apod
            ///depuis la source apod on recupère l'élément explanation
            var BindingExpl = new Binding("explanation")
            {
                Source = apod
            };
            var BindingTitle = new Binding("title")
            {
                Source = apod
            };

            // Bind the data source to the TextBox control's Text dependency property
            ApodExplanation.SetBinding(TextBlock.TextProperty, BindingExpl);
            ApodTitle.SetBinding(TextBlock.TextProperty, BindingTitle);
        }

        //une fonction pour agrandir et retécir le menu, peut etre amélioré
        private void menu_click(object sender, RoutedEventArgs e)
        {
            if (stateClose)
            {
                sidePanel.Width = 150;
                stateClose = false;
            }
            else
            {
                sidePanel.Width = 45;
                stateClose = true;
            }
        }

        ///rend la fenetre dragable 
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton== MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        ///crée un bouton custom de sortie du programme
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FullApod windowTwo = new FullApod();
            //this will open your child window
            windowTwo.Show();
            //this will close parent window. windowOne in this case
            this.Close();
        }

        private void click_toAsteroid(object sender, RoutedEventArgs e)
        {
            asteroid windowTwo = new asteroid();
            //this will open your child window
            windowTwo.Show();
            //this will close parent window. windowOne in this case
            this.Close();
        }

        //lien qui emene vers le site officiel de la nasa
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }
    }
}