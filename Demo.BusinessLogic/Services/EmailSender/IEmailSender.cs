using Demo.DataAccess.Models.Shared;

namespace Demo.BusinessLogic.Services.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
    }
}
