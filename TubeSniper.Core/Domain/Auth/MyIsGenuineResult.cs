namespace TubeSniper.Core.Domain.Auth
{
	public enum MyIsGenuineResult
	{
		/// <summary>Is activated and genuine.</summary>
		Genuine = 0,

		/// <summary>Is activated and genuine and the features changed.</summary>
		GenuineFeaturesChanged = 1,

		/// <summary>Not genuine (note: use this in tandem with NotGenuineInVM).</summary>
		NotGenuine = 2,

		/// <summary>Not genuine because you're in a Virtual Machine.</summary>
		NotGenuineInVM = 3,

		/// <summary>Treat this error as a warning. That is, tell the user that the activation couldn't be validated with the servers and that they can manually recheck with the servers immediately.</summary>
		InternetError = 4
	}
}