using AppPeliculas.Modelos;
using Newtonsoft.Json;
using System.Text;
using System;

namespace AppPeliculas.Views;

public partial class AgregarPelicula : ContentPage
{
	public AgregarPelicula()
	{
		InitializeComponent();
	}

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        string url = "https://practprof2023-2855.restdb.io/rest/peliculas";

        //instanciamos un cliente HTTP
        HttpClient cliente = new HttpClient();
        //configuramos que trabajará con respuestas JSON
        cliente.DefaultRequestHeaders.Add("Accept", "application/json");
        cliente.DefaultRequestHeaders.Add("apikey", "6466d9870b60fc42f4e197bf");

        Pelicula nuevaPelicula = new Pelicula() {
            nombre=txtNombre.Text,
            genero=txtGenero.Text,
            duracion=Convert.ToInt32(txtDuracion.Text),
            trailer_url=txtTrailerUrl.Text,
            sinopsis=txtSinopsis.Text,
            portada_url=txtPortadaUrl.Text
        };

        //enviamos por POST el objeto que creamos a la URL de la API
        var respuesta = await cliente.PostAsync(url,
            new StringContent(
                JsonConvert.SerializeObject(nuevaPelicula),
                Encoding.UTF8, "application/json"));

        //retorna el objeto que se agregó en la API ya con su ID generado por la base de datos
        Pelicula pelicula= JsonConvert.DeserializeObject<Pelicula>(
            await respuesta.Content.ReadAsStringAsync());

        if(pelicula._id!="")
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