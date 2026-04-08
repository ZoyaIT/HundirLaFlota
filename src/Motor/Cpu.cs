using System;
using System.Collections.Generic;

using HundirLaFlota.src.dominio;

namespace HundirLaFlota.src.Motor
{
    public class Cpu : Jugador
    {
        private Random rnd;
        private List<(int fila, int columna)> listaTiros;
        private int posActual;

        public Cpu() : base("CPU")
        {
            rnd = new Random();
            listaTiros = new List<(int, int)>();
            CargarTiros();
        }

        private void CargarTiros()
        {
            for (int f = 0; f < 10; f++)
            {
                for (int c = 0; c < 10; c++)
                {
                    listaTiros.Add((f, c));
                }
            }

            for (int i = listaTiros.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = listaTiros[i];
                listaTiros[i] = listaTiros[j];
                listaTiros[j] = temp;
            }

            posActual = 0;
        }

        public void ColocarBarcosAlAzar(List<Barco> flota)
        {
            foreach (var barco in flota)
            {
                bool colocado = false;

                while (!colocado)
                {
                    int fila = rnd.Next(10);
                    int columna = rnd.Next(10);
                    bool horizontal = rnd.Next(2) == 0;

                    if (MiTablero.SePuedeColocarAqui(barco, fila, columna, horizontal))
                    {
                        MiTablero.PonerBarco(barco, fila, columna, horizontal);
                        colocado = true;
                    }
                }
            }
        }

        public (int fila, int columna) EscogerObjetivo()
        {
            if (posActual >= listaTiros.Count)
                posActual = 0;

            var tiro = listaTiros[posActual];
            posActual++;

            return tiro;
        }
    }
}


