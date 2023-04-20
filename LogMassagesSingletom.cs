public class LogMassagesSingletom
{
    private static LogMassages _instance;

    public static LogMassages Instance
    {
        get {
            if (_instance == null)
                _instance = new LogMassages();
            return _instance;
        }
    }
}
