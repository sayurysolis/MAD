using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.Entidad
{
    public class PUESTO
    {
        [DisplayName("ID")]
        public int ID_Puesto { get; set; }
        
        [DisplayName("Nombre del PUESTO")]
        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; } 

        [DisplayName("Estado")]
        public string estatus { get; set; }

        [DisplayName("Empresa")]
        public string EmpresaID { get; set; }

        [DisplayName("Departamento")]
        public string DepartamentoID {  get; set; }

          public PUESTO() { }

        public PUESTO(int idPuesto, string nombre, string Descripcion,string estatus, string EmpresaID,string Departamento)
        {
            this.ID_Puesto = idPuesto;
            this.Nombre = nombre;
            this.Descripcion= Descripcion;
            this.estatus = estatus;
            this.EmpresaID = EmpresaID;
            this.DepartamentoID = Departamento;
        }
    }
}
