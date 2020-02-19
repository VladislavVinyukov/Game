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

namespace exerWpf
{
    /// <summary>
    /// Interaction logic for HelpWin.xaml
    /// </summary>
    public partial class HelpWin : Window
    {
        MediaPlayer player;
        public HelpWin()
        {
            InitializeComponent();
            player = new MediaPlayer();
            Audio();
        }
        private void Audio()
        {
            player.Open(new Uri(@"C:\Users\BanCry\source\repos\exerWpf\exerWpf\audio\2.mp3", UriKind.Relative));
            player.Play();
        }
        private void Titul_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            this.Close();
        }
    }
}
