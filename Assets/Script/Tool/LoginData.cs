using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LoginState
{
    WaitForLogin,
    isLogined,
    LoginFailed
}
public enum Identity {
    Teacher,
    Student
}
public class LoginData  {
    public string url;
    public string name;
    public string password;
}
[Serializable]
public class UserInfo {
    public string userName;
}