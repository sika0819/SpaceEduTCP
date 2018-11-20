using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainViewMediator : Mediator {
    [Inject]
    public MainView mainView { get; set; }
    [Inject]
    public ActivePanelSingal activePanelSingal { get; set; }
    public override void OnRegister()
    {
        OnShowLogin();
        
    }


    public void OnShowLogin() {
        activePanelSingal.Dispatch(GlobalName.LoginPanel);
    }

    public override void OnRemove()
    {
        
       

    }
}
