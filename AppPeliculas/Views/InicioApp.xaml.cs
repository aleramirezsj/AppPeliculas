using AppPeliculas.Modelos;
using AppPeliculas.Repositories;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AppPeliculas.Views;

public partial class InicioApp : ContentPage, INotifyPropertyChanged
{
    public ObservableCollection<Pelicula> Peliculas { get; set; }
    private Pelicula peliculaSeleccionada;
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        /*Este código que comprueba si un valor es nulo se puede hacer
        * con una sola linea con la forma ?.Invoke
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }*/
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Pelicula PeliculaSeleccionada
    {
        get { return peliculaSeleccionada; }
        set
        {
            peliculaSeleccionada = value;
            OnPropertyChanged(nameof(PeliculaSeleccionada));
        }
    }



    RepositoryPeliculas repositoryPeliculas = new RepositoryPeliculas();
    public InicioApp()
    {
        InitializeComponent();
        Peliculas = new ObservableCollection<Pelicula>();
        PeliculasCollectionView.SelectionChanged += SeleccionarPelicula;
    }

    private void SeleccionarPelicula(object sender, SelectionChangedEventArgs e)
    {
        if (PeliculasCollectionView.SelectedItem != null)
        {
            PeliculaSeleccionada = (Pelicula)PeliculasCollectionView.SelectedItem;
        }
    }

    public async Task GetAllPeliculas(object sender, EventArgs e)
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
                    PeliculasCollectionView.ScrollTo(pelicula, null, ScrollToPosition.Center, true);
                    //PeliculasCollectionView.;
                    break;
                }
            }
        }

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        NetworkAccess conexionInternet = Connectivity.Current.NetworkAccess;
        if (conexionInternet == NetworkAccess.Internet)
        {
            await GetAllPeliculas(this, EventArgs.Empty);
            SeleccionarPeliculaEnCollectionView(this, EventArgs.Empty);
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
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar", $"¿Está seguro que desea borrar la película:{PeliculaSeleccionada.nombre}", "Si", "No");
            if (respuesta)
            {
                var eliminado = await repositoryPeliculas.RemoveAsync(PeliculaSeleccionada._id);
                if (eliminado)
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
        if (PeliculaSeleccionada != null)
        {
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

    private async void VerTrailerBtn_Clicked(object sender, EventArgs e)
    {
        if (PeliculaSeleccionada != null)
        {
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Ver trailer", "Error: debe seleccionar la película que quiere ver", "ok");
        }
    }
}