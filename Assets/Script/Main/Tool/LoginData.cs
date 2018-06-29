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
public class LoginData
{
    public string url { get; set; }
    public string name { get; set; }
    public string password { get; set; }
}

