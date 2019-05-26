
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using com.co.uan.DMiravalle.Inventario;
using System.Web;

namespace com.co.uan.DMiravalle.Informes
{
   public  class Email : IAlerta
    {
        private MailMessage Mensaje;
        private string CuerpoCorreo = "";
        public Email() {
            Mensaje = new MailMessage();
        }
        public void EnviarNotificacion()
        {
            try
            {
                SmtpClient ServicioEmail = new SmtpClient();
                ServicioEmail.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPuerto"]);
                ServicioEmail.Host = ConfigurationManager.AppSettings["smtp"];
                ServicioEmail.EnableSsl = true;
                ServicioEmail.DeliveryMethod = SmtpDeliveryMethod.Network;
                ServicioEmail.UseDefaultCredentials = false;
                Mensaje.From = new MailAddress(ConfigurationManager.AppSettings["usuarioSmtp"]);
                Mensaje.Subject = ConfigurationManager.AppSettings["AsuntoVencidos"];
                Mensaje.Body = CuerpoCorreo;
                Mensaje.IsBodyHtml = true;
                ServicioEmail.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["usuarioSmtp"], ConfigurationManager.AppSettings["claveSmtp"]);
                ServicioEmail.Send(Mensaje);
            }
            catch (Exception ex)
            {

            }
        }
      

        public void  GenerarCuerpoHTMLCorreo(InventarioDTO ElementoVencido,string NombrePlantillaCorreo)
        {
            if (!string.IsNullOrEmpty(ElementoVencido.SedeInventario.Administrador.Correo))
            {
                Mensaje.To.Add(new MailAddress(ElementoVencido.SedeInventario.Administrador.Correo));
            } else {
                Mensaje.To.Add(new MailAddress(ConfigurationManager.AppSettings["correoGerente"]));
            }
            CuerpoCorreo = new StreamReader(HttpContext.Current.Server.MapPath( ConfigurationManager.AppSettings["RutaHTMLCorreo"].ToString()+NombrePlantillaCorreo)).ReadToEnd();
            CuerpoCorreo = CuerpoCorreo.Replace("#PRODUCTO", ElementoVencido.LoteProducto.Producto.NombreProducto);
            CuerpoCorreo = CuerpoCorreo.Replace("#LOTE", ElementoVencido.LoteProducto.CodigoLote);
            CuerpoCorreo = CuerpoCorreo.Replace("#VENCIMIENTO", ElementoVencido.LoteProducto.FechaVencimiento.ToString("dd/MM/yyyy"));
            CuerpoCorreo = CuerpoCorreo.Replace("#CANTIDAD", ElementoVencido.Cantidad.ToString());
            CuerpoCorreo = CuerpoCorreo.Replace("#SEDE", ElementoVencido.SedeInventario.NombreSede);
        }

    }
}
