namespace LactafarmaAPI.Services.Interfaces
{
    public interface IMailService
    {
        #region Public Methods

        void SendMail(string to, string from, string subject, string body);

        #endregion
    }
}