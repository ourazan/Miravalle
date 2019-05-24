
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using com.co.uan.DMiravalle.Inventario;

namespace com.co.uan.DMiravalle.Informes
{
   public  class Email : IAlerta
    {
        private MailMessage Mensaje;
        private string CuerpoCorreo = "";
        public Email() {
            Mensaje = new MailMessage();
        }
        public void  EnviarNotificacion()
        {
            try
            {
            SmtpClient ServicioEmail = new SmtpClient();
            ServicioEmail.Port = 25;
            ServicioEmail.DeliveryMethod = SmtpDeliveryMethod.Network;
            ServicioEmail.UseDefaultCredentials = false;
            ServicioEmail.Host = ConfigurationManager.AppSettings["smtp"];// "smtp.gmail.com";
            Mensaje.From=  new MailAddress ( ConfigurationManager.AppSettings["usuarioSmtp"]);
            Mensaje.Subject = ConfigurationManager.AppSettings["AsuntoVencidos"];
            ServicioEmail.Credentials = new  System.Net.NetworkCredential(ConfigurationManager.AppSettings["userSmtp"], ConfigurationManager.AppSettings["claveSmtp"]);
            ServicioEmail.Send(Mensaje);
            }
            catch (Exception ex)
            {
                
            }
        }
      

        public void  GenerarCuerpoHTMLCorreo(Inventario.Inventario ElementoVencido,string NombrePlantillaCorreo)
        {
            Mensaje.To.Add(new MailAddress(ElementoVencido.SedeInventario.Administrador.Correo));
            CuerpoCorreo = new StreamReader(ConfigurationManager.AppSettings["RutaHTMLCorreo"].ToString()).ReadToEnd();
            CuerpoCorreo = CuerpoCorreo.Replace("#PRODUCTO", ElementoVencido.LoteProducto.Producto.NombreProducto);
            CuerpoCorreo = CuerpoCorreo.Replace("#LOTE", ElementoVencido.LoteProducto.CodigoLote);
            CuerpoCorreo = CuerpoCorreo.Replace("#VENCIMIENTO", ElementoVencido.LoteProducto.FechaVencimiento.ToString("dd/MM/yyyy"));
            CuerpoCorreo = CuerpoCorreo.Replace("#CANTIDAD", ElementoVencido.Cantidad.ToString());
            CuerpoCorreo = CuerpoCorreo.Replace("#SEDE", ElementoVencido.SedeInventario.NombreSede);
        }

    }
}
