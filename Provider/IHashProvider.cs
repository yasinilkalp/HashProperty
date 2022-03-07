namespace HashProperty.Provider
{
    public interface IHashProvider
    {
        string Hash(string value);
        bool ValidateHash(string value, string hash);
    }
}
