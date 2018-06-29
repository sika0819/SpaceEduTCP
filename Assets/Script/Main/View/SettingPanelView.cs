using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelView : View
{

    private GameObject panelObject;
    private UIPanel panel;
    private GameObject settingArea;
    private UIGrid grid;
    private UIButton changeCharBtn;
    private GameObject connectBtnObj;
    private UIButton connectBtn;
    private UISlider onlyDownloadBtn;
    private UIButton settingBtn;
    private UIButton callbackBtn;
    private UIButton helpBtn;
    bool isForward = false;
    public void InitPanel(GameObject go)
    {
        panelObject = go;
        panel = go.GetComponent<UIPanel>();
        settingArea = go.transform.Find("settingArea").gameObject;
        grid = settingArea.transform.Find("Grid").GetComponent<UIGrid>();
        changeCharBtn = grid.transform.Find("changeCharBtn").GetComponent<UIButton>();
        if (grid.transform.Find("connectBtn")) {
            connectBtnObj = grid.transform.Find("connectBtn").gameObject;
            connectBtn = connectBtnObj.GetComponentInChildren<UIButton>();
        }
        if (grid.transform.Find("onlydownload"))
        {
            onlyDownloadBtn = grid.transform.Find("onlydownload").GetComponentInChildren<UISlider>(true);
        }
        settingBtn= grid.transform.Find("setting").GetComponentInChildren<UIButton>();
        callbackBtn= grid.transform.Find("callback").GetComponentInChildren<UIButton>();
        helpBtn= grid.transform.Find("help").GetComponentInChildren<UIButton>();
    }

    public void AddchangeCharBtn(EventDelegate eventDelegate)
    {
        Debug.Log("Back");
        changeCharBtn.onClick.Add(eventDelegate);
    }
    public void RemovechangeCharBtn(EventDelegate eventDelegate)
    {
        changeCharBtn.onClick.Remove(eventDelegate);
    }
}
