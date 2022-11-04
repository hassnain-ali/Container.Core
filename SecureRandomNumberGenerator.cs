namespace Container.Core;

/// <summary>
///  Represents the class implementation of cryptographic random number generator derive
/// </summary>
public partial class SecureRandomNumberGenerator : RandomNumberGenerator
{
    #region Field

    private bool _disposed = false;
    private readonly RandomNumberGenerator _rng;

    #endregion

    #region Ctor

    public SecureRandomNumberGenerator()
    {
        _rng = Create();
    }

    #endregion

    #region Methods

    public int Next()
    {
        byte[] data = new byte[sizeof(int)];
        _rng.GetBytes(data);
        return BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
    }

    public int Next(int maxValue) => Next(0, maxValue);

    public int Next(int minValue, int maxValue) => minValue > maxValue
            ? throw new ArgumentOutOfRangeException(nameof(minValue))
            : (int)Math.Floor(minValue + (((double)maxValue - minValue) * NextDouble()));

    public double NextDouble()
    {
        byte[] data = new byte[sizeof(uint)];
        _rng.GetBytes(data);
        uint randUint = BitConverter.ToUInt32(data, 0);
        return randUint / (uint.MaxValue + 1.0);
    }

    public override void GetBytes(byte[] data) => _rng.GetBytes(data);

    public override void GetNonZeroBytes(byte[] data) => _rng.GetNonZeroBytes(data);


    // Protected implementation of Dispose pattern.
    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _rng?.Dispose();
        }

        _disposed = true;
    }

    #endregion
}
