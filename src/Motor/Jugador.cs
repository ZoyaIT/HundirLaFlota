using HundirLaFlota.src.dominio;

namespace HundirLaFlota.src.Motor
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public Tablero MiTablero { get; private set; }
        public int TotalDisparos { get; private set; }
        public int TotalAciertos { get; private set; }
        public int TotalFallos { get; private set; }

        public double Precision
        {
            get { return TotalDisparos == 0 ? 0 : (double)TotalAciertos / TotalDisparos * 100; }
        }

        public Jugador(string nombre)
        {
            Nombre = nombre;
            MiTablero = new Tablero();
            TotalDisparos = 0;
            TotalAciertos = 0;
            TotalFallos = 0;
        }

        public void RegistrarDisparo(ResultadoDisparo resultado)
        {
            TotalDisparos++;

            if (resultado == ResultadoDisparo.Impacto || resultado == ResultadoDisparo.Hundido)
            {
                TotalAciertos++;
            }
            else if (resultado == ResultadoDisparo.Agua)
            {
                TotalFallos++;
            }
        }
    }
}



