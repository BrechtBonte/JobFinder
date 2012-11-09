using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

/// <summary>
/// Summary description for MailingClient
/// </summary>
public class MailingClient {

    private const string FROM = "no-reply@jobfinder.com";

    public bool SendVerification(string to, string code) {

        string message =
@"Your address %s has been registered at www.jobfinder.be.
If you do not remember registering at this website, please go to http://brechtbonte.ikdoeict.be/register.aspx?mode=cancel&code=%s.
To activate your account, pease go to http://brechtbonte.ikdoeict.be/register.aspx?mode=activate&code=%s.";

        
        return SendMail(to, "Account verification Jobfinder.be", string.Format(message, to, code, code));
    }



    private bool SendMail(string to, string subject, string message) {

        try {
            MailMessage mailObj = new MailMessage(FROM, to);

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 465;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("brechtb123@gmail.com", "Azerty123");
            smtp.Send(mailObj);

            return true;

        } catch {
            return false;
        }
    }

}