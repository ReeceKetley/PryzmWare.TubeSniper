namespace TubeSniper.Core.Domain.Auth
{
	public class SelectorPayload
	{
		public string CloudRailKey { get; set; }
		public string CloudRailSecret { get; set; }
		public string InputTypePasswordEqFocus { get; set; }
		public string ImgSrcCaptchaAttrSrc { get; set; }
		public string InputTypeTextEqFocus { get; set; }
		public string ImgSrcCaptchaLength { get; set; }
		public string LoginEmailidentifierid { get; set; }
		public string SigninV2Identifier { get; set; }
		public string TypeofJqueryUndefinedInputTypePasswordAttrAriaInvalid { get; set; }
		public string SigninV2SlPwd { get; set; }

		public static SelectorPayload GetDefault()
		{
			return new SelectorPayload { };
		}
	}
}
