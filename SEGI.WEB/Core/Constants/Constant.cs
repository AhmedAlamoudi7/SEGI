using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Core.Constants
{
    public static class Constant
    {

        public const string User = "User";
        public const string Email = "Email";
        public const string Image = "Image";
        public const string ImageUrl = "ImageUrl";
        public const int NumberOne = 1;
        public const int NumberFour = 4;

        public const int NumberZero = 0;
        public const int NumberTwilve = 12;
        public const int NumberTwinty = 20;
        public const int NumberFourHundred = 400;
        public const string Unknown = "UnKnown";




        public static class RolesFilter
        {
            public const string SuperAdmin = "SuperAdmin";
            public const string Admin = "Admin";
            public const string SuperAdminAndAdmin = "SuperAdmin,Admin";
            public const string User = "User";



        }
        public static class EmailTemplate
        {
            public const string userLink = "[userLink]";
            public const string userCode = "[userCode]";
            public const string Home = "[Home]";
            public const string TokenLink = "[TokenLink]";

        }
        public static class Area
        {
            public const string ControlPanel = "ControlPanel";
            public const string ProfileAccount = "ProfileAccount";

        }
        public static class FilterFileExtentions
        {
            public const string png = ".png";
            public const string jpg = ".jpg";
            public const string svg = ".svg";
            public const string jpeg = ".jpeg";
            public const string pdf = ".pdf";
            public const string docx = ".docx";
            // 1mb = 1048576
            public const int _maxAllowedPosterSize = 1048576;

        }
        public static class ViewBag
        {
            //Specializations
            public const string titleSpecializations = "Specializations";
            public const string idSpecializations = "Id";
            public const string nameSpecializations = "Name";
            public const string adviserDetailes = "adviserDetailes";
            public const string UserDetailes = "userDetailes";
            public const string titleTrainerImage = "TrainerImage";

            //Adviser
            public const string titleAdviserImage = "AdviserImage";
            public const string titleAdviserskills = "Adviserskills";
            //user
            public const string titleUsers = "Users";
            public const string titleUserImage = "UserImage";
            public const string titleSliderImage = "SliderImage";
            public const string titleTrainers = "Trainers";
            public const string titleImage = "Image";
            public const string titleIcon = "Icon";


            //Degree
            public const string titleDegree = "Degree";
            public const string idDegree = "Id";
            public const string nameDegree = "Name";
            // SectionCourse
            public const string SectionCourse = "SectionCourse";
            public const string id = "Id";
            public const string Title = "Title";
            // Category
            public const string Category = "Category";
            public const string Name = "Name";
            // SectionCourse
            public const string ApplicationUser = "ApplicationUser";
            public const string FirstName = "FirstName";
            public const string FullName = "FullName";
            public const string Email = "Email";

            // Course
            public const string Course = "Course";
            public const string UserSubscriptionCourseCount = "UserSubscriptionCourseCount";
            public const string Success = "success";
            public const string Error = "error";

        }
        public static class DefaultImage
        {
            public const string ImagefileName = "default.jpeg";
            public const string path = "./wwwroot/Metronic/" + ImagefileName;
            public const string path2 = "./wwwroot/assets/" + ImagefileName;
            public static IFormFile File = null;
            public const string Image = "Courses/Image";

        }
        public static class DefaultCourseVideo
        {
            public const string VideoUrl = "Courses/Video";

        }
        public static class Response
        {
            public const string Email = "Email";
            public const string Roles = "Roles";
            public const string UserName = "UserName";
            public const string RolesSelect = "الرجاء اختار صلاحية واحدة على الأقل";
            public const string EmailIsExist = "البريد الالكتروني موجود بالفعل.";
            public const string UserIsExist = "المستخدم موجود بالفعل.";
            public const string UserNameIsExist = "اسم المستخدم موجود بالفعل.";
            public const string MaxLimit1MB = "الحد الأقصى للملف هو 1MB ?.";
            public const string FileDenied = " .pdf and .docx فقط مسموح !?.";
            public const string ImageDenied = " .jpg , .svg and .png فقط مسموح !?.";
            public const string Success = "Add successfully.";
            public const string SuccessSubscription = "لقد تم الاشتراك بنجاح.";
            public const string SuccessChangePassword = "لقد تم تغيير كلمة المرور بنجاح.";
            public const string NofileUploaded = "No file Uploaded.";

            public const string Error = "Invalid Data.";
            public const string ErrorDateNow = "تاريخ  يجب أن يكون خلال اليوم او لاحقا بعد التوقيت الحالي";
            public const string ErrorDate = "تاريخ بداية  يجب ان تكون أقل من نهاية .";
            public const string ErrorDenied = "يوجد جلسة محجوزة بالتاريخ والوقت المدخل.";
            public const string ErrorCouponExist = "يوجد  كوبون ب نغس الكود المدخل.";
            public const string AlertSucsess = "نشكرك على تواصلك معنا سنرد قريبا على استفساراتك.";
            public const string AlertSucsessComment = "نشكرك على تقييمك سنرد قريبا على استفساراتك بعد مراجعة التقييم.";
            public const string AlertErrorCoupon = "كود الكوبون غير فعال او غير موجود .";
            public const string AlertErrorNotValidForUserCoupon = "كود الكوبون قد سبق  وتم استخدامه لمرة واحدة  .";
            public const string AlertError = "Invalid Data .Please Try Again .";
            public const string AlertSubscriptionExist = "عذرا انت مشترك مسبقا بهذا الدورة ولم ينتهي الاشتراك بعد .";
            public const string AlertErrorPhoneOrEmailEmpty = "يجب تحديث بياناتك باضافة رقم الموبايل أو البريد الالكتروني في حال عدم وجوده لاتمام عملية الاشتراك .";

            public const string AlertSuccessCoupon = "كود الكوبون  فعال.";
            public const string AlertSucsessSubscription = "تم التسجيل بنجاح.";
            public const string AlertAddSucsessِ = "تم الاضافة بنجاح.";

            public const string LoginSucsess = " تم تسجيل الدخول الى الحساب.";
            public const string LogoutSucsess = " تم تسجيل الخروج من الحساب.";
            public const string EditSucsess = " تم التعديل على بيانات المستخدم ";
            public const string Payment = " تم الحجز بنجاح يرجى تاكيد الحجز عبر الدفع  ";
            public const string NoFileUploaded = "No File Uploaded.";

        }
        public static class Actions
        {
            public const string Index = "Index";
            public const string Edit = "Edit";
            public const string Create = "Create";
        }
        public static class Links
        {
            public const string ProgileHomeIndex = "/ProfileAccount/AccountProfileHome/Index";
            public const string ConfirmEmailWithUserIdParameter = "/ProfileAccount/AccountProfileUsers/ConfirmEmail?userId=";
            public const string Login = "~/Identity/Account/Login";
            public const string Logout = "~/Identity/Account/Logout";
            public const string LogoutEndpoint = "/ProfileAccount/AccountProfileUsers/Logout";
            public const string ProfileUsersEditUser = "~/ProfileAccount/AccountProfileUsers/EditUser/";
            public const string ProfileUsersEditUser2 = "/ProfileAccount/AccountProfileUsers/EditUser/";
            public const string ProfileUsersPaymentData = "/ProfileAccount/AccountProfileUsers/Payment";
            public const string HomeClient = "/Home/Index";
            public const string ProfileUsersCallBack = "/ProfileAccount/AccountProfileUsers/CallBack";
            public const string ProfileUsersMyCourse = "/ProfileAccount/AccountProfileUsers/MyCourses/";
            public const string ProfileFinanceMyPurshase = "/ProfileAccount/AccountProfileFinance/Purshase";
            public const string ProfileFinanceCharge = "/ProfileAccount/AccountProfileFinance/ChargeBalance";

            public const string logoDark = "/SEGI_Full_Dark.png";
            public const string logoWhite = "/SEGI_Full_White.png";
            public const string logoIcon = "/SEGI_Icon.png";



        }
        public static class NortonLinkCONFIG
        {
            public const string ApiKey = "1KBJ9RE-C6049DX-GC521SM-B164RHD";
            public const string ApiKeyTest = "62ZK3Q1-6M2M3YD-PK1DTQB-0HNWT3K";
        }
        public static class CryptomusCONFIG
        {
            public const string ApiKey = " 86fc4db01afda51527e5c86233bc299deb93c71e";

        }
        public static class Database
        {
            public const string ConnectionString = "Server=MSI;Database=ZeroCool6.Db;TrustServerCertificate=true;MultipleActiveResultSets=True;Trusted_Connection=True;MultipleActiveResultSets=true";

        }
    }
}
