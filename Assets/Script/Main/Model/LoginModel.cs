using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginModel : ILoginModel {
    public string token { get; set; }
    public long expires { get; set; }
    public string userName { get; set; }

    public Identity identity { get; set; }

    public void ConvertType(LoginJsonData loginData)
    {
        token = loginData.token;
        expires = loginData.expires;
        userName = loginData.userInfo.userName;
    }
}
