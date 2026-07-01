using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Interfaces
{
    public class MultaRegular : ICalculadoraMulta
    {
        public decimal Calcular(int dias)
        {
            return dias * 2;
        }
    }
}
