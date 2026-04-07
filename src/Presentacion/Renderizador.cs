using System;
using System.Threading;
using HundirLaFlota.src.dominio;
using HundirLaFlota.src.motor;

namespace HundirLaFlota.src.presentacion
{
    public class Renderizador
    {
        private string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        public void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(ArteAscii.Logo);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("1. Nueva partida");
            Console.WriteLine("2. Salir");
            Console.WriteLine();
            Console.Write("Selecciona una opción: ");
        }

        public void MostrarBienvenida()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(ArteAscii.Logo);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Bienvenido a Hundir la Flota");
            Console.WriteLine("Pulsa ENTER para comenzar...");
            Console.ReadLine();
        }

        public void MostrarTablerosBatalla(Jugador jugador, Cpu cpu)
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("                       HUNDIR LA FLOTA");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("     TU TABLERO                          MAR ENEMIGO");
            Console.Write("       ");
            
            for (int c = 1; c <= 10; c++)
                Console.Write($"{c,3}");
            Console.Write("           ");
            for (int c = 1; c <= 10; c++)
                Console.Write($"{c,3}");
            Console.WriteLine();

            for (int f = 0; f < 10; f++)
            {
                Console.Write($"  {letras[f]}   ");
                for (int c = 0; c < 10; c++)
                    ImprimirCasilla(jugador.MiTablero.DameCasilla(f, c), true);

                Console.Write($"       {letras[f]}   ");
                for (int c = 0; c < 10; c++)
                    ImprimirCasilla(cpu.MiTablero.DameCasilla(f, c), false);

                Console.WriteLine();
            }

            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine($"  Disparos: {jugador.TotalDisparos}   Aciertos: {jugador.TotalAciertos}   Fallos: {jugador.TotalFallos}   Precisión: {jugador.Precision:F1}%");
            Console.WriteLine($"  Barcos restantes: {jugador.MiTablero.CuantosBarcosQuedan}/5       Barcos enemigos: {cpu.MiTablero.CuantosBarcosQuedan}/5");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("  . = vacío   S = barco   ~ = agua   X = impacto   # = hundido");
            Console.WriteLine();
        }

        private void ImprimirCasilla(Casilla casilla, bool esPropio)
        {
            if (casilla.FueDisparada)
            {
                if (casilla.BarcoQueOcupa != null && casilla.BarcoQueOcupa.EstaDestruido)
                {
                    Console.ForegroundColor = Colores.Hundido;
                    Console.Write("  #");
                }
                else if (casilla.BarcoQueOcupa != null)
                {
                    Console.ForegroundColor = Colores.Impacto;
                    Console.Write("  X");
                }
                else
                {
                    Console.ForegroundColor = Colores.Agua;
                    Console.Write("  ~");
                }
            }
            else if (!casilla.EstaLibre && esPropio)
            {
                Console.ForegroundColor = Colores.BarcoPropio;
                Console.Write("  S");
            }
            else
            {
                Console.ForegroundColor = Colores.CasillaVacia;
                Console.Write("  .");
            }
            Console.ResetColor();
        }

        public (int fila, int columna) PedirCoordenada()
        {
            while (true)
            {
                Console.Write("Coordenada (ej. B7): ");
                string? input = Console.ReadLine()?.Trim().ToUpper();

                if (string.IsNullOrWhiteSpace(input))
                {
                    MostrarError("Coordenada inválida");
                    continue;
                }

                if (input.Length < 2)
                {
                    MostrarError("Formato inválido. Usa como B7");
                    continue;
                }

                char letra = input[0];
                if (letra < 'A' || letra > 'J')
                {
                    MostrarError("Fila inválida. Usa A-J");
                    continue;
                }

                string numeroStr = input.Substring(1);
                if (!int.TryParse(numeroStr, out int columna) || columna < 1 || columna > 10)
                {
                    MostrarError("Columna inválida. Usa 1-10");
                    continue;
                }

                return (letra - 'A', columna - 1);
            }
        }

        public void MostrarTableroColocacion(Tablero tablero, Barco barco)
        {
            Console.Clear();
            Console.WriteLine($"Colocando {barco.NombreDelBarco} (tamaño {barco.Tamaño} casillas)");
            Console.WriteLine();
            Console.Write("     ");
            
            for (int c = 1; c <= 10; c++)
                Console.Write($"{c,2} ");
            Console.WriteLine();

            for (int f = 0; f < 10; f++)
            {
                Console.Write($"  {letras[f]}  ");
                for (int c = 0; c < 10; c++)
                {
                    Casilla casilla = tablero.DameCasilla(f, c);
                    if (!casilla.EstaLibre && casilla.BarcoQueOcupa != null)
                    {
                        Console.ForegroundColor = Colores.BarcoPropio;
                        Console.Write(" S ");
                    }
                    else
                    {
                        Console.ForegroundColor = Colores.CasillaVacia;
                        Console.Write(" . ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void MostrarResultadoDisparo(ResultadoDisparo resultado, string letra, int numero)
        {
            Console.WriteLine();
            switch (resultado)
            {
                case ResultadoDisparo.Agua:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"¡AGUA! Disparaste a {letra}{numero} y no había nada.");
                    break;
                case ResultadoDisparo.Impacto:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"¡IMPACTO! Has dado a un barco en {letra}{numero}");
                    break;
                case ResultadoDisparo.Hundido:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"¡HUNDIDO! ¡Has destruido un barco enemigo!");
                    break;
                case ResultadoDisparo.YaDisparado:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"Ya habías disparado a {letra}{numero}");
                    break;
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public void MostrarDisparoCPU(ResultadoDisparo resultado, string letra, int numero)
        {
            Console.WriteLine();
            switch (resultado)
            {
                case ResultadoDisparo.Agua:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"La CPU disparó a {letra}{numero} y falló.");
                    break;
                case ResultadoDisparo.Impacto:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"¡La CPU impactó en {letra}{numero}!");
                    break;
                case ResultadoDisparo.Hundido:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"¡La CPU hundió uno de tus barcos en {letra}{numero}!");
                    break;
                default:
                    Console.WriteLine($"La CPU disparó a {letra}{numero}");
                    break;
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public void MostrarPantallaFinal(bool ganaJugador, Jugador jugador)
        {
            Console.Clear();
            
            if (ganaJugador)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(ArteAscii.Victoria);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ArteAscii.Derrota);
            }
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("═══════════════════ ESTADÍSTICAS FINALES ═══════════════════");
            Console.WriteLine($"  Disparos totales: {jugador.TotalDisparos}");
            Console.WriteLine($"  Aciertos: {jugador.TotalAciertos}");
            Console.WriteLine($"  Fallos: {jugador.TotalFallos}");
            Console.WriteLine($"  Precisión: {jugador.Precision:F1}%");
            Console.WriteLine("═════════════════════════════════════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("Presiona cualquier tecla para volver al menú...");
            Console.ReadKey(true);
        }

        public void MostrarError(string mensaje)
        {
            Console.ForegroundColor = Colores.Error;
            Console.WriteLine($"Error: {mensaje}");
            Console.ResetColor();
            Thread.Sleep(1500);
        }

        public void MostrarMensajeError(string mensaje)
        {
            MostrarError(mensaje);
        }
    }
}

