using HundirLaFlota.tests;

namespace HundirLaFlota
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Hundir la Flota - Pruebas";
            
            // Ejecutar todas las pruebas
            BarcoTests.EjecutarTodas();
            Console.WriteLine("\n" + new string('=', 50) + "\n");
            
            CpuTests.EjecutarTodas();
            Console.WriteLine("\n" + new string('=', 50) + "\n");
            
            TableroTests.EjecutarTodas();
            
            Console.WriteLine("\n\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}