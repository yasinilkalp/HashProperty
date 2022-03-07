namespace HashProperty.Config
{
    public class HashConfig
    {
        public HashConfig(string saltKey, bool isHash)
        {
            SaltKey = saltKey;
            IsHash = isHash;
        }
        public static bool IsHash { get; set; }
        public static string SaltKey { get; set; }


        public static HashConfig SetConfig(string saltKey, bool isHash) => new(saltKey, isHash);
    }
}