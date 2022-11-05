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
    /// <summary>
    /// 
    /// </summary>
    public SecureRandomNumberGenerator()
    {
        _rng = Create();
    }

    #endregion

    #region Methods
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int Next()
    {
        byte[] data = new byte[sizeof(int)];
        _rng.GetBytes(data);
        return BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public int Next(int maxValue) => Next(0, maxValue);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public int Next(int minValue, int maxValue) => minValue > maxValue
            ? throw new ArgumentOutOfRangeException(nameof(minValue))
            : (int)Math.Floor(minValue + (((double)maxValue - minValue) * NextDouble()));
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public double NextDouble()
    {
        byte[] data = new byte[sizeof(uint)];
        _rng.GetBytes(data);
        uint randUint = BitConverter.ToUInt32(data, 0);
        return randUint / (uint.MaxValue + 1.0);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public override void GetBytes(byte[] data) => _rng.GetBytes(data);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public override void GetNonZeroBytes(byte[] data) => _rng.GetNonZeroBytes(data);


    /// <summary>
    /// Protected implementation of Dispose pattern.
    /// </summary>
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
