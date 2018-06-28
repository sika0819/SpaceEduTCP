using strange.extensions.command.impl;
using strange.extensions.context.api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCourseCommand : Command {
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject mainViewObject { get; set; }
    [Inject]
    public ICourseListModel model { get; set; }
    public override void Execute()
    {
       
    }
}
