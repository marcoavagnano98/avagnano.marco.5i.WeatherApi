using System;
using System.Collections.Generic;
using System.Linq;
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
using Newtonsoft.Json;

namespace avagnano.marco._5i.WeatherApi
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
        public  async void Start(string citta)
        {
            List<Riga> lista = new List<Riga>();
            Riga row = new Riga();
            try
            {
                HttpClient client = new HttpClient();
                string result = await client.GetStringAsync(
                    new Uri(@"http://api.wunderground.com/api/154c0fe4f4e124d7/conditions/q/IT/" + citta + ".json"));

                Meteo Meteo = JsonConvert.DeserializeObject<Meteo>(result);

                row.Citta =citta;
                row.Latitudine = Meteo.current_observation.display_location.latitude;
                row.Longitudine = Meteo.current_observation.display_location.longitude;
                row.Temperatura = Meteo.current_observation.temp_c.ToString() + "°C";
                row.Umidita = Meteo.current_observation.relative_humidity;
                row.VelocitaVento = Meteo.current_observation.wind_kph.ToString() + "Km/h";
                weather_icon.Source = new BitmapImage(new Uri(Meteo.current_observation.icon_url));
                lista.Add(row);
               
            }
            
            catch
            {
                MessageBox.Show("Inserire una localita' corretta ed italiana");
            }
            dgdati.ItemsSource = lista;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Start(txt1.Text);
        }
    }
}
