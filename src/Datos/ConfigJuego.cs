namespace HundirLaFlota.src.datos;

public class ConfigJuego
{
    public bool MostrarColores { get; set; } = true;
    public string NombreJugador { get; set; } = "Jugador";
    public string Dificultad { get; set; } = "Normal";
    public bool SonidoActivado { get; set; } = true;
    public int VelocidadAnimacion { get; set; } = 100; // milisegundos
    public bool AutoGuardado { get; set; } = true;
    public int AutoGuardadoCadaTurnos { get; set; } = 5;
    
    // Guardar configuración
    public void Guardar()
    {
        // Implementación para guardar configuración
    }
    
    // Cargar configuración
    public static ConfigJuego Cargar()
    {
        // Implementación para cargar configuración
        return new ConfigJuego();
    }
}

