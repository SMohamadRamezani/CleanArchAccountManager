using App.Interfaces;

namespace App.Utils
{
    public class MessageService : IMessageService
    {
        private readonly Dictionary<string, string> _messages = new()
        {
            { "UserNotFound", "کاربر پیدا نشد." },
            { "Unauthorized", "شما مجوز دسترسی ندارید." },
            { "OldPasswordIsIncorrect","رمز عبور فعلی اشتباه است." },
            { "UserUpdated","کاربر با موفقیت ویرایش شد." },
            { "UserExists","این نام کاربری وجود دارد." },
            { "UserCreated","کاربر با موفقیت ایجاد شد." },
            { "UnAuthorizedUser","نام کاربری یا رمز عبور نادرست است" },
            { "TokenInvalid","توکن نامعتبر است" },
            { "ResetedPassword","رمز عبور بازنشانی شد." },
            {"NationalCodeNotFound","کد ملی یافت نشد" },
            {"RoleIsExists","این نقش از قبل وجود دارد" },
            {"RoleNotFound","نقش یافت نشد" }
        };

        public string GetMessage(string key)
        {
            return _messages.TryGetValue(key, out var message) ? message : "پیام نامشخص";
        }
    }

}
