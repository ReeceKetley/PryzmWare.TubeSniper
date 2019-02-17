    namespace TubeSniper.Core.Domain.Models.States
{
    internal enum CheckLoginStateCode
    {
        Success,
        Captcha,
        Recovery,
        SetRecovery,
        BadCredentails,
        Timeout
    }
}