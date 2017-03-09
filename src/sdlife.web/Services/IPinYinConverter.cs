namespace sdlife.web.Services
{
    public interface IPinYinConverter
    {
        char GetCharCapitalPinYin(char ch);
        string GetCharPinYin(char ch);
        string GetStringCapitalPinYin(string str, string spliter = "");
        string GetStringPinYin(string str, string spliter = "");
    }
}