using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Baraja
    {
        private List<Carta> cartas;

        public Baraja()
        {
            cartas = new List<Carta>();
            CrearBaraja();
            Barajar();
        }

        private void CrearBaraja()
        {
            foreach (Carta.ePalos palo in Enum.GetValues(typeof(Carta.ePalos)))
            {
                for (int numero = 1; numero <= 12; numero++)
                {
                    var carta = new Carta(palo, numero);
                    if (carta.EsValida())
                    {
                        cartas.Add(carta);
                    }
                }
            }
        }

        public void Barajar()
        {
            Random random = new Random();
            cartas = cartas.OrderBy(c => random.Next()).ToList();
        }

        public Carta RobarCarta()
        {
            if (cartas.Count == 0) return null;

            Carta cartaRobada = cartas[0];
            cartas.RemoveAt(0);
            return cartaRobada;
        }

        public Carta CogerCartaN(int posicion)
        {
            if (posicion < 0 || posicion >= cartas.Count) return null;

            Carta carta = cartas[posicion];
            cartas.RemoveAt(posicion);
            return carta;
        }

        public Carta CogerCartaAlAzar()
        {
            if (cartas.Count == 0) return null;

            Random rnd = new Random();
            int indice = rnd.Next(cartas.Count);
            Carta carta = cartas[indice];
            cartas.RemoveAt(indice);
            return carta;
        }

        public void MostrarBaraja()
        {
            foreach (var carta in cartas)
            {
                Console.WriteLine(carta);
            }
        }
        public int CantidadCartas()
        {
            return cartas.Count;
        }
    }
}

