namespace LAN.Core.Types.Tests
{
    public interface IConvertible<out TValue>
    {
        TValue ToValueType();
    }
}
