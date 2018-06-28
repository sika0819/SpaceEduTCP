public interface ILoginModel  {
    string token { get; set; }
    long expires { get; set; }
    string userName { get; set; }
    void ConvertType(LoginJsonData loginData);
}
