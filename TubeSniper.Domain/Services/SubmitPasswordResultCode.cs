namespace TubeSniper.Domain.Services
{
    internal enum SubmitPasswordResultCode
    {
        Success,
        BadCredentails,
        Recovery,
        Failure,
        Captcha
    }
}