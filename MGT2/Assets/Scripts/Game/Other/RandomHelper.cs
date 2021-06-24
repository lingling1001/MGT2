public class RandomHelper
{
    private static int _randomSpeed;
    private static System.Random _random;

    public static void ChangeRandomSpeed(int speed)
    {
        if (_randomSpeed != speed || _random == null)
        {
            _random = new System.Random(speed);
            _randomSpeed = speed;
        }
    }
    public static System.Random GetRandom()
    {
        if (_random == null)
        {
            ChangeRandomSpeed((int)System.DateTime.UtcNow.Ticks);
        }
        return _random;
    }

}

