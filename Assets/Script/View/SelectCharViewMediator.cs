using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharViewMediator : Mediator {
   

    [Inject]
    public SelectCharView selectCharView { get; set; }
    public override void OnRegister()
    {
        
        selectCharView.ClickStudentLogin(new EventDelegate(OnStudentLogin));
        selectCharView.ClickTeacherLogin(new EventDelegate(OnTeacherLogin));
        
    }
    
    private void OnTeacherLogin()
    {
        selectCharView.DispatchSingal(Identity.Teacher);
    }

    private void OnStudentLogin()
    {
        selectCharView.DispatchSingal(Identity.Student);
    }
    public override void OnRemove()
    {
        //Clean up listeners just as you do with EventDispatcher
        selectCharView.RemoveStudentLogin();
        selectCharView.RemoveTeacherLogin();
    }
}
