using HundirLaFlota.src.dominio;

namespace HundirLaFlota.tests
{
    public static class BarcoTests
    {
        public static void EjecutarTodas()
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║       PRUEBAS DE LA CLASE BARCO       ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            
            int pasaron = 0;
            int total = 0;
            
            total++;
            if (TestConstructor()) pasaron++;
            
            total++;
            if (TestRecibirImpactos()) pasaron++;
            
            total++;
            if (TestHundimientoTamanio2()) pasaron++;
            
            total++;
            if (TestHundimientoTamanio3()) pasaron++;
            
            total++;
            if (TestHundimientoTamanio4()) pasaron++;
            
            total++;
            if (TestHundimientoTamanio5()) pasaron++;
            
            total++;
            if (TestIndependenciaBarcos()) pasaron++;
            
            total++;
            if (TestCasillasQueOcupa()) pasaron++;
            
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine($"║  RESULTADO: {pasaron}/{total} pruebas pasaron  {(pasaron == total ? "✓" : "✗")}   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
        }
        
        static bool TestConstructor()
        {
            try
            {
                Barco barco = new Barco("Portaaviones", 5);
                
                bool nombreOk = barco.NombreDelBarco == "Portaaviones";
                bool tamanioOk = barco.Tamaño == 5;
                bool noHundido = !barco.EstaDestruido;
                bool casillasExiste = barco.CasillasQueOcupa != null;
                
                if (nombreOk && tamanioOk && noHundido && casillasExiste)
                {
                    Console.WriteLine("  ✓ PRUEBA 1 - Constructor: OK");
                    return true;
                }
                else
                {
                    Console.WriteLine("  ✗ PRUEBA 1 - Constructor: FALLO");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 1 - Constructor: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestRecibirImpactos()
        {
            try
            {
                Barco barco = new Barco("Destructor", 3);
                
                barco.RecibirUnImpacto();
                if (barco.EstaDestruido)
                {
                    Console.WriteLine("  ✗ PRUEBA 2 - Impactos: se hundió con 1 impacto");
                    return false;
                }
                
                barco.RecibirUnImpacto();
                if (barco.EstaDestruido)
                {
                    Console.WriteLine("  ✗ PRUEBA 2 - Impactos: se hundió con 2 impactos");
                    return false;
                }
                
                barco.RecibirUnImpacto();
                if (!barco.EstaDestruido)
                {
                    Console.WriteLine("  ✗ PRUEBA 2 - Impactos: no se hundió con 3 impactos");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 2 - RecibirImpactos: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 2 - RecibirImpactos: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestHundimientoTamanio2()
        {
            try
            {
                Barco barco = new Barco("Patrullera", 2);
                
                barco.RecibirUnImpacto();
                if (barco.EstaDestruido)
                {
                    Console.WriteLine("  ✗ PRUEBA 3 - Tamaño 2: se hundió con 1 impacto");
                    return false;
                }
                
                barco.RecibirUnImpacto();
                if (!barco.EstaDestruido)
                {
                    Console.WriteLine("  ✗ PRUEBA 3 - Tamaño 2: no se hundió con 2 impactos");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 3 - Hundimiento tamaño 2: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 3 - Hundimiento tamaño 2: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestHundimientoTamanio3()
        {
            try
            {
                Barco barco = new Barco("Submarino", 3);
                
                for (int i = 1; i <= 3; i++)
                {
                    barco.RecibirUnImpacto();
                    
                    if (i < 3 && barco.EstaDestruido)
                    {
                        Console.WriteLine($"  ✗ PRUEBA 4 - Tamaño 3: se hundió con {i} impactos");
                        return false;
                    }
                    
                    if (i == 3 && !barco.EstaDestruido)
                    {
                        Console.WriteLine("  ✗ PRUEBA 4 - Tamaño 3: no se hundió con 3 impactos");
                        return false;
                    }
                }
                
                Console.WriteLine("  ✓ PRUEBA 4 - Hundimiento tamaño 3: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 4 - Hundimiento tamaño 3: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestHundimientoTamanio4()
        {
            try
            {
                Barco barco = new Barco("Acorazado", 4);
                
                for (int i = 1; i <= 4; i++)
                {
                    barco.RecibirUnImpacto();
                    
                    if (i < 4 && barco.EstaDestruido)
                    {
                        Console.WriteLine($"  ✗ PRUEBA 5 - Tamaño 4: se hundió con {i} impactos");
                        return false;
                    }
                    
                    if (i == 4 && !barco.EstaDestruido)
                    {
                        Console.WriteLine("  ✗ PRUEBA 5 - Tamaño 4: no se hundió con 4 impactos");
                        return false;
                    }
                }
                
                Console.WriteLine("  ✓ PRUEBA 5 - Hundimiento tamaño 4: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 5 - Hundimiento tamaño 4: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestHundimientoTamanio5()
        {
            try
            {
                Barco barco = new Barco("Portaaviones", 5);
                
                for (int i = 1; i <= 5; i++)
                {
                    barco.RecibirUnImpacto();
                    
                    if (i < 5 && barco.EstaDestruido)
                    {
                        Console.WriteLine($"  ✗ PRUEBA 6 - Tamaño 5: se hundió con {i} impactos");
                        return false;
                    }
                    
                    if (i == 5 && !barco.EstaDestruido)
                    {
                        Console.WriteLine("  ✗ PRUEBA 6 - Tamaño 5: no se hundió con 5 impactos");
                        return false;
                    }
                }
                
                Console.WriteLine("  ✓ PRUEBA 6 - Hundimiento tamaño 5: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 6 - Hundimiento tamaño 5: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestIndependenciaBarcos()
        {
            try
            {
                Barco barco1 = new Barco("Barco1", 2);
                Barco barco2 = new Barco("Barco2", 3);
                
                barco1.RecibirUnImpacto();
                barco1.RecibirUnImpacto();
                
                if (!barco1.EstaDestruido)
                {
                    Console.WriteLine("  ✗ PRUEBA 7 - Independencia: barco1 no se hundió");
                    return false;
                }
                
                if (barco2.EstaDestruido)
                {
                    Console.WriteLine("  ✗ PRUEBA 7 - Independencia: barco2 se hundió sin recibir daño");
                    return false;
                }
                
                barco2.RecibirUnImpacto();
                barco2.RecibirUnImpacto();
                barco2.RecibirUnImpacto();
                
                if (!barco2.EstaDestruido)
                {
                    Console.WriteLine("  ✗ PRUEBA 7 - Independencia: barco2 no se hundió después de 3 impactos");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 7 - Independencia entre barcos: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 7 - Independencia: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
        
        static bool TestCasillasQueOcupa()
        {
            try
            {
                Barco barco = new Barco("Destructor", 3);
                
                Casilla c1 = new Casilla(0, 0);
                Casilla c2 = new Casilla(0, 1);
                Casilla c3 = new Casilla(0, 2);
                
                barco.CasillasQueOcupa.Add(c1);
                barco.CasillasQueOcupa.Add(c2);
                barco.CasillasQueOcupa.Add(c3);
                
                if (barco.CasillasQueOcupa.Count != 3)
                {
                    Console.WriteLine("  ✗ PRUEBA 8 - CasillasQueOcupa: cantidad incorrecta");
                    return false;
                }
                
                if (barco.CasillasQueOcupa[0] != c1 || barco.CasillasQueOcupa[1] != c2 || barco.CasillasQueOcupa[2] != c3)
                {
                    Console.WriteLine("  ✗ PRUEBA 8 - CasillasQueOcupa: las casillas no coinciden");
                    return false;
                }
                
                Console.WriteLine("  ✓ PRUEBA 8 - CasillasQueOcupa: OK");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ PRUEBA 8 - CasillasQueOcupa: EXCEPCIÓN - {ex.Message}");
                return false;
            }
        }
    }
}