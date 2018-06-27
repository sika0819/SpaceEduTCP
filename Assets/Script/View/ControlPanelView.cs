using strange.extensions.context.impl;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelView: View
{
    private GameObject panelObejct;
    private UIPanel controlPanel;
    private GameObject setPanelObject;
    private SettingPanel settingPanel;
    private GameObject navBarObject;
    public NavBarView navBar;
    public void Init(GameObject go)
    {
        panelObejct = go;
        controlPanel = go.GetComponent<UIPanel>();
        setPanelObject = panelObejct.transform.Find("settingPanel").gameObject;
        settingPanel = setPanelObject.AddComponent<SettingPanel>();
        settingPanel.InitPanel(setPanelObject);
        navBarObject = panelObejct.transform.Find("navBar").gameObject;
        navBar = navBarObject.AddComponent<NavBarView>();
        navBar.Init(navBarObject);
        
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            controlPanel.alpha = 1;
        }
        else
        {
            controlPanel.alpha = 0;
        }
    }
}


