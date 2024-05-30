using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Carta
    {
        public enum ePalos { Oros, Bastos, Espadas, Copas }

        private ePalos? palo;
        private int? numero;

        public ePalos? Palo
        {
            get { return palo; }
            set
            {
                if (Enum.IsDefined(typeof(ePalos), value))
                    palo = value;
            }
        }

        public int? Numero
        {
            get { return numero; }
            set
            {
                if (value >= 1 && value <= 12)
                    numero = value;
            }
        }

        public Carta(ePalos? palo, int? numero)
        {
            Palo = palo;
            Numero = numero;
        }

        public override string ToString()
        {
            if (numero == null || palo == null)
                return "Carta inválida";

            string nombreFigura;
            switch (numero)
            {
                case 1:
                    nombreFigura = "As";
                    break;
                case 10:
                    nombreFigura = "Sota";
                    break;
                case 11:
                    nombreFigura = "Caballo";
                    break;
                case 12:
                    nombreFigura = "Rey";
                    break;
                default:
                    nombreFigura = numero.ToString();
                    break;
            }
            return $"{nombreFigura} de {palo}";
        }

        public bool EsValida()
        {
            return numero != null && palo != null;
        }
    }
}
