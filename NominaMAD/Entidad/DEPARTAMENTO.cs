using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.Entidad
{
    public class DEPARTAMENTO
    {
        [DisplayName("ID")]
        public int ID_Departamento { get; set; }

        [DisplayName("Nombre del Departamento")]
        public string nombre { get; set; }
        [DisplayName("Estado")]
        public string estatus {  get; set; }
        [DisplayName("Empresa")]
        public string EmpresaID { get; set; }
        public DEPARTAMENTO() { }

        public DEPARTAMENTO(int iD_Departamento, string nombre,string estatus, string empresaID)
        {
            this.ID_Departamento = iD_Departamento;
            this.nombre = nombre;
            this.estatus = estatus;
            this.EmpresaID = empresaID;
        }
    }
}
