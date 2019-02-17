namespace TubeSniper.Core.Services
{
    internal enum SolveCaptchaCode
    {
        Success,
        InvalidCaptcha,
        Recovery,
        BadUrl,
        ObjectNotFound,
        Error
    }
}