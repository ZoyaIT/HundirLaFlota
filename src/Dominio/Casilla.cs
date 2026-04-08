namespace HundirLaFlota.src.dominio
{
    public class Casilla
    {
        public int Fila;
        public int Columna;
        public Barco? BarcoQueOcupa;
        public bool FueDisparada;

        public Casilla(int f, int c)
        {
            Fila = f;
            Columna = c;
            BarcoQueOcupa = null;
            FueDisparada = false;
        }

        public bool EstaLibre => BarcoQueOcupa == null;
        public bool TieneImpacto => FueDisparada && BarcoQueOcupa != null;
        public bool EsAgua => FueDisparada && BarcoQueOcupa == null;
    }
}



