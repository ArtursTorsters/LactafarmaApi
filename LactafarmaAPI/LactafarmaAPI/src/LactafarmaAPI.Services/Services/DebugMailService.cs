using System.Diagnostics;
using LactafarmaAPI.Services.Interfaces;

namespace LactafarmaAPI.Services.Services
{
    public class DebugMailService : IMailService
    {
        #region Public Methods

        public void SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending Mail: To: {to} From: {from} Subject: {subject} Body: {body}");
        }

        #endregion
    }
}