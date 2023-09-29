using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPeliculas.Modelos
{
    public class Pelicula
    {
        public string _id { get; set; }
        public string nombre { get; set; }
        public string genero { get; set; }
        public int duracion { get; set; }
        public string trailer_url { get; set; }
        public string sinopsis { get; set; }
        public string portada_url { get; set; }

        public override string ToString()
        {
            return nombre+" - "+genero;
        }
    }
}
