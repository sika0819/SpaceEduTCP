using strange.extensions.command.impl;
using strange.extensions.context.api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginByIndentityCommand : Command {
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject mainViewObject { get; set; }
    [Inject]
    public ActivePanelSingal activePanelSingal{get; set;}
    [Inject]
    public Identity identity { get; set; }
    [Inject]
    public ILoginModel model { get; set; }//登陆数据
    public override void Execute()
    {
        
        MainView mainView = mainViewObject.GetComponent<MainView>();
        OnLoginByIdentity(identity);
        model.identity = identity;
    }

    private void OnLoginByIdentity(Identity identity)
    {
        string panelData = "";
        switch (identity) {
            case Identity.Teacher:
                panelData = GlobalName.TeacherPanel;
                ServerController.Instance.StartUdpServer();
                break;
            case Identity.Student:
                panelData= GlobalName.StudentPanel;
                ClientController.Instance.StartUdpClient();
                break;
        }
        activePanelSingal.Dispatch(panelData);
    }
}
