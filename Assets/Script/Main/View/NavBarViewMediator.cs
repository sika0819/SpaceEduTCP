using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBarViewMediator : Mediator {
    [Inject]
    public NavBarView navBarView { get; set; }
    [Inject]
    public ActivePanelSingal activePanelSingal { get; set; }
    [Inject]
    public LogoutSignal logoutSignal { get; set; }
    [Inject]
    public ILoginModel model { get; set; }//登陆数据
    EventDelegate OnBackLogin;
    public override void OnRegister()
    {
        OnBackLogin = new EventDelegate(OnBackLoginClick);
        navBarView.AddBackDeleagate(OnBackLogin);
        logoutSignal.AddListener(OnLogOut);
    }

    private void OnLogOut()
    {
        Debug.Log(model.identity);
        switch (model.identity) {
            case Identity.Teacher:
                ServerController.Instance.CloseServer();
                break;
            case Identity.Student:
                ClientController.Instance.CloseClient();
                break;
        }
    }

    private void OnBackLoginClick()
    {
        logoutSignal.Dispatch();
        activePanelSingal.Dispatch(GlobalName.SelectCharPanel);
    }

    public override void OnRemove()
    {
        navBarView.RemoveBackDelegate(OnBackLogin);
        logoutSignal.RemoveListener(OnLogOut);
    }
}
