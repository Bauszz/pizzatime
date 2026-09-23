using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pizzatime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> kinálat = new List<string>() { "Margherita", "Sonkás", "Gombás", "Hawaii", "Négy sajtos", "Magyaros" };
        List<string> rendelés = new List<string>() { };

        List<string> keres = new List<string>() { };

        public MainWindow()
        {
            InitializeComponent();
            Lb_pizza.ItemsSource = kinálat;
        }

        private void Adás(object sender, RoutedEventArgs e)
        {

            if (Tb_adás.Text != "")
            {
                if (!kinálat.Contains(Tb_adás.Text))
                {
                    kinálat.Add(Tb_adás.Text);
                    Tb_adás.Text = "";
                    Lb_pizza.ItemsSource = null;
                    Lb_pizza.ItemsSource = kinálat;
                }
                else
                {
                    MessageBox.Show("Duplikáns elem nem fogadható el!", "Figyelem", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
            else
            {
                MessageBox.Show("Ne hagyja üresen a nevet!","Figyelem",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }

        private void törlés(object sender, RoutedEventArgs e)
        {
            string választott = "" + Lb_pizza.SelectedItem;

            if (választott != null)
            {
                if (MessageBox.Show($"Biztosan kívánja törölni a {választott} elemet?", "Figyelem", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    kinálat.Remove(választott);
                    Lb_pizza.ItemsSource = null;
                    Lb_pizza.ItemsSource = kinálat;
                }
            }
        }

        private void Kivalasztas(object sender, SelectionChangedEventArgs e)
        {
            string választott = "" + Lb_pizza.SelectedItem;
            Tblk_Kiválasztott.Text = választott;
        }

        private void Rendelés(object sender, RoutedEventArgs e)
        {
            if ((Tb_méret.Text.ToLower().Trim() == "kicsi"|| Tb_méret.Text.ToLower().Trim() == "közepes" || Tb_méret.Text.ToLower().Trim() == "nagy") && Tblk_Kiválasztott.Text != null)
            {
                rendelés.Add(Tblk_Kiválasztott.Text + " " + Tb_méret.Text.ToLower().Trim());
                Lb_Checkout.ItemsSource = null;
                Lb_Checkout.ItemsSource = rendelés;
                Tblk_Rendszám.Text = "Rendelések száma: " + rendelés.Count.ToString();
            }
            else
            {
                Tb_méret.Text = "";
                MessageBox.Show("Kicsi, közepes és nagyot fogadunk el csak.", "Figyelem", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SelDelCheck(object sender, RoutedEventArgs e)
        {
            string választottrend = "" + Lb_Checkout.SelectedItem;
            if (MessageBox.Show($"Biztosan kívánja törölni a {választottrend} elemet?", "Figyelem", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                
                rendelés.Remove(választottrend);
                Lb_Checkout.ItemsSource = null;
                Lb_Checkout.ItemsSource = rendelés;
                Tblk_Rendszám.Text = "Rendelések száma: " + rendelés.Count.ToString();
            }
;
        }

        private void AllDelCheck(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Biztosan kívánja törölni az összes rendelést?", "Figyelem", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                rendelés = new List<string>();
                Lb_Checkout.ItemsSource = null;
                Lb_Checkout.ItemsSource = rendelés;
                Tblk_Rendszám.Text = "Rendelések száma: " + rendelés.Count.ToString();

            }
        }

        private void Keres(object sender, RoutedEventArgs e)
        {
            if (rendelés.Contains(Tb_keres.Text.ToLower().Trim()))
            {
                MessageBox.Show($"A(z) {Tb_keres.Text.ToLower().Trim()} pizza {Keresőcucc()} szor van a rendelésben", "Figyelem", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private int Keresőcucc()
            {
            keres = new List<string>();
            string keresendő = Tb_keres.Text.ToLower().Trim();

            foreach (var item in rendelés)
            {
                if (item.ToLower().Trim() == keresendő)
                {
                    keres.Add(item);
                }
            }

            return keres.Count;
        }
    }
}