using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanelView:View{
    
    public LoginSignal loginSignal;
    public LoginStateChangedSignal loginStateChangedSignal;
    private GameObject panelObejct;
    private UIPanel loginPanel;
    private UIInput userNameInput;
    private UIInput passwordInput;
    private UIButton loginBtn;

    private LoginData loginData;
    private UILabel loginLabel;

    public void Init(GameObject go) {
        panelObejct = go;
        loginPanel = go.GetComponent<UIPanel>();
        loginSignal = new LoginSignal();
        loginData = new LoginData();
        loginStateChangedSignal = new LoginStateChangedSignal();
         userNameInput = panelObejct.transform.Find("UserInput").GetComponent<UIInput>();
        passwordInput = panelObejct.transform.Find("PasswordInput").GetComponent<UIInput>();
        loginBtn = panelObejct.transform.Find("loginBtn").GetComponent<UIButton>();
        loginLabel = panelObejct.transform.Find("loginInfo").GetComponent<UILabel>();
     
        loginBtn.onClick.Add(new EventDelegate(OnLoginClick));

    }

    public void ChangeLoginText(string loginText) {
        loginLabel.text = loginText;
    }
    public void OnLogin() {
        SetActive(false);

    }
    private void OnLoginClick()
    {
        loginData.url = CommenValue.LoginURL;
        loginData.name = userNameInput.value;
        loginData.password = passwordInput.value;
        loginSignal.Dispatch(loginData);
    }
    public void SetActive(bool isActive) {
        if (isActive)
        {
            loginPanel.alpha = 1;
        }
        else {
            loginPanel.alpha = 0;
        }
    }
}
