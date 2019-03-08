    namespace TubeSniper.Core.Domain.Models.States.V0
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