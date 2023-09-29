using AppPeliculas.Views;

namespace AppPeliculas;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new InicioApp();
	}
}
