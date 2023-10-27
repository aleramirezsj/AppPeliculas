using AppPeliculas.Modelos;
using AppPeliculas.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace AppPeliculas.Views;

public partial class EditarPelicula : ContentPage
{
    RepositoryPeliculas repositoryPeliculas=new RepositoryPeliculas();
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

        var guardada = await repositoryPeliculas.UpdateAsync(peliculaEditada);

        if (guardada)
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