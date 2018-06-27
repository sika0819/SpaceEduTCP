using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainViewMediator : Mediator {
    [Inject]
    public MainView mainView { get; set; }
    public override void OnRegister()
    {
        mainView.DispachPanelActive(GlobalName.StudentPanel,false);
        mainView.DispachPanelActive(GlobalName.TeacherPanel, false);
    }
}
