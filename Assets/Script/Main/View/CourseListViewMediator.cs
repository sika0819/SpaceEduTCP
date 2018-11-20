using System;
using strange.extensions.mediation.impl;

public class CourseListViewMediator : Mediator {
    [Inject]
    public CourseListView courseListView { get; set; }
    [Inject]
    public GenerateCourseListSignal generateCourseListSignal { get; set; }
    [Inject]
    public LogoutSignal logoutSignal { get; set; }
    public override void OnRegister()
    {
        generateCourseListSignal.AddListener(OnGenerateList);
        logoutSignal.AddListener(OnClearCourse);
    }

    private void OnClearCourse()
    {
        courseListView.ClearCourseList();
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
