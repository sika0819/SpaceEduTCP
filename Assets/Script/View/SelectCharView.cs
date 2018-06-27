﻿using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharView : View {
    public UIPanel selectCharPanel;
    private LoginByIdentySingal loginByIdentySingal;
    private GameObject SelectCharObject;
    private UIButton stuLoginBtn;
    private UIButton teacherLoginBtn;

    public void Init(GameObject go)
    {
        SelectCharObject = go;
        selectCharPanel = SelectCharObject.GetComponent<UIPanel>();
        stuLoginBtn = SelectCharObject.transform.Find("stuLoginBtn").GetComponent<UIButton>();
        teacherLoginBtn = SelectCharObject.transform.Find("teacherLoginBtn").GetComponent<UIButton>();
        loginByIdentySingal = new LoginByIdentySingal();
        SetActive(false);
    }
    public void ClickStudentLogin(EventDelegate OnClickStuLogin) {
        stuLoginBtn.onClick.Add(OnClickStuLogin);
    }

    public void RemoveTeacherLogin()
    {
        stuLoginBtn.onClick.RemoveRange(0, stuLoginBtn.onClick.Count);
    }

    public void RemoveStudentLogin()
    {
        teacherLoginBtn.onClick.RemoveRange(0, teacherLoginBtn.onClick.Count);
    }

    public void ClickTeacherLogin(EventDelegate OnClickTeacherLogin) {

        teacherLoginBtn.onClick.Add(OnClickTeacherLogin);
    }
    public void AddLoginById(Action<Identity> action)
    {
        loginByIdentySingal.AddListener(action);
    }
    public void RemoveLoginById(Action<Identity> action)
    {
        loginByIdentySingal.RemoveListener(action);
    }
    public void DispatchSingal(Identity identity) {
        loginByIdentySingal.Dispatch(identity);
    }
    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            selectCharPanel.alpha = 1;
        }
        else
        {
            selectCharPanel.alpha = 0;
        }
    }
}