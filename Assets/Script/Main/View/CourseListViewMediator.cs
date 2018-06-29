using System;
using strange.extensions.mediation.impl;

public class CourseListViewMediator : Mediator {
    [Inject]
    public CourseListView courseListView { get; set; }
    [Inject]
    public GenerateCourseListSignal generateCourseListSignal { get; set; }
    public override void OnRegister()
    {
        generateCourseListSignal.AddListener(OnGenerateList);

    }

    private void OnGenerateList(CourseList courses)
    {
        courseListView.GenerateList(courses);
        
    }

    public override void OnRemove()
    {
        generateCourseListSignal.RemoveListener(OnGenerateList);
    }
}
