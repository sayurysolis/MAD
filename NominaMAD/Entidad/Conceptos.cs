using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.Entidad
{
    public class Concepto
    {

        public int ID_Conceptos { get; set; }
        public bool Tipo { get; set; }          // 1 = Percepcion, 0 = Deduccion
        public string Nombre { get; set; }
        public bool EsPorcentaje { get; set; }
        public decimal Valor { get; set; }
        public bool General { get; set; }
        public bool Estatus { get; set; } = true;

        public Concepto() { }
        public Concepto(int id, bool tipo, string nombre, bool esPorcentaje, decimal valor, bool general, bool estatus = true)
        {
            ID_Conceptos = id;
            Tipo = tipo;
            Nombre = nombre;
            EsPorcentaje = esPorcentaje;
            Valor = valor;
            General = general;
            Estatus = estatus;
        }
    }
}
