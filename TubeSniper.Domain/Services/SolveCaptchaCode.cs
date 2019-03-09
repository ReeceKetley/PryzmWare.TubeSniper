namespace TubeSniper.Domain.Services
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