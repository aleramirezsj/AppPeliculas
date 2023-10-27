using AppPeliculas.Modelos;
using Newtonsoft.Json;
using System.Text;
using System;
using AppPeliculas.Repositories;

namespace AppPeliculas.Views;

public partial class AgregarPelicula : ContentPage
{
    RepositoryPeliculas repositoryPeliculas= new RepositoryPeliculas();
	public AgregarPelicula()
	{
		InitializeComponent();
	}

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        Pelicula nuevaPelicula = new Pelicula() {
            nombre=txtNombre.Text,
            genero=txtGenero.Text,
            duracion=Convert.ToInt32(txtDuracion.Text),
            trailer_url=txtTrailerUrl.Text,
            sinopsis=txtSinopsis.Text,
            portada_url=txtPortadaUrl.Text
        };

        var agregada = await repositoryPeliculas.AddAsync(nuevaPelicula);

        if(agregada)
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