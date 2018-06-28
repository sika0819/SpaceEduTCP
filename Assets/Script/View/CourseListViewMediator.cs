using strange.extensions.mediation.impl;

public class CourseListViewMediator : Mediator {
    [Inject]
    private CourseListView courseListView { get; set; }
    [Inject]
    private CourseListModel courseListModel { get; set; }
    public override void OnRegister()
    {

    }

    public override void OnRemove()
    {

    }
}
