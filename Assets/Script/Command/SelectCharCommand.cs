using strange.extensions.command.impl;
using strange.extensions.context.api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharCommand : Command {
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject mainViewObject { get; set; }
    MainView mainView;
    public override void Execute()
    {
        mainView = mainViewObject.GetComponent<MainView>();
        mainView.selectCharView.SetActive(true);
        mainView.selectCharView.AddLoginById(OnSelectIdentity);
    }

    private void OnSelectIdentity(Identity identity)
    {
        mainView.DispachPanelActive(GlobalName.SelectCharPanel, false);
        switch (identity) {
            case Identity.Teacher:
                mainView.DispachPanelActive(GlobalName.TeacherPanel, true);
                break;
            case Identity.Student:
                mainView.DispachPanelActive(GlobalName.StudentPanel, true);
                break;
        }
    }
}
