using AppPeliculas.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPeliculas.Repositories
{
    public class RepositoryPeliculas
    {
        string urlApi = "https://practprof2023-2855.restdb.io/rest/peliculas";
        HttpClient client = new HttpClient();

        public RepositoryPeliculas()
        {
            //configuramos que trabajará con respuestas JSON
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("apikey", "6466d9870b60fc42f4e197bf");
        }

        public async Task<ObservableCollection<Pelicula>> GetAllAsync()
        {
            try
            {
                var response = await client.GetStringAsync(urlApi);
                return JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(response);
            }
            catch (Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error:" + error.Message, "Ok");
                return null;
            }
        }
    }
}
