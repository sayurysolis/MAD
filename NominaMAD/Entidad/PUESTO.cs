using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.Entidad
{
    public class PUESTO
    {
        public int ID_Puesto {  get; set; } 
        public string Nombre { get; set; }
        public string Descripcion {  get; set; }
        public int EmpresaID {  get; set; }
        public int DepartamentoID {  get; set; }

        public PUESTO() { }

        public PUESTO(int idPuesto, string nombre, string Descripcion, int EmpresaID,int Departamento)
        {
            this.ID_Puesto = idPuesto;
            this.Nombre = nombre;
            this.Descripcion= Descripcion;
            this.EmpresaID = EmpresaID;
            this.DepartamentoID = Departamento;
        }
    }
}
