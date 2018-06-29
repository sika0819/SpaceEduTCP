using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharViewMediator : Mediator {


    [Inject]
    public SelectCharView selectCharView { get; set; }
    [Inject]
    public LoginByIdentySingal loginByIdentySingal{get;set;}
    [Inject]
    public CallGetHttpSingal callGetHttpSingal { get; set; }
    public override void OnRegister()
    {
        
        selectCharView.ClickStudentLogin(new EventDelegate(OnStudentLogin));
        selectCharView.ClickTeacherLogin(new EventDelegate(OnTeacherLogin));
        
    }
    
    private void OnTeacherLogin()
    {
        loginByIdentySingal.Dispatch(Identity.Teacher);
        callGetHttpSingal.Dispatch(requestUrl.courses);
    }

    private void OnStudentLogin()
    {
        loginByIdentySingal.Dispatch(Identity.Student);
    }
    public override void OnRemove()
    {
        //Clean up listeners just as you do with EventDispatcher
        selectCharView.RemoveStudentLogin();
        selectCharView.RemoveTeacherLogin();
    }
}
