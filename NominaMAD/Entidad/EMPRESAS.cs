using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.Entidad
{
    public class EMPRESAS
    {
        public int ID { get; set; }
        public string RazonSocial {  get; set; }
        public string Direccion {  get; set; }
        public string contacto {  get; set; }
        public string registroPatronal {  get; set; }
        public string RFC {  get; set; }
        public DateTime fechaInicio {  get; set; }

        public EMPRESAS() { }
        public EMPRESAS(int ID, string RazonSocial,string Direccion, string contacto,string registroPatronal,string RFC,DateTime FechaIni)
        {
            this.ID = ID;
            this.RazonSocial = RazonSocial;
            this.Direccion = Direccion;
            this.contacto = contacto;
            this.registroPatronal = registroPatronal;
            this.RFC =RFC ;
            this.fechaInicio = FechaIni;
        }


    }
}
