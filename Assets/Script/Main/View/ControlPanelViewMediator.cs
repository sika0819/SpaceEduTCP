using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlPanelViewMediator : Mediator{
    [Inject]
    public ControlPanelView controllView { get; set; }
    [Inject]
    public ActivePanelSingal activePanelSingal { get; set; }
    [Inject]
    public LogoutSignal logoutSignal { get; set; }
    [Inject]
    public ILoginModel model { get; set; }//登陆数据
    EventDelegate onSettingClick;
    public override void OnRegister()
    {
        onSettingClick = new EventDelegate(HideChangeChar);
        controllView.settingPanel.AddchangeCharBtn(onSettingClick);
    }
    public void Update()
    {
        if (model.identity == Identity.Student)
        {
            if (ClientController.Instance.isStartTcp) {
                SceneManager.LoadSceneAsync("SpherePlayer");
            }
        }

    }
    private void HideChangeChar()
    {
        controllView.navBar.SetSettingToggle(false);
        activePanelSingal.Dispatch(GlobalName.SelectCharPanel);
        logoutSignal.Dispatch();
    }

    public override void OnRemove()
    {
        //Clean up listeners just as you do with EventDispatcher
        controllView.settingPanel.RemovechangeCharBtn(onSettingClick);
    }
    //private void OnLogOut()
    //{
    //    switch (model.identity)
    //    {
    //        case Identity.Teacher:
    //            ServerController.Instance.CloseServer();
    //            break;
    //        case Identity.Student:
    //            ClientController.Instance.CloseClient();
    //            break;
    //    }
    //}

}
