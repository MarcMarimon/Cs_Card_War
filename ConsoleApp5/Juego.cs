using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Juego
    {
        private List<Jugador> jugadores;
        private Baraja baraja;

        // Constructor de la clase Juego
        public Juego(List<Jugador> jugadores)
        {
            this.jugadores = jugadores;
            baraja = new Baraja();
            RepartirCartas(); // Repartir las cartas al inicio del juego
        }

        // Método para repartir las cartas entre los jugadores
        private void RepartirCartas()
        {
            while (baraja.CantidadCartas() > 0)
            {
                foreach (var jugador in jugadores)
                {
                    Carta carta = baraja.CogerCartaAlAzar();
                    if (carta != null)
                        jugador.Cartas.Add(carta);
                }
            }
        }

        // Método principal para iniciar el juego
        public void Jugar()
        {
            Jugador ganador = null;

            // Mientras no haya un ganador, continuar el juego
            while ((ganador = JuegoTerminado()) == null)
            {
                RealizarRonda();  // Realizar una nueva ronda
            }

            Console.WriteLine($"¡{ganador.Nombre} gana el juego!"); // Anunciar al ganador
        }

        // Método para realizar una ronda de juego
        private void RealizarRonda()
        {
            List<Jugador> ganadoresRonda = new List<Jugador>();
            List<(Carta carta, Jugador jugador)> cartasEnJuego = ObtenerCartasEnJuego(); // Obtener las cartas jugadas

            Carta cartaMasAlta = EncontrarCartaMasAlta(cartasEnJuego.Select(t => t.carta).ToList()); // Encontrar la carta más alta

            // Determinar los ganadores de la ronda
            foreach (var jugadorJugando in cartasEnJuego)
            {
                if (jugadorJugando.carta.Numero == cartaMasAlta.Numero)
                    ganadoresRonda.Add(jugadorJugando.jugador);
                
            }

            // Si hay empate, realizar un desempate
            if (ganadoresRonda.Count > 1)
            {
                Console.WriteLine("¡Empate! Se realizará un desempate...");
                RealizarDesempate(ganadoresRonda); 
            }
            // Si solo hay un ganador, asignarle las cartas ganadas
            else if (ganadoresRonda.Count == 1)
            {
                ganadoresRonda[0].Cartas.AddRange(cartasEnJuego.Select(t => t.carta));
                Console.WriteLine($"{ganadoresRonda[0].Nombre} gana la ronda y ahora tiene {ganadoresRonda[0].Cartas.Count} cartas");
            }
        }

        // Método para obtener las cartas jugadas en la ronda
        private List<(Carta carta, Jugador jugador)> ObtenerCartasEnJuego() 
        {
            List<(Carta carta, Jugador jugador)> cartasEnJuego = new List<(Carta, Jugador)>();

            foreach (var jugador in jugadores)
            {
                if (jugador.Cartas.Count > 0)
                {
                    Carta cartaJugada = jugador.Cartas[0];
                    jugador.Cartas.RemoveAt(0);
                    cartasEnJuego.Add((cartaJugada, jugador));
                    Console.WriteLine($"{jugador.Nombre} juega {cartaJugada}");
                }
            }

            return cartasEnJuego;
        }

        // Método para realizar un desempate en caso de empate
        private void RealizarDesempate(List<Jugador> jugadoresEmpatados)
        {
            bool empateResuelto = false;

            while (!empateResuelto)
            {
                List<Carta> cartasDesempate = new List<Carta>();
                List<Jugador> ganadoresDesempate = new List<Jugador>();

                // Obtener las cartas jugadas en el desempate
                List<(Carta carta, Jugador jugador)> cartasEnJuego = new List<(Carta, Jugador)>();

                foreach (var jugador in jugadoresEmpatados)
                {
                    if (jugador.Cartas.Count > 0)
                    {
                        Carta cartaJugada = jugador.Cartas[0];
                        jugador.Cartas.RemoveAt(0);
                        cartasEnJuego.Add((cartaJugada, jugador));
                        Console.WriteLine($"{jugador.Nombre} juega {cartaJugada}");
                    }
                }

                // Encontrar la carta más alta entre las jugadas en el desempate
                Carta cartaMasAltaDesempate = EncontrarCartaMasAlta(cartasEnJuego.Select(t => t.carta).ToList());

                // Determinar los ganadores del desempate
                foreach (var jugadorDesempate in cartasEnJuego)
                {
                    if (jugadorDesempate.carta.Numero == cartaMasAltaDesempate.Numero)
                        ganadoresDesempate.Add(jugadorDesempate.jugador);
                }

                // Si hay un único ganador del desempate, asignarle las cartas ganadas
                if (ganadoresDesempate.Count == 1)
                {
                    ganadoresDesempate[0].Cartas.AddRange(cartasEnJuego.Select(t => t.carta));
                    Console.WriteLine($"{ganadoresDesempate[0].Nombre} gana el desempate y ahora tiene {ganadoresDesempate[0].Cartas.Count} cartas");
                    empateResuelto = true;
                }
                // Si aún hay empate en el desempate, continuar con otro desempate
                else
                {
                    jugadoresEmpatados = new List<Jugador>(ganadoresDesempate);
                    Console.WriteLine("¡Empate en el desempate! Se realizará otro desempate...");
                }
            }
        }

        // Método para encontrar la carta más alta entre un conjunto de cartas
        private Carta EncontrarCartaMasAlta(List<Carta> cartas)
        {
            return cartas.OrderByDescending(c => c.Numero).First();
        }

        // Método para verificar si el juego ha terminado
        private Jugador JuegoTerminado()
        {
            return jugadores.FirstOrDefault(j => j.Cartas.Count == 48);
        }
    }
}
