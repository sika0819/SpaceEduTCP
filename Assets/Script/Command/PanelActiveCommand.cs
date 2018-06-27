using strange.extensions.command.impl;
using strange.extensions.context.api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActiveCommand : Command {
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject mainViewObject { get; set; }
    [Inject]
    public PanelData panelData { get; set; }
    public override void Execute()
    {

        MainView mainView = mainViewObject.GetComponent<MainView>();
        mainView.SetPanelActive(panelData);
    }
}
