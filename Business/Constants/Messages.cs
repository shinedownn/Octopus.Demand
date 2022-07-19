using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    /// <summary>
    /// This class was created to get rid of magic strings and write more readable code.
    /// </summary>
    public static class Messages
    {
        public static string HotelNotFound =>"Product Id'ye ait Hotel bulunamadı"; 
        public static string MainDemandClosed => "Genel talep kapalı";
        public static string ActionCountIsZero => "Aksiyon ekleyiniz"; 
        public static string HotelActionCountIsZero => "Hotel Aksiyon ekleyiniz"; 
        public static string TourActionCountIsZero => "Tur Aksiyon ekleyiniz"; 
        public static string OnRequestWaitingApproves => "Onay bekleyen Sor-Sat bulundu";
        public static string ThereOpenActions =>"Açık aksiyonlar mevcut";
        public static string Upserted => "Eksik kayıtlar eklendi, varolan kayıtlar güncellendi";
        public static string OnRequestIsOpenCannotDelete => "Açık Sor-Sat Silinemez!, önce kapatınız!";
        public static string ThereIsMainDemandActionThisActionCannotDelete => "Bu aksiyona kayıtlı genel bilgiler mevcut, o yüzden bu aksiyon silinemez!";
        public static string ThereIsHotelDemandActionThisActionCannotDelete => "Bu aksiyona kayıtlı hotel talep mevcut, o yüzden bu aksiyon silinemez!";
        public static string ThereIsTourDemandActionThisActionCannotDelete => "Bu aksiyona kayıtlı tur talep mevcut, o yüzden bu aksiyon silinemez!";
        public static string DemandIsOpenCannotDelete => "Açık Talep Silinemez!, önce kapatınız!";
        public static string ActionIsOpenCannotDelete => "Açık Aksiyon Silinemez!!!, önce kapatınız";
        public static string RecordNotFound => "Kayıt bulunamadı";
        public static string RecordAlreadyExist => "Bu kayıt daha önce girilmiş";
        public static string StringLengthMustBeGreaterThanThree => "StringLengthMustBeGreaterThanThree";
        public static string CouldNotBeVerifyCid => "CouldNotBeVerifyCid";
        public static string VerifyCid => "VerifyCid";
        public static string OperationClaimExists => "OperationClaimExists";
        public static string AuthorizationsDenied => "AuthorizationsDenied";
        public static string Added => "Eklendi";
        public static string Deleted => "Silindi";
        public static string Updated => "Güncellendi";
        public static string UserNotFound => "UserNotFound";
        public static string PasswordError => "PasswordError";
        public static string SuccessfulLogin => "SuccessfulLogin";
        public static string SendMobileCode => "SendMobileCode";
        public static string NameAlreadyExist => "Bu isimde kayıt daha önce girilmiş";
        public static string WrongCitizenId => "WrongCID";
        public static string CitizenNumber => "CID";
        public static string PasswordEmpty => "PasswordEmpty";
        public static string PasswordLength => "PasswordLength";
        public static string PasswordUppercaseLetter => "PasswordUppercaseLetter";
        public static string PasswordLowercaseLetter => "PasswordLowercaseLetter";
        public static string PasswordDigit => "PasswordDigit";
        public static string PasswordSpecialCharacter => "PasswordSpecialCharacter";
        public static string SendPassword => "SendPassword";
        public static string InvalidCode => "InvalidCode";
        public static string SmsServiceNotFound => "SmsServiceNotFound";
        public static string TrueButCellPhone => "TrueButCellPhone";
        public static string TokenProviderException => "TokenProviderException";
        public static string Unknown => "Unknown";
        public static string NewPassword => "NewPassword";
        public static string MainDemandNotFound => "Genel bilgiler bulunamadı";
        public static string TourDemandNotFound => "Tour talep bulunamadı";
        public static string HotelDemandNotFound => "Hotel talep bulunamadı";
        public static string OnRequestNotFound => "OnRequestNotFound";
    }
}
