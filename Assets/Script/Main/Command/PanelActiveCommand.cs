using strange.extensions.command.impl;
using strange.extensions.context.api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActiveCommand : Command {
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject mainViewObject { get; set; }
    [Inject]
    public string panelData { get; set; }
    public override void Execute()
    {
        Debug.Log(panelData);
        MainView mainView = mainViewObject.GetComponent<MainView>();
        mainView.SetPanelActive(panelData);
       
    }
}
