using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseItemView : View {
    private UITexture ThumbTexture;
    private UILabel NameLabel;
    private UILabel DateLabel;
    private GameObject CourseItemObj;
    public void Init(GameObject go)
    {
        CourseItemObj = go;
        ThumbTexture = CourseItemObj.transform.Find("ThumbTexture").GetComponent<UITexture>();
        NameLabel = CourseItemObj.transform.Find("NameLabel").GetComponent<UILabel>();
        DateLabel= CourseItemObj.transform.Find("DateLabel").GetComponent<UILabel>();
    }
    public void SetData(CourseModel model) {
       
    }
}
