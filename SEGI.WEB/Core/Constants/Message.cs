using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Core.Constants
{
    public static class Message
    {
        public const string RegularExpPhone = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        public const string Password = "Password";
        //public const string RegularExpEmail = @"[a-z0-9]+@[a-z]+\.[a-z]{2,3}";
        public const string DescriptionFName = "الاسم الأول";
        public const string DescriptionDeacription = "Description";
        public const string DescriptionTitle = "Title";
        public const string DescriptionTitleSecond = "العنوان الثاني";
        public const string DescriptionTitleThird = "العنوان الثالث";
        public const string Description = "الوصف";
        public const string DescriptionFirst = "الوصف الأول";
        public const string DescriptionSecond = "الوصف الثاني";
        public const string DescriptionThird = "الوصف الثالث";
        public const string DescriptionTrainer = "المدرب";

        public const string DescriptionName = "Name";
        public const string DescriptionLName = "الاسم الاخير";
        public const string DescriptionFullName = "الاسم كامل";

        public const string DescriptionPhone = "رقم الهاتف";
        public const string DescriptionJob = "الوظيفة";

        public const string DescriptionEmail = "البريد الالكتروني";
        public const string DescriptionCountry = "الدولة";
        public const string DescriptionSpecialization = "التخصص";
        public const string DescriptionDegree = "الدرجة العلمية";
        public const string DescriptionPassword = "كلمة المرور";
        public const string DescriptionConfirmPassword = "تأكيد كلمة المرور";
        public const string DescriptionImage = "Image ";
        public const string DescriptionCV = "السيرة الذاتية";
        public const string DescriptionSkills = "المهارات";
        public const string DescriptionWrittenBy = "المؤلف";
        public const string DescriptionMessage = "الرسالة";
        public const string DescriptionAcceptTerms = "الموافقة على سياسة الخصوصية";
        public const string DescriptionCourseType = "نوع الدورة";
        public const string DescriptionActiveType = "تفعيل الدورة";
        public const string DescriptionTotalHours = "عدد ساعات الدورة";
        public const string DescriptionPriceBeforDiscount = "السعر قبل الخصم";
        public const string DescriptionPriceAfterDiscount = "السعر بعد الخصم";
        public const string DescriptionFinalPrice = "السعر النهائي";
        public const string DescriptionUser = "المدرب";
        public const string DescriptionCategory = "Category";
        public const string DescriptionImageCover = "صورة الغلاف";
        public const string DescriptionOrderShow = "ترتيب الظهور";
        public const string DescriptionCourse = "اسم الدورة";
        public const string DescriptionViedoUrl = "الفيديو";
        public const string DescriptionSectionCourse = "القسم التابع له";
        public const string DescriptionStart = "من بداية";
        public const string DescriptionEnd = " ل نهاية";
        public const string DescriptionDiscound = " قيمة الخصم";
        public const string DescriptionPaymentType = "طريقة الدفع";


        public const string DescriptionFaceBook = " Facebook Url";
        public const string DescriptionTwitter = " x Link";
        public const string DescriptionInstagram = " Instagram Url";
        public const string DescriptionWhatsapp = " Watsapp Link";
        public const string DescriptionTelegram = " Telegram Url";



        public const int MinLength3 = 3;
        public const int MinLength6 = 6;
        public const int MaxLength100 = 100;
        public const int MaxLength200 = 200;
        public const int MaxLength400 = 400;
        public const int MaxLength800 = 800;

        public static class ErrorMessage
        {
            public const string Max100_Min3Length = "Max 100 and Min 3 Lingth";
            public const string Max100_Min6Length = "Max 100 and Min 3 Lingth.";
            public const string Max200_Min6Length = "Max 200 and Min 3 Lingth.";
            public const string Max400_Min6Length = "Max 400 and Min 3 Lingth.";
            public const string Max800_Min6Length = "Max 800 and Min 3 Lingth.";
            public const string RightPhoneEnter = "05XXXXXXX or 05XXXXXXX  الرجاء ادخال رقم موبايل صحيح";
            public const string PassAndConfirmPassNotSame = "كلمة المرور وكلمة المرور التأكيدية غير متطابقتين.";
            public const string RequiredField = "هذا الحقل مطلوب.";
        }
        public static class MessageEmail
        {
            public const string ActiveCode = "كود تفعيل الحساب";
            public const string ResetPassword = "Rest Password";

        }
    }
}
