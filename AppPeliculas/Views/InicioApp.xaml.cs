using AppPeliculas.Modelos;
using AppPeliculas.Repositories;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AppPeliculas.Views;

public partial class InicioApp : ContentPage
{
    public ObservableCollection<Pelicula> Peliculas { get; set; }
    Pelicula PeliculaSeleccionada { get; set; }

    RepositoryPeliculas repositoryPeliculas= new RepositoryPeliculas();
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

        Peliculas = await repositoryPeliculas.GetAllAsync();

        PeliculasCollectionView.ItemsSource = Peliculas;
            
        
        
    }

    public void SeleccionarPeliculaEnCollectionView(object sender, EventArgs e)
    {
        //iteramos las peliculas hasta encontrar la coincide con la Pelicula Seleccionada, al encontrarla la utilizaremos para indicar que es el SelectedItem del CollectionView e interrumpiremos la iteración.
        if (PeliculaSeleccionada != null)
        {
            foreach (var pelicula in Peliculas)
            {
                if (pelicula._id == PeliculaSeleccionada._id)
                {
                    PeliculasCollectionView.SelectedItem = pelicula;
                    PeliculasCollectionView.ScrollTo(pelicula,null,ScrollToPosition.Center,true);
                    //PeliculasCollectionView.;
                    break;
                }
            }
        }

    }

    protected  override void OnAppearing()
    {
        base.OnAppearing();
        NetworkAccess conexionInternet = Connectivity.Current.NetworkAccess;
        if (conexionInternet == NetworkAccess.Internet)
        {
            GetAllPeliculas(this, EventArgs.Empty);
            SeleccionarPeliculaEnCollectionView(this,EventArgs.Empty);
        }

    }
    protected override bool OnBackButtonPressed()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> se ha pulsado el botón de atrás");
        return false;
    }
    protected override void OnDisappearing()
    {
        Debug.Print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> se ha cerrado la ventana de la lista de películas"); 
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
            bool respuesta=await Application.Current.MainPage.DisplayAlert("Eliminar", $"¿Está seguro que desea borrar la película:{PeliculaSeleccionada.nombre}", "Si","No");
            if (respuesta)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("apikey", "6466d9870b60fc42f4e197bf");

                string urlEliminar = "https://practprof2023-2855.restdb.io/rest/peliculas/"+PeliculaSeleccionada._id;

                var response=await client.DeleteAsync(urlEliminar);
                if(response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Eliminar", $"Se ha eliminado la película {PeliculaSeleccionada.nombre} correctamente", "Ok");
                    GetAllPeliculas(this, EventArgs.Empty);
                }
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Eliminar", "Error: debe seleccionar la película que quiere borrar", "ok");
        }
    }

    private async void EditarPeliculaBtn_Clicked(object sender, EventArgs e)
    {
        if(PeliculaSeleccionada!= null) { 
            await Navigation.PushAsync(new EditarPelicula(PeliculaSeleccionada));
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Editar", "Error: debe seleccionar la película que quiere editar", "ok");
}
    }

    private void PeliculasBtn_Clicked(object sender, EventArgs e)
    {
        //PeliculasCollectionView.
        //SeleccionarPeliculaEnCollectionView(this,EventArgs.Empty);
    }
}