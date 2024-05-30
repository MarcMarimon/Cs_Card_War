using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Baraja baraja = null;
            while (true)
            {
                Console.WriteLine("\nMenú de Opciones:");
                Console.WriteLine("1. Crear una carta individual");
                Console.WriteLine("2. Crear una baraja");
                Console.WriteLine("3. Barajar la baraja");
                Console.WriteLine("4. Robar la siguiente carta de la baraja");
                Console.WriteLine("5. Coger una carta de una posición en concreto");
                Console.WriteLine("6. Coger una carta al azar");
                Console.WriteLine("7. Mostrar todas las cartas en la baraja");
                Console.WriteLine("8. Jugar a la batalla de cartas");
                Console.WriteLine("9. Salir");
                Console.Write("Selecciona una opción: ");
                string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            CrearCarta();
                            break;
                        case "2":
                            baraja = new Baraja();
                            Console.WriteLine("Baraja creada.");
                            break;
                        case "3":
                            if (baraja != null)
                            {
                                baraja.Barajar();
                                Console.WriteLine("Baraja barajada.");
                            }
                            else
                                Console.WriteLine("Primero crea una baraja.");
                            break;
                        case "4":
                            if (baraja != null)
                            {
                                Carta cartaRobada = baraja.RobarCarta();
                                if (cartaRobada != null)
                                    Console.WriteLine("Carta robada: " + cartaRobada);
                                else
                                    Console.WriteLine("No quedan mas cartas");
                            }
                            else
                                Console.WriteLine("Primero crea una baraja.");
                            break;
                        case "5":
                            if (baraja != null)
                            {
                                Console.Write("Introduce la posición: ");
                                int posicion = int.Parse(Console.ReadLine());
                                Carta cartaPosicionN = baraja.CogerCartaN(posicion);
                                if (cartaPosicionN != null)
                                    Console.WriteLine("Carta en posición " + posicion + ": " + cartaPosicionN);
                                else
                                    Console.WriteLine("Posición fuera de rango.");
                            }
                            else                           
                                Console.WriteLine("Primero crea una baraja.");
                            break;
                        case "6":
                            if (baraja != null)
                            {
                                Carta cartaAlAzar = baraja.CogerCartaAlAzar();
                                if (cartaAlAzar != null)
                                    Console.WriteLine("Carta al azar: " + cartaAlAzar);
                                else
                                    Console.WriteLine("No quedan cartas en la baraja.");
                            }
                            else
                                Console.WriteLine("Primero crea una baraja.");
                            
                            break;
                        case "7":
                            if (baraja != null)
                            {
                                Console.WriteLine("Cartas en la baraja:");
                                baraja.MostrarBaraja();
                            }
                            else
                                Console.WriteLine("Primero crea una baraja.");
                            break;
                        case "8":
                       
                            BatallaCartas();
                       
                        break;
                    case "9":
                            return;
                        default:
                            Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                            break;
                    }
            }
        }

        private static void CrearCarta()
        {

            Console.Write("Introduce el palo (Oros, Bastos, Espadas, Copas): ");
            string paloInput = Console.ReadLine();

            Carta.ePalos paloEnum;
            if (!Enum.TryParse(paloInput, true, out paloEnum))
            {
                Console.WriteLine("Palo inválido.");
                return;
            }

            Console.Write("Introduce el número (1-12): ");
            if (!int.TryParse(Console.ReadLine(), out int numero) || numero < 1 || numero > 12)
            {
                Console.WriteLine("Número inválido. Debe estar entre 1 y 12.");
                return;
            }

            Carta carta = new Carta(paloEnum, numero);

            if (carta.EsValida())
                Console.WriteLine("Carta creada: " + carta);
            else
                Console.WriteLine("Error al crear la carta.");
        }

        private static void BatallaCartas()
        {
            Console.WriteLine("Bienvenido al juego de batalla de cartas.");

            Console.Write("Por favor, ingresa el número de jugadores (entre 2 y 5): ");
            int numJugadores;
            while (!int.TryParse(Console.ReadLine(), out numJugadores) || numJugadores < 2 || numJugadores > 5)
            {
                Console.WriteLine("Número de jugadores inválido. Debe ser un número entre 2 y 5.");
                Console.Write("Por favor, intenta nuevamente: ");
            }

            List<Jugador> jugadores = new List<Jugador>();
            for (int i = 1; i <= numJugadores; i++)
            {
                Console.Write($"Ingresa el nombre del jugador {i}: ");
                string nombre = Console.ReadLine();
                jugadores.Add(new Jugador(nombre));
            }

            Juego juego = new Juego(jugadores);
            juego.Jugar();
        }
    
    }
 }

