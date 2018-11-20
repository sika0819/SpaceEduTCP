using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMangerViewMediator : Mediator {
    [Inject]
    public UIMangerView mangerView { get; set; }
    public override void OnRegister()
    {
        base.OnRegister();
     
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
