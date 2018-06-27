using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel:View  {
    public UIButton changeCharBtn;
    private GameObject panelObject;
    private UIPanel panel;
    private GameObject settingArea;
    private UIGrid grid;
    public void InitPanel(GameObject go) {
        panelObject = go;
        panel = go.GetComponent<UIPanel>();
        settingArea = go.transform.Find("settingArea").gameObject;
        grid = settingArea.transform.Find("Grid").GetComponent<UIGrid>();
        changeCharBtn = grid.transform.Find("changeCharBtn").GetComponent<UIButton>();
    }

}
