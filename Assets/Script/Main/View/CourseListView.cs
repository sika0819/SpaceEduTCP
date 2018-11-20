using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

public class CourseListView : View {
    private GameObject ListObject;
    public List<CourseItemViewMediator> courseList;
    private UIGrid grid;
    private GameObject itemViewObject;
    
    public void Init(GameObject go) {
        ListObject = go;
        courseList = new List<CourseItemViewMediator>();
        grid = ListObject.transform.Find("UIGrid").GetComponent<UIGrid>();
        itemViewObject = grid.transform.Find("CourseItem").gameObject;
      
    }
    public void GenerateList(CourseList model) {
        for (int i = 0; i < model.courses.Count; i++) {
            GameObject item = Instantiate<GameObject>(itemViewObject,grid.transform);
            CourseItemView view = item.AddComponent<CourseItemView>();
            view.Init(item);
            item.SetActive(true);
            view.enabled = true;
            grid.AddChild(item.transform);
            CourseItemViewMediator courseItemViewMediator = item.GetComponent<CourseItemViewMediator>();
            courseItemViewMediator.OnSetCourse(model.courses[i]);
            courseList.Add(courseItemViewMediator);
        }
    }
    public void ClearCourseList() {
        for (int i = courseList.Count-1; i >=0; i--) {
            courseList[i].DestoryCourse();
            courseList.RemoveAt(i);
        }
    }
}
