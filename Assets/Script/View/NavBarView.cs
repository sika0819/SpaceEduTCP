using System;
using strange.extensions.mediation.impl;
using UnityEngine;

public class NavBarView : View
{
    private GameObject NavBarObejct;
    public UIButton backBtn;
    private UIButton student;
    private UIButton searchBtn;
    EventDelegate OnClickBackBtn;
    public SelectCharSingal selectCharSingal;
    public void Init(GameObject go)
    {
        NavBarObejct = go;
        backBtn = go.transform.Find("backBtn").GetComponent<UIButton>();
        OnClickBackBtn = new EventDelegate(OnBackBtnClick);
        selectCharSingal = new SelectCharSingal();
        backBtn.onClick.Add(OnClickBackBtn);
    }

    private void OnBackBtnClick()
    {
        selectCharSingal.Dispatch();
    }
}