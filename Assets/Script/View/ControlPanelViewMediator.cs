using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelViewMediator : Mediator{
    [Inject]
    public ControlPanelView controllView { get; set; }
    [Inject]
    public SelectCharSingal selectCharSingal { get; set; }
    public override void OnRegister()
    {
      
    }

    public override void OnRemove()
    {
        //Clean up listeners just as you do with EventDispatcher
     
    }
   
}
