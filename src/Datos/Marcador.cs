using System.Text.Json;

namespace HundirLaFlota.src.Datos
{
    public class Marcador
    {
        private const int MAX_ENTRADAS = 10;
        private static readonly string RutaArchivo = "marcador.json";
        private List<EntradaMarcador> entradas = new List<EntradaMarcador>();  // Inicialización directa

        public Marcador()
        {
            Cargar();
        }

        private void Cargar()
        {
            try
            {
                if (File.Exists(RutaArchivo))
                {
                    string json = File.ReadAllText(RutaArchivo);
                    var temp = JsonSerializer.Deserialize<List<EntradaMarcador>>(json);
                    if (temp != null)
                        entradas = temp;
                }
            }
            catch { }
        }

        private void Guardar()
        {
            try
            {
                string json = JsonSerializer.Serialize(entradas, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(RutaArchivo, json);
            }
            catch { }
        }

        public void AgregarEntrada(string nombre, int disparos, double precision)
        {
            double puntuacion = precision * (100.0 / Math.Max(disparos, 1));
            
            entradas.Add(new EntradaMarcador
            {
                Nombre = nombre,
                Disparos = disparos,
                Precision = precision,
                Puntuacion = puntuacion,
                Fecha = DateTime.Now
            });
            
            entradas = entradas.OrderByDescending(e => e.Puntuacion).Take(MAX_ENTRADAS).ToList();
            Guardar();
        }

        public List<EntradaMarcador> ObtenerTop10()
        {
            return entradas.OrderByDescending(e => e.Puntuacion).ToList();
        }
    }
}