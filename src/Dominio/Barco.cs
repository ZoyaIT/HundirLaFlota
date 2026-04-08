using System.Collections.Generic;

namespace HundirLaFlota.src.dominio
{
    public class Barco
    {
        public string NombreDelBarco;
        public int Tamaño;
        private int impactos;
        public List<Casilla> CasillasQueOcupa;

        public Barco(string nombre, int tam)
        {
            NombreDelBarco = nombre;
            Tamaño = tam;
            impactos = 0;
            CasillasQueOcupa = new List<Casilla>();
        }

        public void RecibirUnImpacto()
        {
            impactos++;
        }

        public bool EstaDestruido => impactos >= Tamaño;
    }
}

