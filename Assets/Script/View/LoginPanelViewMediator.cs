using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanelViewMediator:Mediator{
    [Inject]
    public LoginPanelView loginView { get; set; }
    [Inject]
    public CallLoginHttpSingal callLogin { get; set; }
    [Inject]
    public ActivePanelSingal selectCharSingal { get; set; }
    
    public override void OnRegister()
    {
        loginView.loginSignal.AddListener(onLoginClick);
        loginView.loginStateChangedSignal.AddListener(OnLoginStateChanged);
    }

    public override void OnRemove()
    {
        //Clean up listeners just as you do with EventDispatcher
        loginView.loginSignal.RemoveListener(onLoginClick);
        loginView.loginStateChangedSignal.RemoveListener(OnLoginStateChanged);
    }
    private void OnLoginStateChanged(LoginState loginState)
    {
        string loginInfo = "";
       
        switch (loginState)
        {
            case LoginState.WaitForLogin:
                loginInfo = CommenValue.WaitForLogin;
                break;
            case LoginState.isLogined:
                loginInfo = CommenValue.LoginSuccess;
                loginView.OnLogin();
                selectCharSingal.Dispatch(GlobalName.SelectCharPanel);
                break;
            case LoginState.LoginFailed:
                loginInfo = CommenValue.LoginFailed;
                break;
        }
        loginView.ChangeLoginText(loginInfo);
    }
    private void onLoginClick(LoginData data)
    {
        //Dispatch a Signal. We're adding a string value (different from MyFirstContext,
        //just to show how we can Inject values into commands)
        callLogin.Dispatch(data);

    }

}
