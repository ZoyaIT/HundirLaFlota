using HundirLaFlota.src.Datos;
using HundirLaFlota.src.dominio;
using HundirLaFlota.src.presentacion;

namespace HundirLaFlota.src.Motor
{
    public class Juego
    {
        private Jugador jugador = null!;
        private Cpu cpu = null!;
        private Renderizador vista = new Renderizador();
        private Marcador marcador = new Marcador();

        public void Iniciar()
        {
            while (true)
            {
                vista.MostrarMenuPrincipal();
                string? op = Console.ReadLine();
                if (op == "1") NuevaPartida();
                else if (op == "2") break;
                else vista.MostrarError("Opción incorrecta");
            }
        }

        private void NuevaPartida()
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "Jugador";
            if (nombre == "") nombre = "Jugador";

            jugador = new Jugador(nombre);
            cpu = new Cpu();

            // Colocar barcos del jugador
            foreach (var barco in Flota.ObtenerBarcos())
            {
                bool ok = false;
                while (!ok)
                {
                    vista.MostrarTableroColocacion(jugador.MiTablero, barco);
                    Console.WriteLine($"Colocando {barco.NombreDelBarco} (tamaño {barco.Tamaño})");
                    
                    Console.Write("Fila (A-J): ");
                    string? f = Console.ReadLine();
                    if (string.IsNullOrEmpty(f)) { vista.MostrarError("Fila inválida"); continue; }
                    int fila = f[0] - 'A';
                    
                    Console.Write("Columna (1-10): ");
                    if (!int.TryParse(Console.ReadLine(), out int c)) { vista.MostrarError("Columna inválida"); continue; }
                    int col = c - 1;
                    
                    Console.Write("Horizontal (S/N): ");
                    bool horiz = Console.ReadLine()?.ToUpper() == "S";

                    if (fila >= 0 && fila < 10 && col >= 0 && col < 10)
                    {
                        if (jugador.MiTablero.SePuedeColocarAqui(barco, fila, col, horiz))
                        {
                            jugador.MiTablero.PonerBarco(barco, fila, col, horiz);
                            ok = true;
                        }
                        else vista.MostrarError("No se puede");
                    }
                    else vista.MostrarError("Fuera del tablero");
                }
            }

            // CPU coloca barcos
            cpu.ColocarBarcosAlAzar(Flota.ObtenerBarcos());

            // Batalla
            string[] letras = "ABCDEFGHIJ".ToCharArray().Select(c => c.ToString()).ToArray();
            bool turnoJugador = true;

            while (true)
            {
                vista.MostrarTablerosBatalla(jugador, cpu);

                if (turnoJugador)
                {
                    var coord = vista.PedirCoordenada();
                    var res = cpu.MiTablero.LanzarProyectil(coord.fila, coord.columna);
                    jugador.RegistrarDisparo(res);
                    
                    if (cpu.MiTablero.TodosLosBarcosHundidos)
                    {
                        vista.MostrarPantallaFinal(true, jugador);
                        marcador.AgregarEntrada(jugador.Nombre, jugador.TotalDisparos, jugador.Precision);
                        break;
                    }
                }
                else
                {
                    var tiro = cpu.EscogerObjetivo();
                    var res = jugador.MiTablero.LanzarProyectil(tiro.fila, tiro.columna);
                    cpu.RegistrarDisparo(res);
                    
                    if (jugador.MiTablero.TodosLosBarcosHundidos)
                    {
                        vista.MostrarPantallaFinal(false, jugador);
                        break;
                    }
                }

                turnoJugador = !turnoJugador;
                Console.ReadKey(true);
            }
        }
    }
}

