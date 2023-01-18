using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Experimental.System.Messaging;

namespace CommonLayer.Models
{
    public class MSMQ
    {
        Experimental.System.Messaging.MessageQueue messageQueue = new Experimental.System.Messaging.MessageQueue();
        public string recieverEmailAddr;
        public string receiverName;

        public void sendData2Queue(string token, string emailId, string name)
        {
            recieverEmailAddr = emailId;
            receiverName = name;
            messageQueue.Path = @".\private$\Token";
            if (!Experimental.System.Messaging.MessageQueue.Exists(messageQueue.Path))
            {
                Experimental.System.Messaging.MessageQueue.Create(messageQueue.Path);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        public void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                MailMessage mailMessage = new MailMessage();
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("vaibhavj112233@gmail.com", "rvkxjzsxioexgnrs"),
                    EnableSsl = true
                };
                mailMessage.From = new MailAddress("vaibhavj112233@gmail.com");
                mailMessage.To.Add(new MailAddress(recieverEmailAddr));
                string mailBody = $"<!DOCTYPE html>" +
                                $"<html>" +
                                $"<style>" +
                                $".blink" +
                                $"</style>" +
                                $"<body style=\"background-color:#WDBFF73;text-align:center;padding:5px;\">" +
                                $"<h1 style=\"color:#648D02;border-bottom:3px solid #84AF08;margin-top:5px\">Dear <b>{receiverName}</b></h1>\n" +
                                $"<h3 style=\"color:#8AB411;\"> For Resetting Password The Below Link Is Issued</h3>" +
                                $"<h3 style=\"color:#8AB411;\"> Click Below Link For Resetting Password</h3>" +
                                $"<a style=\"color:#00802b;text-decoration:none;font-size:20px;\" href='............'>Click Me</a>\n" +
                                $"<h3 style=\"color:#8AB411;margin-bottom:5px;\"><blink>Token Will Be Valid For Next 6 Hours</blink></h3>" +
                                $"</body>" +
                                $"</html>";
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "BookStore Reset Password Link";
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
