namespace ContentServices
{
    public interface IAppService
    {
        string NegativeWordCounter(string input);
        string UpdateBadWordList(string input);
        string NegativeWordFilter(string input);
    }
}