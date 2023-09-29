namespace AppPeliculas;

public partial class PaginaInicio : ContentPage
{
	public PaginaInicio()
	{
		InitializeComponent();
	}
	public async void MostrarPeliculas(object sender, EventArgs e)
	{
        //await DisplayAlert("Peliculas", "Mostrando la lista de peliculas", "OK");
        ContenidoLbl.Text = "Peliculas";
    }
    public void MostrarSeries(object sender, EventArgs e)
    {
        ContenidoLbl.Text = "Series";
    }
    public void MostrarDocumentales(object sender, EventArgs e)
    {
        ContenidoLbl.Text = "Documentales";
    }
}