using System.Text.Json;

namespace HundirLaFlota.src.Datos
{
    public static class GestorGuardado
    {
        private static readonly string RutaArchivo = "partida_guardada.json";
        
        private static readonly JsonSerializerOptions OpcionesJson = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public static void GuardarPartida(EstadoPartida estado)
        {
            try
            {
                string json = JsonSerializer.Serialize(estado, OpcionesJson);
                File.WriteAllText(RutaArchivo, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar: {ex.Message}");
            }
        }

        public static EstadoPartida? CargarPartida()
        {
            try
            {
                if (!ExistePartidaGuardada()) return null;
                string json = File.ReadAllText(RutaArchivo);
                return JsonSerializer.Deserialize<EstadoPartida>(json, OpcionesJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar: {ex.Message}");
                return null;
            }
        }

        public static void EliminarGuardado()
        {
            try
            {
                if (ExistePartidaGuardada()) File.Delete(RutaArchivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar: {ex.Message}");
            }
        }

        public static bool ExistePartidaGuardada() => File.Exists(RutaArchivo);
    }
}
