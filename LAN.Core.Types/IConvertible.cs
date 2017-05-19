namespace LAN.Core.Types
{
	public interface IConvertible<out T>
	{
		T ToValueType();
	}
}
