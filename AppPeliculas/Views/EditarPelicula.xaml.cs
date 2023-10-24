using AppPeliculas.Modelos;
using Newtonsoft.Json;
using System.Text;

namespace AppPeliculas.Views;

public partial class EditarPelicula : ContentPage
{
    public Pelicula PeliculaSeleccionada { get; }

    public EditarPelicula()
	{
		InitializeComponent();
	}

    public EditarPelicula(Pelicula peliculaSeleccionada)
    {
        InitializeComponent();  
        PeliculaSeleccionada = peliculaSeleccionada;
        CargarDatosEnPantalla();
    }

    private void CargarDatosEnPantalla()
    {
        txtNombre.Text=PeliculaSeleccionada.nombre;
        txtGenero.Text = PeliculaSeleccionada.genero;
        txtDuracion.Text = PeliculaSeleccionada.duracion.ToString();
        txtPortadaUrl.Text = PeliculaSeleccionada.portada_url;
        txtSinopsis.Text = PeliculaSeleccionada.sinopsis;
        txtTrailerUrl.Text = PeliculaSeleccionada.trailer_url;
    }

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        string url = $"https://practprof2023-2855.restdb.io/rest/peliculas/{PeliculaSeleccionada._id}";

        //instanciamos un cliente HTTP
        HttpClient cliente = new HttpClient();
        //configuramos que trabajará con respuestas JSON
        cliente.DefaultRequestHeaders.Add("Accept", "application/json");
        cliente.DefaultRequestHeaders.Add("apikey", "6466d9870b60fc42f4e197bf");

        Pelicula peliculaEditada = new Pelicula()
        {
            _id=PeliculaSeleccionada._id,
            nombre = txtNombre.Text,
            genero = txtGenero.Text,
            duracion = Convert.ToInt32(txtDuracion.Text),
            trailer_url = txtTrailerUrl.Text,
            sinopsis = txtSinopsis.Text,
            portada_url = txtPortadaUrl.Text
        };

        //enviamos por PUT el objeto que creamos a la URL de la API
        var respuesta = await cliente.PutAsync(url,
            new StringContent(
                JsonConvert.SerializeObject(peliculaEditada),
                Encoding.UTF8, "application/json"));

        //retorna el objeto que se agregó en la API ya con su ID generado por la base de datos
        Pelicula pelicula = JsonConvert.DeserializeObject<Pelicula>(
            await respuesta.Content.ReadAsStringAsync());

        if (pelicula._id != "")
        {
            await DisplayAlert("Notificación", "Pelicula guardada", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}