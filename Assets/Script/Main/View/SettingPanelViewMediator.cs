using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelViewMediator : Mediator {


    [Inject]
    public SettingPanelView settingPanel { get; set; }
    public override void OnRegister()
    {
 
        
    }

}
