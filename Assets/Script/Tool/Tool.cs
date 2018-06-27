using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System;
using System.Net.NetworkInformation;
#if UNITY_EDITOR
using UnityEngine;
#endif
public static class LogTool {

    public static void Log(object msg) {
        NGUIDebug.Log(msg);
        //NGUIDebug.Clear();
    }
    
}
public static class CommenValue
{
    public static string HttpAddress = @"http://spaced.com.cn:8088/";
    public static string LoginURL = "Login";
    public static string WaitForLogin = "Please Input UserName and Password To Login";
    public static string LoginSuccess = "Login Success";
    public static string LoginFailed = "Login Failed!Please Check UserName Or Password";
}
public static class GlobalName {
    public const string LoginPanel = "LoginPanel";
    public const string SelectCharPanel = "SelectCharPanel";
    public const string StudentPanel = "StudentPanel";
    public const string TeacherPanel = "TeacherPanel";
}

