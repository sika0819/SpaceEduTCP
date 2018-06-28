using System;
using strange.extensions.mediation.impl;
using UnityEngine;

public class NavBarView : View
{
    private GameObject NavBarObejct;
    private UIToggle SettingBtn;
    private UIButton backBtn;
    private UIButton student;
    private UIButton searchBtn;
    public void Init(GameObject go)
    {
        NavBarObejct = go;
        backBtn = go.transform.Find("backBtn").GetComponent<UIButton>();
        SettingBtn= go.transform.Find("SettingBtn").GetComponent<UIToggle>();
    }

    public void AddSettingDelegate(EventDelegate action)
    {
        SettingBtn.onChange.Add(action);
    }
    public void SetSettingToggle(bool value) {
        SettingBtn.value=value;
    }
    public void RemoteSettingDelegate(EventDelegate action)
    {
        SettingBtn.onChange.Remove(action);
    }

    public void AddBackDeleagate(EventDelegate action)
    {
        backBtn.onClick.Add(action);
    }
    public void RemoveBackDelegate(EventDelegate action)
    {
        backBtn.onClick.Remove(action);
    }
}