namespace TubeSniper.Core.Domain.Auth
{
	public enum CheckNewActivationCode
	{
		InvalidProductKeyException,
		InternetException,
		PkeyMaxUsedException,
		PkeyRevokedException,
		EnableNetworkAdaptersException,
		DateTimeException,
		VirtualMachineException,
		Exception,
		IsActivated
	}
}