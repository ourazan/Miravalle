
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using com.co.uan.DMiravalle.Inventario;
using System.Web;
using System.Text;

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
      

        public void  GenerarCuerpoHTMLCorreo(List<InventarioDTO> ElementoVencidos,string NombrePlantillaCorreo)
        {
            if (!string.IsNullOrEmpty(ElementoVencidos[0].SedeInventario.Administrador.Correo))
            {
                Mensaje.To.Add(new MailAddress(ElementoVencidos[0].SedeInventario.Administrador.Correo));
            } else {
                Mensaje.To.Add(new MailAddress(ConfigurationManager.AppSettings["correoGerente"]));
            }
            CuerpoCorreo = new StreamReader(HttpContext.Current.Server.MapPath( ConfigurationManager.AppSettings["RutaHTMLCorreo"].ToString()+NombrePlantillaCorreo)).ReadToEnd();
            CuerpoCorreo = CuerpoCorreo.Replace("#SEDE", ElementoVencidos[0].SedeInventario.NombreSede);
            CuerpoCorreo = CuerpoCorreo.Replace("#DETALLE", GenerarDetalleProductos(ElementoVencidos));
        }


        private string GenerarDetalleProductos(List<InventarioDTO> ElementoVencidos) {
            StringBuilder Detalle = new StringBuilder();
            Detalle.AppendLine("<table style='width: 400px; padding: 10px; margin:0 auto; border-collapse: collapse;  border-radius: 10px; color: white; text-align: center;' border='1' ><tr><td>Producto</td><td>Código Lote</td><td>Fecha Vencimiento</td><td>Cantidad</td></tr>");
            foreach (InventarioDTO Elemento in ElementoVencidos)
            {
                Detalle.AppendLine("<tr><td>"+ Elemento.LoteProducto.Producto.NombreProducto 
                    + "</td><td>"+ Elemento.LoteProducto.CodigoLote 
                    + "</td><td>"+ Elemento.LoteProducto.FechaVencimiento.ToString("dd/MM/yyyy") 
                    +"</td><td>"+ Elemento.Cantidad.ToString() + "</td></tr>");
            }
            Detalle.AppendLine("</table>");
            return Detalle.ToString();
        }

    }
}
