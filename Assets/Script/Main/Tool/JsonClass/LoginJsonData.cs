using System;

[Serializable]
public class LoginJsonData
{
    public string token;
    public long expires;
    public UserInfo userInfo;
}
[Serializable]
public class UserInfo
{
    public string userName;
}