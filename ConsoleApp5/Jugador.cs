using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Jugador
    {
        private string nombre;
        private List<Carta> cartas;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public List<Carta> Cartas
        {
            get { return cartas; }
            set { cartas = value; }
        }

        public Jugador(string nombre)
        {
            Nombre = nombre;
            Cartas = new List<Carta>();
        }
    }
}
