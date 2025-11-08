using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.Entidad
{
    public class Empleado
    {
        public int empresaID { get; set; }
        public int depID { get; set; }
        public int puestoID{ get; set; }
        public bool gerente { get; set; }

        public int ID_Empleado { get; set;}
        public string contrasena { get; set; }


        public string CURP { get; set; }
        public string RFC{ get; set; }
        public string NSS { get; set; }

        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public DateTime fechaNacimiento {  get; set; }

        public string banco { get; set; }
        public string numCuenta{ get; set; }
        public  Decimal SalarioDiario {  get; set; }
        public Decimal SalarioDiarioIntegrado{ get; set; }

        public string Email { get; set; }
        public int DireccionID{ get; set; }
        public int TelefonoID{ get; set; }

        public bool estatus {  get; set; }
        public DateTime fechaIngreso { get; set; }

        public Empleado() {}

        public Empleado (int empresaID, int depID, int puestoID, bool gerente, int iD_Empleado, string contrasena, string CURP, string RFC, string NSS, string nombre,
            string apellidoP, string apellidoM, DateTime fechaNacimiento, string banco, string numCuenta, decimal salarioDiario, decimal salarioDiarioIntegrado,
            string email, int direccionID, int telefonoID, bool estatus, DateTime fechaIngreso)
        {
            this.empresaID = empresaID;
            this.depID = depID;
            this.puestoID = puestoID;
            this.gerente = gerente;
            this.ID_Empleado = iD_Empleado;
            this.contrasena = contrasena;
            this.CURP = CURP;
            this.RFC = RFC;
            this.NSS = NSS;
            this.nombre = nombre;
            this.apellidoP = apellidoP;
            this.apellidoM = apellidoM;
            this.fechaNacimiento = fechaNacimiento;
            this.banco = banco;
            this.numCuenta = numCuenta;
            this.SalarioDiario = SalarioDiario;
            this.SalarioDiarioIntegrado = SalarioDiarioIntegrado;
            this.Email = Email;
            this.DireccionID = DireccionID;
            this.TelefonoID = TelefonoID;
            this.estatus = estatus;
            this.fechaIngreso = fechaIngreso;
        }
    }
}
