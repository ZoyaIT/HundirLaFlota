using System.Collections.Generic;

namespace HundirLaFlota.src.dominio
{
    public static class Flota
    {
        public static List<Barco> ObtenerBarcos()
        {
            List<Barco> lista = new List<Barco>();
            lista.Add(new Barco("Portaaviones", 5));
            lista.Add(new Barco("Acorazado", 4));
            lista.Add(new Barco("Destructor", 3));
            lista.Add(new Barco("Submarino", 3));
            lista.Add(new Barco("Patrullera", 2));
            return lista;
        }
    }
}


