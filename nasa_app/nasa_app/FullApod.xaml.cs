using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace nasa_app
{
    /// <summary>
    /// Logique d'interaction pour FullApod.xaml
    /// </summary>
    public partial class FullApod : Window
    {
        public FullApod()
        {
            InitializeComponent();

            Apod Newapod = new Apod();
            Apod apod = Newapod.GetApod();

            ///on envoie ici l'image du jour pour changer le fond d'ecran
            string URLimage = String.Format(apod.hdurl);
            image.Source = new BitmapImage(new Uri(URLimage));
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();

            }
        }
        ///crée un bouton qui redirige vers la page principal
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainW = new MainWindow();
            //this will open your child window
            mainW.Show();
            //this will close parent window. mainW in this case
            this.Close();
        }
    }
}
