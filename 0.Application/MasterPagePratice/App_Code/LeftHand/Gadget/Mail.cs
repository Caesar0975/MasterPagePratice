using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace LeftHand.Gadget
{
    public class Mail
    {
        private static SmtpClient _SmtpClient = null;

        public static void Initial(string SmtpUserName, string SmtpPassword, int SmtpPort, string SmtpHost, bool UseSsl)
        {
            // 設定SMTP伺服器
            _SmtpClient = new SmtpClient();
            _SmtpClient.Credentials = new System.Net.NetworkCredential(SmtpUserName, SmtpPassword);
            _SmtpClient.Port = SmtpPort;// 587;
            _SmtpClient.Host = SmtpHost;//"smtp.gmail.com";
            _SmtpClient.EnableSsl = UseSsl;//true;
        }

        public static void Send(string FromeName, string FromAddress, string ToAddress, string CcAddress, string BccAddress, string Title, string Content)
        {
            Send(FromeName, FromAddress, new List<string>() { ToAddress }, new List<string>() { CcAddress }, new List<string>() { BccAddress }, Title, Content, _SmtpClient);
        }

        public static void Send(string FromeName, string FromAddress, List<string> ToAddress, List<string> CcAddress, List<string> BccAddress, string Title, string Content)
        {
            Send(FromeName, FromAddress, ToAddress, CcAddress, BccAddress, Title, Content, _SmtpClient);
        }

        public static void Send(string FromeName, string FromAddress, string ToAddress, string CcAddress, string BccAddress, string Title, string Content, SmtpClient SmtpClient)
        {
            Send(FromeName, FromAddress, new List<string>() { ToAddress }, new List<string>() { CcAddress }, new List<string>() { BccAddress }, Title, Content, SmtpClient);
        }

        public static void Send(string FromeName, string FromAddress, List<string> ToAddress, List<string> CcAddress, List<string> BccAddress, string Title, string Content, SmtpClient SmtpClient)
        {
            ToAddress.Remove("");
            CcAddress.Remove("");
            BccAddress.Remove("");

            if (ToAddress.Count == 0 && CcAddress.Count == 0 && BccAddress.Count == 0) { throw new Exception("沒有任何收件人"); }
            if (string.IsNullOrWhiteSpace(FromeName) == true) { throw new Exception("FromeName不可為空白"); }
            if (string.IsNullOrWhiteSpace(FromAddress) == true) { throw new Exception("FromeName不可為空白"); }
            if (string.IsNullOrWhiteSpace(Title) == true) { throw new Exception("Title不可為空白"); }
            if (string.IsNullOrWhiteSpace(Content) == true) { throw new Exception("Content不可為空白"); }
            if (SmtpClient == null) { throw new Exception("SmtpClient不可為null"); }

            using (MailMessage Mail = new MailMessage())
            {
                Mail.From = new MailAddress(FromAddress, FromeName);
                foreach (string Address in ToAddress) { Mail.Bcc.Add(Address); }
                foreach (string Address in CcAddress) { Mail.CC.Add(Address); }
                foreach (string Address in BccAddress) { Mail.Bcc.Add(Address); }
                Mail.Priority = MailPriority.High;  //優先等級
                Mail.Subject = Title; //主旨
                Mail.Body = Content;  //Email 內容
                Mail.IsBodyHtml = true;  // 設定Email 內容為HTML格式
                Mail.BodyEncoding = System.Text.Encoding.UTF8;

                SmtpClient.Send(Mail);
            }
        }

        //驗證Email字串是否有效
        public static bool Check(string EmailAddress)
        {
            return Check(new List<string> { EmailAddress });
        }

        //驗證Email字串是否有效
        public static bool Check(List<string> EmailAddress)
        {
            foreach (string Address in EmailAddress)
            {
                if (IsEmail(Address) == false) { return false; }
                if (getMailServer(Address) == false) { return false; }
            }

            return true;
        }

        //驗證Email格式是否正確
        private static bool IsEmail(string EmailAddress)
        {
            return Regex.IsMatch(EmailAddress, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        //驗證Mail server是否存在
        private static bool getMailServer(string EmailAddress)
        {
            string SourceMailServer = "";

            string strDomain = EmailAddress.Split('@')[1];
            ProcessStartInfo info = new ProcessStartInfo();
            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.FileName = "nslookup";
            info.CreateNoWindow = true;
            info.Arguments = "-type=mx " + strDomain;
            Process ns = Process.Start(info);
            StreamReader sout = ns.StandardOutput;
            Regex reg = new Regex("mail exchanger = (?<mailServer>[^＼＼s]+)");
            string strResponse = "";
            while ((strResponse = sout.ReadLine()) != null)
            {
                Match amatch = reg.Match(strResponse);
                if (reg.Match(strResponse).Success)
                { SourceMailServer = amatch.Groups["mailServer"].Value; }
            }

            if (SourceMailServer == "") { return false; }

            return true;
        }
    }
}