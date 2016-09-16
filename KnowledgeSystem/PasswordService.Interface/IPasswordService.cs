namespace CryptoLogic.Interface
{
    public interface IPasswordService
    {
        string Key { get; set; }
        string GetHash(string password, string key);
        bool VerifyPassword(string password, string key, string hash);
    }
}
