using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.Entidad
{
    public class DEPARTAMENTO
    {
        public int ID_Departamento { get; set; }
        public string nombre { get; set; }
        public bool estado {  get; set; }
        public string EmpresaID { get; set; }

        public DEPARTAMENTO() { }

        public DEPARTAMENTO(int iD_Departamento, string nombre, string empresaID)
        {
            this.ID_Departamento = iD_Departamento;
            this.nombre = nombre;
            this.EmpresaID = empresaID;
        }
    }
}
