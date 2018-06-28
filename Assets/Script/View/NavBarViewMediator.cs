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
    EventDelegate OnBackLogin;
    public override void OnRegister()
    {
        OnBackLogin = new EventDelegate(OnBackLoginClick);
        navBarView.AddBackDeleagate(OnBackLogin);
    }

    private void OnBackLoginClick()
    {
        activePanelSingal.Dispatch(GlobalName.SelectCharPanel);
    }

    public override void OnRemove()
    {
        navBarView.RemoveBackDelegate(OnBackLogin);
    }
}
