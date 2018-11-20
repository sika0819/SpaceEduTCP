using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System;
using System.Net.NetworkInformation;
using UnityEngine;
public static class LogTool {
    public static string Message;
    public static void Log(object msg) {
        Message = msg.ToString();
        Debug.Log(msg);
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
    public static string AssetPath = Application.persistentDataPath;
    public static string ThumbPath = "/thumb/";
    public const string ImagePath = "/asset/image/";
}
public static class GlobalName {
    public const string LoginPanel = "LoginPanel";
    public const string SelectCharPanel = "SelectCharPanel";
    public const string StudentPanel = "StudentPanel";
    public const string TeacherPanel = "TeacherPanel";
    public const string CourseId= "courseId";
    public static string MainScene = "MainScene";
    public static string SpherePlayer = "SpherePlayer";
    public static string GooglePlayer = "GooglePlayer";
    
}
public static class requestUrl {
    public const string login = @"login";
    public const string courses = @"v1/courses";
    public const string textureUrl = @"http://www.spaced.com.cn/vrclass/";
    public const string topics="topics";
   
}
