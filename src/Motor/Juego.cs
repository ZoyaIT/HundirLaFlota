using System;
using System.Collections.Generic;
using System.Threading;
using HundirLaFlota.src.dominio;
using HundirLaFlota.src.presentacion;

namespace HundirLaFlota.src.motor
{
    public class Juego
    {
        private Jugador jugadorHumano;
        private Cpu cpu;
        private Renderizador pantalla;
        private List<Barco> flota;
        private bool turnoJugador;

        private enum Fase
        {
            Colocando,
            Batalla,
            Fin
        }

        private Fase faseActual;

        public Juego()
        {
            pantalla = new Renderizador();
            flota = Flota.ObtenerBarcos();
            faseActual = Fase.Colocando;
            turnoJugador = true;
        }

        public void Iniciar()
        {
            bool salir = false;

            while (!salir)
            {
                pantalla.MostrarMenuPrincipal();
                string? op = Console.ReadLine();

                if (op == "1")
                {
                    NuevaPartida();
                }
                else if (op == "2")
                {
                    salir = true;
                    Console.WriteLine("Hasta luego");
                }
                else
                {
                    pantalla.MostrarMensajeError("Opción incorrecta");
                }
            }
        }

        private void NuevaPartida()
        {
            Console.Clear();

            Console.Write("Nombre del jugador: ");
            string? nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
                nombre = "Jugador";

            jugadorHumano = new Jugador(nombre);
            cpu = new Cpu();

            ColocarBarcosJugador();

            Console.WriteLine("La CPU está colocando barcos...");
            cpu.ColocarBarcosAlAzar(flota);

            Console.WriteLine("Todo listo. Comienza la batalla.");
            Thread.Sleep(1500);

            faseActual = Fase.Batalla;
            turnoJugador = true;

            Batalla();

            bool gano = jugadorHumano.MiTablero.TodosLosBarcosHundidos;
            pantalla.MostrarPantallaFinal(gano, jugadorHumano);

            faseActual = Fase.Fin;
        }

        private void ColocarBarcosJugador()
        {
            var barcos = new List<Barco>(flota);

            foreach (var barco in barcos)
            {
                bool colocado = false;

                while (!colocado)
                {
                    pantalla.MostrarTableroColocacion(jugadorHumano.MiTablero, barco);

                    Console.WriteLine("Colocando " + barco.NombreDelBarco + " tamaño " + barco.Tamaño);
                    Console.Write("Fila (A-J): ");
                    string? inputFila = Console.ReadLine()?.ToUpper();

                    Console.Write("Columna (1-10): ");
                    string? inputCol = Console.ReadLine();

                    Console.Write("Horizontal? (S/N): ");
                    string? inputHoriz = Console.ReadLine()?.ToUpper();

                    if (string.IsNullOrEmpty(inputFila))
                    {
                        pantalla.MostrarMensajeError("Fila inválida");
                        continue;
                    }

                    int fila = inputFila[0] - 'A';
                    
                    if (!int.TryParse(inputCol, out int col))
                    {
                        pantalla.MostrarMensajeError("Columna inválida");
                        continue;
                    }
                    
                    int columna = col - 1;
                    bool horizontal = inputHoriz == "S";

                    if (fila < 0 || fila >= 10 || columna < 0 || columna >= 10)
                    {
                        pantalla.MostrarMensajeError("Coordenadas fuera del tablero");
                        continue;
                    }

                    if (jugadorHumano.MiTablero.SePuedeColocarAqui(barco, fila, columna, horizontal))
                    {
                        jugadorHumano.MiTablero.PonerBarco(barco, fila, columna, horizontal);
                        colocado = true;
                        Console.WriteLine("Barco colocado");
                        Thread.Sleep(800);
                    }
                    else
                    {
                        pantalla.MostrarMensajeError("No se puede poner ahí");
                    }
                }
            }
        }

        private void Batalla()
        {
            string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            while (faseActual == Fase.Batalla)
            {
                pantalla.MostrarTablerosBatalla(jugadorHumano, cpu);

                if (turnoJugador)
                {
                    Console.WriteLine("Turno de " + jugadorHumano.Nombre);

                    var coord = pantalla.PedirCoordenada();
                    int fila = coord.fila;
                    int columna = coord.columna;

                    ResultadoDisparo resultado = cpu.MiTablero.LanzarProyectil(fila, columna);
                    jugadorHumano.RegistrarDisparo(resultado);

                    pantalla.MostrarResultadoDisparo(resultado, letras[fila], columna + 1);

                    if (cpu.MiTablero.TodosLosBarcosHundidos)
                    {
                        Console.WriteLine("¡Ganaste la batalla!");
                        Thread.Sleep(1500);
                        faseActual = Fase.Fin;
                        break;
                    }

                    turnoJugador = false;
                }
                else
                {
                    Console.WriteLine("Turno de la CPU...");
                    Thread.Sleep(1000);

                    var tiro = cpu.EscogerObjetivo();
                    int fila = tiro.fila;
                    int columna = tiro.columna;

                    ResultadoDisparo resultado = jugadorHumano.MiTablero.LanzarProyectil(fila, columna);
                    cpu.RegistrarDisparo(resultado);

                    pantalla.MostrarDisparoCPU(resultado, letras[fila], columna + 1);

                    if (jugadorHumano.MiTablero.TodosLosBarcosHundidos)
                    {
                        Console.WriteLine("La CPU ganó...");
                        Thread.Sleep(1500);
                        faseActual = Fase.Fin;
                        break;
                    }

                    turnoJugador = true;
                }

                Console.WriteLine("Pulsa una tecla para continuar...");
                Console.ReadKey(true);
            }
        }
    }
}

