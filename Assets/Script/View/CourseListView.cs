using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

public class CourseListView : View {
    private GameObject ListObject;
    public List<CourseItemView> courseList;
    public CourseItemView view;
    public void Init(GameObject go) {
        ListObject = go;
        courseList = new List<CourseItemView>();
    }
    public void GenerateList(CourseListModel model) {

    }
}
