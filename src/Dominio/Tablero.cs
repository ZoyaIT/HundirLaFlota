using System.Collections.Generic;

namespace HundirLaFlota.src.dominio
{
    public class Tablero
    {
        private Casilla[,] casillas;
        private List<Barco> barcos;
        private const int TAM = 10;

        public Tablero()
        {
            casillas = new Casilla[TAM, TAM];
            barcos = new List<Barco>();

            for (int f = 0; f < TAM; f++)
            {
                for (int c = 0; c < TAM; c++)
                {
                    casillas[f, c] = new Casilla(f, c);
                }
            }
        }

        public Casilla DameCasilla(int fila, int columna)
        {
            return casillas[fila, columna];
        }

        public bool TodosLosBarcosHundidos
        {
            get
            {
                foreach (var b in barcos)
                {
                    if (!b.EstaDestruido)
                        return false;
                }
                return true;
            }
        }

        public int CuantosBarcosQuedan
        {
            get
            {
                int cont = 0;
                foreach (var b in barcos)
                {
                    if (!b.EstaDestruido)
                        cont++;
                }
                return cont;
            }
        }

        public bool SePuedeColocarAqui(Barco barco, int fila, int columna, bool horizontal)
        {
            if (horizontal)
            {
                if (columna + barco.Tamaño > TAM)
                    return false;
            }
            else
            {
                if (fila + barco.Tamaño > TAM)
                    return false;
            }

            for (int i = 0; i < barco.Tamaño; i++)
            {
                int f = horizontal ? fila : fila + i;
                int c = horizontal ? columna + i : columna;

                for (int df = -1; df <= 1; df++)
                {
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        int nf = f + df;
                        int nc = c + dc;

                        if (nf >= 0 && nf < TAM && nc >= 0 && nc < TAM)
                        {
                            if (casillas[nf, nc].BarcoQueOcupa != null)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        public void PonerBarco(Barco barco, int fila, int columna, bool horizontal)
        {
            List<Casilla> lista = new List<Casilla>();

            for (int i = 0; i < barco.Tamaño; i++)
            {
                int f = horizontal ? fila : fila + i;
                int c = horizontal ? columna + i : columna;

                Casilla cas = casillas[f, c];
                cas.BarcoQueOcupa = barco;
                lista.Add(cas);
            }

            barco.CasillasQueOcupa.AddRange(lista);
            barcos.Add(barco);
        }

        public ResultadoDisparo LanzarProyectil(int fila, int columna)
        {
            Casilla cas = casillas[fila, columna];

            if (cas.FueDisparada)
                return ResultadoDisparo.YaDisparado;

            cas.FueDisparada = true;

            if (cas.EstaLibre)
                return ResultadoDisparo.Agua;

            Barco b = cas.BarcoQueOcupa!;
            b.RecibirUnImpacto();

            if (b.EstaDestruido)
                return ResultadoDisparo.Hundido;

            return ResultadoDisparo.Impacto;
        }
    }
}

