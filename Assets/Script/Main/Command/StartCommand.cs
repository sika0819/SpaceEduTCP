using strange.extensions.command.impl;
using strange.extensions.context.api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCommand : Command {
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }
    public override void Execute()
    {
        GameObject go = contextView;
        MainView mainView = go.AddComponent<MainView>();
        mainView.init();

        //mainView.TeacherPanel.Hide();
        //mainView.StudentPanel.Hide();
    }
}
