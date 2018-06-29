using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelViewMediator : Mediator{
    [Inject]
    public ControlPanelView controllView { get; set; }
    [Inject]
    public ActivePanelSingal activePanelSingal { get; set; }
    EventDelegate onSettingClick;
    public override void OnRegister()
    {
        onSettingClick = new EventDelegate(HideChangeChar);
        controllView.settingPanel.AddchangeCharBtn(onSettingClick);
    }

    private void HideChangeChar()
    {
        controllView.navBar.SetSettingToggle(false);
        activePanelSingal.Dispatch(GlobalName.SelectCharPanel);
    }

    public override void OnRemove()
    {
        //Clean up listeners just as you do with EventDispatcher
        controllView.settingPanel.RemovechangeCharBtn(onSettingClick);
    }
   
}
