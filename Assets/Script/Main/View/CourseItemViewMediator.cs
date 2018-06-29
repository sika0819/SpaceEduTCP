using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseItemViewMediator : Mediator {
    [Inject]
    public CourseItemView courseItemView { get; set; }
    [Inject]
    public CallHttpGetIconSignal callHttpGetIconSignal { get; set; }
    [Inject]
    public ReceiveIconSignal receiveIconSignal { get; set; }
    [Inject]
    public ChangeSceneSignal changeSceneSignal { get; set; }
    public override void OnRegister()
    {
        receiveIconSignal.AddListener(OnGetIconCompete);
        courseItemView.AddThumbClick(new EventDelegate(OnClickCouse));
    }

    public void OnClickCouse()
    {
        SceneData sceneData = new SceneData();
        sceneData.SceneName = GlobalName.MainScene;
        changeSceneSignal.Dispatch(sceneData);
    }

    public void OnSetCourse(Course course)
    {
        courseItemView.SetData(course);
        callHttpGetIconSignal.Dispatch(course);
    }

    private void OnGetIconCompete(Texture2D icon)
    {
        if(icon.name==courseItemView.course.thumb)
            courseItemView.SetIcon(icon);
    }

    public override void OnRemove()
    {
        receiveIconSignal.RemoveListener(OnGetIconCompete);

    }
}
