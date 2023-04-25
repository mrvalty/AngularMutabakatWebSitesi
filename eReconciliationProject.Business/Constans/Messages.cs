using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Constans
{
    public class Messages
    {
        public static string AddedCompany = "Şirket kaydı işlemi başarıyla yapıldı.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";

        public static string PasswordError = "Şifra hatalı.";
        public static string SuccessfulLogin = "Giriş işlemi başarılı.";
        public static string UserRegistered = "Kullanıcı kayıt işlemi başarılı.";
        public static string UserAlReadyExists = "Bu kullanıcı daha önce sisteme kaydedilmiş.";
        public static string CompanyAlreadyExists = "Bu şirket daha önce sisteme kaydedilmiş.";
        public static string MailAlreadyConfirm = "Mailiniz zaten onaylı.Tekrar gönderim işlemi yapılamadı.";

        public static string MailParameterUpdate = "Mail verileri başarıyla güncellendi.";
        public static string MailSendSuccess = "Mail başarıyla gönderildi.";

        public static string MailTemplateAdded = "Mail şablonu başarıyla kaydedildi.";
        public static string MailTemplateDeleted = "Mail şablonu başarıyla silindi.";
        public static string MailTemplateUpdated = "Mail şablonu başarıyla güncellendi.";


        public static string UserMailConfirmSuccess = "Mailiniz başarıyla onaylandı.";
        public static string SendConfirmEmailSuccess = "Onay maili tekrar gönderildi.";


        public static string MailConfirmTimeHasNotExpired = "Mail onayını 5 dakikada bir gönderebilirsiniz.";
    }
}
