using System;
using System.Collections.Generic;
using HundirLaFlota.src.dominio;
using HundirLaFlota.src.Motor;

namespace HundirLaFlota.tests
{
    public static class CpuTests
    {
        public static void EjecutarTodas()
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║        PRUEBAS DE LA CLASE CPU        ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            
            int pasaron = 0;
            int total = 0;
            
            total++;
            if (TestColocarBarcosAlAzar()) pasaron++;
            
            total++;
            if (TestEscogerObjetivoSinRepetir()) pasaron++;
            
            total++;
            if (TestTodasLasCoordenadas()) pasaron++;
            
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine($"║  RESULTADO: {pasaron}/{total} pruebas pasaron  {(pasaron == total ? "✓" : "✗")}   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
        }
        
        static bool TestColocarBarcosAlAzar()
        {
            try
            {
                Cpu cpu = new Cpu();
                List<Barco> flota = Flota.ObtenerBarcos();
                
                cpu.ColocarBarcosAlAzar(flota);
                
                // Verificar que se colocaron 5 barcos
                int barcosColocados = 0;
                for (int f = 0; f < 10; f++)
                {
                    for (int c = 0; c < 10; c++)
                    {
                        if (cpu.MiTablero.DameCasilla(f, c).BarcoQueOcupa != null)
                        {
                            barcosColocados++;
                        }
                    }
                }
                
                // Suma de tamaños: 5+4+3+3+2 = 17 casillas
                if (barcosColocados == 17)
                {
                    Console.WriteLine("  ✓ PRUEBA 1 - ColocarBarcosAlAzar: OK");
                    return true;
                }
                else
                {
                    Console.WriteLine($"  ✗ PRUEBA 1 - ColocarBarcosAlAzar: se colocaron {barcosColocados} casillas, se esperaban 17");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 1 - ColocarBarcosAlAzar: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestEscogerObjetivoSinRepetir()
        {
            try
            {
                Cpu cpu = new Cpu();
                HashSet<(int, int)> objetivosUsados = new HashSet<(int, int)>();
                
                // Escoger 100 objetivos (todas las coordenadas)
                for (int i = 0; i < 100; i++)
                {
                    var objetivo = cpu.EscogerObjetivo();
                    
                    if (objetivosUsados.Contains(objetivo))
                    {
                        Console.WriteLine($"  ✗ PRUEBA 2 - EscogerObjetivo: se repitió la coordenada ({objetivo.fila}, {objetivo.columna})");
                        return false;
                    }
                    
                    objetivosUsados.Add(objetivo);
                }
                
                if (objetivosUsados.Count == 100)
                {
                    Console.WriteLine("  ✓ PRUEBA 2 - EscogerObjetivoSinRepetir: OK");
                    return true;
                }
                else
                {
                    Console.WriteLine($"  ✗ PRUEBA 2 - EscogerObjetivoSinRepetir: solo {objetivosUsados.Count} coordenadas únicas");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 2 - EscogerObjetivoSinRepetir: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestTodasLasCoordenadas()
        {
            try
            {
                Cpu cpu = new Cpu();
                bool[,] coordenadasVisitadas = new bool[10, 10];
                
                // Escoger 100 objetivos
                for (int i = 0; i < 100; i++)
                {
                    var objetivo = cpu.EscogerObjetivo();
                    coordenadasVisitadas[objetivo.fila, objetivo.columna] = true;
                }
                
                // Verificar que todas las coordenadas fueron visitadas
                bool todasVisitadas = true;
                for (int f = 0; f < 10; f++)
                {
                    for (int c = 0; c < 10; c++)
                    {
                        if (!coordenadasVisitadas[f, c])
                        {
                            todasVisitadas = false;
                            Console.WriteLine($"  ✗ PRUEBA 3 - Faltó la coordenada ({f}, {c})");
                        }
                    }
                }
                
                if (todasVisitadas)
                {
                    Console.WriteLine("  ✓ PRUEBA 3 - TodasLasCoordenadas: OK");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 3 - TodasLasCoordenadas: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
    }
}
