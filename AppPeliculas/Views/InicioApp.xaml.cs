using AppPeliculas.Modelos;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AppPeliculas.Views;

public partial class InicioApp : ContentPage
{
    public ObservableCollection<Pelicula> Peliculas { get; set; }
    Pelicula PeliculaSeleccionada { get; set; }
    public InicioApp()
	{
		InitializeComponent();
        Peliculas = new ObservableCollection<Pelicula>();
        PeliculasCollectionView.SelectionChanged += SeleccionarPelicula;
	}

    private void SeleccionarPelicula(object sender, SelectionChangedEventArgs e)
    {
        if (PeliculasCollectionView.SelectedItem!=null)
        {
            PeliculaSeleccionada = (Pelicula)PeliculasCollectionView.SelectedItem;
        }
    }

    public async void GetAllPeliculas(object sender, EventArgs e)
	{
        HttpClient cliente = new HttpClient();
        //configuramos que trabajar� con respuestas JSON
        cliente.DefaultRequestHeaders.Add("Accept", "application/json");
        cliente.DefaultRequestHeaders.Add("apikey", "6466d9870b60fc42f4e197bf");
        

        var respuesta = await cliente.GetStringAsync("https://practprof2023-2855.restdb.io/rest/peliculas");
        ObservableCollection<Pelicula> Peliculas = JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(respuesta);

        PeliculasCollectionView.ItemsSource = Peliculas;
    }
    protected override void OnAppearing()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Se ha cargado la pantalla que muestra la lista de pel�culas");
        GetAllPeliculas(this,EventArgs.Empty);
    }
    protected override bool OnBackButtonPressed()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> se ha pulsado el bot�n de atr�s");
        return false;
    }
    protected override void OnDisappearing()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> se ha cerrado la ventana de la lista de pel�culas"); 
    }
    private async void AbrirPaginaInicio(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PaginaInicio());
    }

    private async void AbrirPaginaControlesMAUI(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ControlesMaui());
    }

    private async void AgregarPeliculaBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarPelicula());
    }

    private async void EliminarPeliculaBtn_Clicked(object sender, EventArgs e)
    {
        if (PeliculaSeleccionada != null)
        {
            bool respuesta=await Application.Current.MainPage.DisplayAlert("Eliminar", $"�Est� seguro que desea borrar la pel�cula:{PeliculaSeleccionada.nombre}", "Si","No");
            if (respuesta)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("apikey", "6466d9870b60fc42f4e197bf");

                string urlEliminar = "https://practprof2023-2855.restdb.io/rest/peliculas/"+PeliculaSeleccionada._id;

                var response=await client.DeleteAsync(urlEliminar);
                if(response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Eliminar", $"Se ha eliminado la pel�cula {PeliculaSeleccionada.nombre} correctamente", "Ok");
                    GetAllPeliculas(this, EventArgs.Empty);
                }
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Eliminar", "Error: debe seleccionar la pel�cula que quiere borrar", "ok");
        }
    }

    private async void EditarPeliculaBtn_Clicked(object sender, EventArgs e)
    {
        if(PeliculaSeleccionada!= null) { 
            await Navigation.PushAsync(new EditarPelicula(PeliculaSeleccionada));
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Editar", "Error: debe seleccionar la pel�cula que quiere editar", "ok");
}
    }
}