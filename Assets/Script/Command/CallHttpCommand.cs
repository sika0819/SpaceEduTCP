﻿/// An Asynchronous Command
/// ============================
/// This demonstrates how to use a Command to perform an asynchronous action;
/// for example, if you need to call a web service. The two most important lines
/// are the Retain() and Release() calls.

using System;
using System.Collections;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

//Again, we extend Command, not EventCommand
public class CallHttpCommand : Command
{

    [Inject]
    public ILoginModel model { get; set; }//登陆数据

    [Inject]
    public IHttpLoginService service { get; set; }//登陆服务

    //Injecting this string just to demonstrate that you can do this with Signals
    [Inject]
    public LoginData loginData { get; set; }

    public CallHttpCommand()
    {
        
    }

    public override void Execute()
    {
        Retain();

        Debug.LogWarning("CallHttpServiceCommand is Executing and received a value via Signal: " + loginData.url);

        //IExampleService defines fulfillSignal as part of its API
        service.fulfillSignal.AddListener(onComplete);
        service.Request(loginData.url, loginData.name, loginData.password, null);
    }



    //The payload is now a type-safe string
    private void onComplete(string json)
    {
        service.fulfillSignal.RemoveListener(onComplete);
        LoginModel loginModel = JsonUtility.FromJson<LoginModel>(json);
        Release();
    }

}

