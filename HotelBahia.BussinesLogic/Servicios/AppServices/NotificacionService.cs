using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.BussinesLogic.Domain;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace HotelBahia.BussinesLogic.Servicios.AppServices
{
    public class NotificacionService
    {
        string Host = "https://sistemahoteleria.azurewebsites.net";
        public NotificacionService()
        {
            
        }
        public void Notificar(Empleado empleado, Habitacion habitacion, ActividadTipo actividadTipo)
        {
            try
            {
                EnviarMail(MensajeMail(empleado, habitacion, actividadTipo));
            }
            catch (Exception)
            {
                return;
            }
            
        }

        private MailMessage MensajeMail(Empleado empleado, Habitacion habitacion, ActividadTipo actividadTipo)
        {
            MailMessage mensaje=new MailMessage();
            mensaje.To.Add(empleado.Correo);
            mensaje.IsBodyHtml=true;
            mensaje.From=new MailAddress("adm_hotel_bahia@hotmail.com", "Administración");
            string mensajeinterno = "";
            string url = "";
            switch (actividadTipo)
            {
                case ActividadTipo.Limpieza:
                    mensaje.Subject="Notificación de Limpieza";
                    mensajeinterno = string.Format(@"Por favor, acudir a la habitación {0} para empezar el proceso de limpieza.<br>
                    Para realizar la limpieza de clic en el boton", habitacion.Numero);
                    url = Host + "Limpieza/" + habitacion.HabitacionId;
                    mensaje.Body=notificacionHMTL(empleado.Nombres, mensajeinterno, url, "Realizar Limpieza");
                    break;
                case ActividadTipo.Mantenimieto:
                   
                    break;
                case ActividadTipo.Supervision:
                    mensaje.Subject = "Notificación de Supervision";
                    url = Host + "Supervision/" + habitacion.HabitacionId;
                    mensajeinterno = string.Format(@"Se ha ejecutado el proceso de limpieza en la habitacion {0}<br> Para supervisar de clic en el boton", habitacion.Numero);
                    mensaje.Body = notificacionHMTL(empleado.Nombres, mensajeinterno, url, "Supervisar Habitacion");
                    break;
                default:
                    break;
            }
            return mensaje;
        }

        public bool EnviarMail(MailMessage mensaje)
        {
            try
            {
                SmtpClient servidor=new SmtpClient();
                servidor.Port=587;
                servidor.Host="smtp.live.com";
                NetworkCredential credenciales=new NetworkCredential();
                credenciales.UserName="adm_hotel_bahia@hotmail.com";
                credenciales.Password="prueba12345";
                servidor.Credentials=credenciales;
                servidor.EnableSsl=true;
                servidor.SendMailAsync(mensaje);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private string notificacionHMTL(string nombre, string contentInterno, string link, string nameBoton)
        {
            return @"
<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional //EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>

<html xmlns='http://www.w3.org/1999/xhtml' xmlns: o='urn:schemas-microsoft-com:office:office' xmlns: v='urn:schemas-microsoft-com:vml' >
       <head >
      
     </head>
<body style='margin: 0; padding: 0; -webkit-text-size-adjust: 100%; background-color: #e4ecf1;'>
 <table bgcolor='#e4ecf1' cellpadding='0' cellspacing='0' class='nl-container' role='presentation' style='table-layout: fixed; vertical-align: top; min-width: 320px; Margin: 0 auto; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #e4ecf1; width: 100%;' valign='top' width='100%'>
        <tbody>
            <tr style='vertical-align: top;' valign='top'>
                <td style='word-break: break-word; vertical-align: top; border-collapse: collapse;' valign='top'>
                   
                    <div style='background-color:transparent;'>
                        <div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 600px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: transparent;;'>
                            <div style='border-collapse: collapse;display: table;width: 100%;background-color:transparent;'>
                                
                                <div class='col num12' style='min-width: 320px; max-width: 600px; display: table-cell; vertical-align: top;;'>
                                    <div style='width:100% !important;'>
                                        
                                        <div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:5px; padding-bottom:0px; padding-right: 0px; padding-left: 0px;'>
                                           
                                            <div align='center' class='img-container center autowidth fullwidth' style='padding-right: 0px;padding-left: 0px; background:#179dc7'>
                                               
                                                <div style='font-size: 12px; line-height: 14px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; color: #FFFFFF;'>
                                                    <p style='font-size: 14px; line-height: 33px; text-align: center; margin: 0; padding:20px'><span style='font-size: 28px;'><strong><span style='font-size: 28px; line-height: 33px; color: #ffffff'>Hotel Bahía Informa</span></p>
                                                </div>
                                                
                                            </div>
                                            
                                        </div>
                                    </div>
                                </div>
                               
                            </div>
                        </div>
                    </div>
                    <div style='background-color:transparent;'>
                        <div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 600px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: #FFFFFF;;'>
                            <div style='border-collapse: collapse;display: table;width: 100%;background-color:#FFFFFF;'>
                               
                                <div class='col num12' style='min-width: 320px; max-width: 600px; display: table-cell; vertical-align: top;;'>
                                    <div style='width:100% !important;'>
                                       
                                        <div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:20px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;'>
                                             <div style='color:#0D0D0D;font-family:'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif;line-height:120%;padding-top:10px;padding-right:10px;padding-bottom:10px;padding-left:10px;'>
                                                <div style='font-size: 12px; line-height: 14px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; color: #0D0D0D;'>
                                                    <p style='font-size: 14px; line-height: 33px; text-align: center; margin: 0;'><span style='font-size: 28px;'><strong><span style='line-height: 33px; font-size: 28px;'>Buen día, " + nombre + @"</span></strong>
                                                        </span>
                                                    </p>
                                                </div>
                                            </div>
                                           
                                            <div align='center' class='img-container center autowidth'>
                                                </div>
                                            <div style='color:#0D0D0D;font-family:'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif;line-height:150%;padding-top:20px;padding-right:10px;padding-bottom:10px;padding-left:10px;'>
                                                <div style='font-size: 12px; line-height: 18px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; color: #0D0D0D;'>
                                                    <p style='font-size: 14px; line-height: 21px; text-align: center; margin: 0;'>" + contentInterno + @"</p>
                                                </div>
                                            </div>
                                            
                                            <div align='center' class='button-container' style='padding-top:25px;padding-right:10px;padding-bottom:10px;padding-left:10px;'>
                                                <a href='" + link + @"' style='padding:10px;text-decoration: none; display: inline-block; color: #ffffff; background:#a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;' target='_blank'>
													<span style='font-size: 16px; line-height: 25px;'>" + nameBoton + @"</span>
													</a>
                                            </div>
                                            <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'>
                                                <tbody>
                                                    <tr style='vertical-align: top;' valign='top'>
                                                        <td class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 30px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px; border-collapse: collapse;' valign='top'>
                                                            <table align='center' border='0' cellpadding='0' cellspacing='0' class='divider_content' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; border-top: 0px solid transparent;' valign='top' width='100%'>
                                                                <tbody>
                                                                    <tr style='vertical-align: top;' valign='top'>
                                                                        <td style='word-break: break-word; vertical-align: top; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; border-collapse: collapse;' valign='top'><span></span></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            
                                        </div>
                                       
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                    <div style='background-color:transparent;'>
                        <div class='block-grid' style='Margin: 0 auto; min-width: 320px; max-width: 600px; overflow-wrap: break-word; word-wrap: break-word; word-break: break-word; background-color: transparent;;'>
                            <div style='border-collapse: collapse;display: table;width: 100%;background-color:transparent;'>
                              
                                <div class='col num12' style='min-width: 320px; max-width: 600px; display: table-cell; vertical-align: top;;'>
                                    <div style='width:100% !important;'>
                                        
                                        <div style='border-top:0px solid transparent; border-left:0px solid transparent; border-bottom:0px solid transparent; border-right:0px solid transparent; padding-top:0px; padding-bottom:5px; padding-right: 0px; padding-left: 0px;'>
                                           
                                            <div align='center' class='img-container center autowidth fullwidth' style='padding-right: 0px;padding-left: 0px; background:#555555'>
                                                <br>
                                               
                                            </div>
                                            <table border='0' cellpadding='0' cellspacing='0' class='divider' role='presentation' style='table-layout: fixed; vertical-align: top; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;' valign='top' width='100%'>
                                                <tbody>
                                                    <tr style='vertical-align: top;' valign='top'>
                                                        <td height='79' valign='top' class='divider_inner' style='word-break: break-word; vertical-align: top; min-width: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 30px; padding-right: 30px; padding-bottom: 30px; padding-left: 30px; border-collapse: collapse;'>&nbsp;</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                           
                                        </div>
                                        
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                   
                </td>
            </tr>
        </tbody>
    </table>
    

</body>
</html>";
        }
    }
}
