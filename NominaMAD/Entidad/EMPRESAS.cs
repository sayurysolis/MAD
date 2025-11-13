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
        public string nombre { get; set; }
        public string RazonSocial {  get; set; }
        public string DomicilioFiscal {  get; set; }
        public string contacto {  get; set; }
        public string registroPatronal {  get; set; }
        public string RFC {  get; set; }
        public DateTime fechaInicio {  get; set; }
        public bool Estatus { get; set; }

        public EMPRESAS() { }
        public EMPRESAS(int ID,string nombre, string RazonSocial,string DomicilioFiscal, string contacto,string registroPatronal,string RFC,DateTime FechaIni,bool estatus)
        {
            this.ID = ID;
            this.nombre = nombre;
            this.RazonSocial = RazonSocial;
            this.DomicilioFiscal = DomicilioFiscal;
            this.contacto = contacto;
            this.registroPatronal = registroPatronal;
            this.RFC =RFC ;
            this.fechaInicio = FechaIni;
            this.Estatus = estatus;
        }


    }
}
