using HundirLaFlota.src.motor;

namespace HundirLaFlota
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Hundir la Flota";
            Juego juego = new Juego();
            juego.Iniciar();
        }
    }
}

