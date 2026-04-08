
namespace HundirLaFlota.src.Datos
{
    public class EstadoPartida
    {
        public string NombreJugador { get; set; } = "";
        public int TotalDisparosJugador { get; set; }
        public int TotalAciertosJugador { get; set; }
        public int TotalFallosJugador { get; set; }
        public bool TurnoJugador { get; set; }
        public List<BarcoInfo> BarcosJugador { get; set; } = new();
        public List<BarcoInfo> BarcosCPU { get; set; } = new();
        public List<DisparoInfo> DisparosJugadorACpu { get; set; } = new();
        public List<DisparoInfo> DisparosCpuAJugador { get; set; } = new();
        public List<Posicion> TirosPendientesCPU { get; set; } = new();
        public int PosActualTirosCPU { get; set; }
        public DateTime FechaGuardado { get; set; } = DateTime.Now;
    }

    public class BarcoInfo
    {
        public string Nombre { get; set; } = "";
        public int Tamaño { get; set; }
        public List<Posicion> Posiciones { get; set; } = new();
        public bool Horizontal { get; set; }
        public int ImpactosRecibidos { get; set; }
        public bool EstaDestruido => ImpactosRecibidos >= Tamaño;
    }

    public class DisparoInfo
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool FueImpacto { get; set; }
        public bool BarcoHundido { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }

    public class Posicion
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
        
        public Posicion() { }
        
        public Posicion(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }
    }
}