using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    string filePath;
    Course course;
    public override void OnRegister()
    {
        receiveIconSignal.AddListener(OnGetIconCompete);
        courseItemView.AddThumbClick(new EventDelegate(OnClickCouse));
        filePath = CommenValue.AssetPath;
    }

    public void OnClickCouse()
    {
        SceneData sceneData = new SceneData();
        sceneData.SceneName = GlobalName.SpherePlayer;
        PlayerPrefs.SetString(GlobalName.CourseId, course.id);
        changeSceneSignal.Dispatch(sceneData);
    }

    public void OnSetCourse(Course course)
    {
        this.course = course;
        courseItemView.SetData(course);
        Debug.Log("Set course");
        CourseRequest courseRequest=null;
        if (!File.Exists(filePath + "/" + course.thumb))
        {
            courseRequest = new CourseRequest(requestUrl.textureUrl, course.thumb);
            callHttpGetIconSignal.Dispatch(courseRequest);
        }
        else
        {
            courseRequest = new CourseRequest("file:///" + filePath , course.thumb);
            callHttpGetIconSignal.Dispatch(courseRequest);
        }
    }

    private void OnGetIconCompete(Texture icon)
    {
        Debug.Log("getIconCompete");
        if(icon.name==courseItemView.course.thumb)
            courseItemView.SetIcon(icon);
    }
    public void DestoryCourse() {
        courseItemView.DestoryCourse();
    }
    public override void OnRemove()
    {
        receiveIconSignal.RemoveListener(OnGetIconCompete);

    }
}
