using AppPeliculas.Modelos;
using Newtonsoft.Json;
using System.Collections.ObjectModel;


namespace AppPeliculas.Views;

public partial class InicioApp : ContentPage
{
    public ObservableCollection<Pelicula> Peliculas { get; set; }
    public InicioApp()
	{
		InitializeComponent();
        Peliculas = new ObservableCollection<Pelicula>();
	}
	public async void GetAllPeliculas(object sender, EventArgs e)
	{
        HttpClient cliente = new HttpClient();
        //configuramos que trabajará con respuestas JSON
        cliente.DefaultRequestHeaders.Add("Accept", "application/json");
        cliente.DefaultRequestHeaders.Add("apikey", "6466d9870b60fc42f4e197bf");
        

        var respuesta = await cliente.GetStringAsync("https://practprof2023-2855.restdb.io/rest/peliculas");
        ObservableCollection<Pelicula> Peliculas = JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(respuesta);

        PeliculasCollectionView.ItemsSource = Peliculas;
    }
}