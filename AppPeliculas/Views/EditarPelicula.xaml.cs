using AppPeliculas.Modelos;

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

    private void GuardarBtn_Clicked(object sender, EventArgs e)
    {

    }

    private void CancelarBtn_Clicked(object sender, EventArgs e)
    {

    }
}