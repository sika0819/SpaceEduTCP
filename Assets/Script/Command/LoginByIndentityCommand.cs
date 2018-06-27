using strange.extensions.command.impl;
using strange.extensions.context.api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginByIndentityCommand : Command {
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject mainViewObject{ get; set; }
    
    public override void Execute()
    {
        
        MainView mainView = mainViewObject.GetComponent<MainView>();
        mainView.selectCharView.AddLoginById(OnLoginByIdentity);
    }

    private void OnLoginByIdentity(Identity identity)
    {
        Debug.Log(identity);
        switch (identity) {
            case Identity.Teacher:
                break;
            case Identity.Student:
                break;
        }
    }
}
