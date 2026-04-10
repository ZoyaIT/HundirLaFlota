using System;
using HundirLaFlota.src.dominio;

namespace HundirLaFlota.tests
{
    public static class TableroTests
    {
        public static void EjecutarTodas()
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║      PRUEBAS DE LA CLASE TABLERO      ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            
            int pasaron = 0;
            int total = 0;
            
            total++;
            if (TestColocarBarco()) pasaron++;
            
            total++;
            if (TestNoColocarBarcoFuera()) pasaron++;
            
            total++;
            if (TestNoColocarBarcosTocados()) pasaron++;
            
            total++;
            if (TestDisparoAgua()) pasaron++;
            
            total++;
            if (TestDisparoImpacto()) pasaron++;
            
            total++;
            if (TestDisparoHundido()) pasaron++;
            
            total++;
            if (TestNoDispararMismoLugar()) pasaron++;
            
            total++;
            if (TestTodosHundidos()) pasaron++;
            
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine($"║  RESULTADO: {pasaron}/{total} pruebas pasaron  {(pasaron == total ? "✓" : "✗")}   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
        }
        
        static bool TestColocarBarco()
        {
            try
            {
                Tablero tablero = new Tablero();
                Barco barco = new Barco("Destructor", 3);
                
                bool puede = tablero.SePuedeColocarAqui(barco, 0, 0, true);
                
                if (!puede)
                {
                    Console.WriteLine("  ✗ PRUEBA 1 - ColocarBarco: no se pudo colocar en posición válida");
                    return false;
                }
                
                tablero.PonerBarco(barco, 0, 0, true);
                
                // Verificar que las casillas tienen el barco
                for (int i = 0; i < 3; i++)
                {
                    if (tablero.DameCasilla(0, i).BarcoQueOcupa != barco)
                    {
                        Console.WriteLine($"  ✗ PRUEBA 1 - ColocarBarco: casilla (0,{i}) no tiene el barco");
                        return false;
                    }
                }
                
                Console.WriteLine("  ✓ PRUEBA 1 - ColocarBarco: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 1 - ColocarBarco: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestNoColocarBarcoFuera()
        {
            try
            {
                Tablero tablero = new Tablero();
                Barco barco = new Barco("Portaaviones", 5);
                
                // Intentar colocar fuera del tablero (horizontal)
                bool puedeHorizontal = tablero.SePuedeColocarAqui(barco, 0, 7, true);
                if (puedeHorizontal)
                {
                    Console.WriteLine("  ✗ PRUEBA 2 - ColocarBarcoFuera: permitió colocar fuera horizontalmente");
                    return false;
                }
                
                // Intentar colocar fuera del tablero (vertical)
                bool puedeVertical = tablero.SePuedeColocarAqui(barco, 7, 0, false);
                if (puedeVertical)
                {
                    Console.WriteLine("  ✗ PRUEBA 2 - ColocarBarcoFuera: permitió colocar fuera verticalmente");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 2 - NoColocarBarcoFuera: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 2 - NoColocarBarcoFuera: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestNoColocarBarcosTocados()
        {
            try
            {
                Tablero tablero = new Tablero();
                Barco barco1 = new Barco("Barco1", 3);
                Barco barco2 = new Barco("Barco2", 2);
                
                tablero.PonerBarco(barco1, 0, 0, true);
                
                // Intentar colocar barco2 tocando al barco1 (adyacente)
                bool puedeTocando = tablero.SePuedeColocarAqui(barco2, 0, 3, true);
                if (!puedeTocando)
                {
                    Console.WriteLine("  ✗ PRUEBA 3 - Se debería poder colocar separado");
                    return false;
                }
                
                // Intentar colocar barco2 tocando al barco1 (diagonal)
                bool puedeDiagonal = tablero.SePuedeColocarAqui(barco2, 1, 3, true);
                if (!puedeDiagonal)
                {
                    Console.WriteLine("  ✗ PRUEBA 3 - Se debería poder colocar en diagonal");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 3 - NoColocarBarcosTocados: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 3 - NoColocarBarcosTocados: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestDisparoAgua()
        {
            try
            {
                Tablero tablero = new Tablero();
                
                ResultadoDisparo resultado = tablero.LanzarProyectil(5, 5);
                
                if (resultado != ResultadoDisparo.Agua)
                {
                    Console.WriteLine($"  ✗ PRUEBA 4 - DisparoAgua: resultado incorrecto: {resultado}");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 4 - DisparoAgua: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 4 - DisparoAgua: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestDisparoImpacto()
        {
            try
            {
                Tablero tablero = new Tablero();
                Barco barco = new Barco("Barco", 3);
                tablero.PonerBarco(barco, 0, 0, true);
                
                ResultadoDisparo resultado = tablero.LanzarProyectil(0, 0);
                
                if (resultado != ResultadoDisparo.Impacto)
                {
                    Console.WriteLine($"  ✗ PRUEBA 5 - DisparoImpacto: resultado incorrecto: {resultado}");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 5 - DisparoImpacto: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 5 - DisparoImpacto: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestDisparoHundido()
        {
            try
            {
                Tablero tablero = new Tablero();
                Barco barco = new Barco("Barco", 2);
                tablero.PonerBarco(barco, 0, 0, true);
                
                tablero.LanzarProyectil(0, 0); // Primer impacto
                ResultadoDisparo resultado = tablero.LanzarProyectil(0, 1); // Segundo impacto (hunde)
                
                if (resultado != ResultadoDisparo.Hundido)
                {
                    Console.WriteLine($"  ✗ PRUEBA 6 - DisparoHundido: resultado incorrecto: {resultado}");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 6 - DisparoHundido: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 6 - DisparoHundido: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestNoDispararMismoLugar()
        {
            try
            {
                Tablero tablero = new Tablero();
                
                tablero.LanzarProyectil(3, 3);
                ResultadoDisparo resultado = tablero.LanzarProyectil(3, 3);
                
                if (resultado != ResultadoDisparo.YaDisparado)
                {
                    Console.WriteLine($"  ✗ PRUEBA 7 - NoDispararMismoLugar: resultado incorrecto: {resultado}");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 7 - NoDispararMismoLugar: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 7 - NoDispararMismoLugar: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestTodosHundidos()
        {
            try
            {
                Tablero tablero = new Tablero();
                Barco barco = new Barco("Barco", 2);
                tablero.PonerBarco(barco, 0, 0, true);
                
                // Al principio no todos están hundidos
                if (tablero.TodosLosBarcosHundidos)
                {
                    Console.WriteLine("  ✗ PRUEBA 8 - TodosHundidos: debería ser false al inicio");
                    return false;
                }
                
                // Hundir el barco
                tablero.LanzarProyectil(0, 0);
                tablero.LanzarProyectil(0, 1);
                
                if (!tablero.TodosLosBarcosHundidos)
                {
                    Console.WriteLine("  ✗ PRUEBA 8 - TodosHundidos: debería ser true después de hundir");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 8 - TodosHundidos: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 8 - TodosHundidos: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
    }
}

