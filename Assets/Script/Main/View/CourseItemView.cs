using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseItemView : View {
    private UITexture ThumbTexture;
    private UILabel NameLabel;
    private UILabel DateLabel;
    private GameObject CourseItemObj;
    public UIButton ThumbBtn;
    public Course course;
    public void Init(GameObject go)
    {
        CourseItemObj = go;
        ThumbBtn = CourseItemObj.transform.GetComponentInChildren<UIButton>();
        ThumbTexture = CourseItemObj.transform.Find("ThumbTexture").GetComponent<UITexture>();
        NameLabel = CourseItemObj.transform.Find("NameLabel").GetComponent<UILabel>();
        DateLabel= CourseItemObj.transform.Find("DateLabel").GetComponent<UILabel>();
        
    }

    public void AddThumbClick(EventDelegate eventDelegate) {
        ThumbBtn.onClick.Add(eventDelegate);
    }
    public void RemoveThumbClick(EventDelegate eventDelegate)
    {
        ThumbBtn.onClick.Remove(eventDelegate);
    }
    public void SetData(Course model) {
        course = model;
        NameLabel.text = model.name;
        DateLabel.text = model.updated;
        
    }
    public void SetIcon(Texture2D icon) {
        ThumbTexture.mainTexture = icon;
    }
}
