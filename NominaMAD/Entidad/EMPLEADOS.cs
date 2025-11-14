using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.Entidad
{
    public class EMPLEADOS
    {
        public int ID_Empleado { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public DateTime fechaNacimiento { get; set; }
        //---------------------------//
        public string empresaID { get; set; }
        public string depID { get; set; }
        public string puestoID { get; set; }
        //---------------------------//
        public string CURP { get; set; }
        public string NSS { get; set; }
        public string RFC{ get; set; }

        public string banco { get; set; }
        public string numCuenta { get; set; }
        public Decimal SalarioDiario { get; set; }
        public Decimal SalarioDiarioIntegrado { get; set; }



        public string calle {  get; set; }
        public int numero {  get; set; }
        public string colonia {get; set;}

        public string municipio  {get; set;}
        public string estado { get; set; }
        public string CodigoPostal {  get; set; }


        public string Email { get; set; }
     
        public string Telefono{ get; set; }

        public bool estatus {  get; set; }
        public DateTime fechaIngreso { get; set; }

        public EMPLEADOS() {}

        public EMPLEADOS(string empresaID, string depID, string puestoID, 
            int iD_Empleado, string CURP, string RFC, string NSS,
            string nombre, string apellidoP, string apellidoM, DateTime fechaNacimiento,
            string banco, string numCuenta, decimal salarioDiario, decimal salarioDiarioIntegrado,
            string calle, int numero, string colonia, string municipio, string estado, string codigoPostal,
            string email, string telefono, bool estatus,DateTime fechaIngreso)
        {
            this.empresaID = empresaID;
            this.depID = depID;
            this.puestoID = puestoID;
            this.ID_Empleado = iD_Empleado;
            this.CURP = CURP;
            this.RFC = RFC;
            this.NSS = NSS;
            this.nombre = nombre;
            this.apellidoP = apellidoP;
            this.apellidoM = apellidoM;
            this.fechaNacimiento = fechaNacimiento;
            this.banco = banco;
            this.numCuenta = numCuenta;
            this.SalarioDiario = salarioDiario;
            this.SalarioDiarioIntegrado = salarioDiarioIntegrado;
            this.Email = email;
            this.calle = calle;
            this.numero = numero;
            this.colonia = colonia;
            this.municipio = municipio;
            this.estado = estado;
            this.CodigoPostal = codigoPostal;
            this.Telefono = telefono;
            this.estatus = estatus;
            this.fechaIngreso = fechaIngreso;
        }
    }
}
