using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneCommand : Command {
    [Inject]
    public SceneData sceneData { get; set; }
    public override void Execute()
    {
        SceneManager.LoadSceneAsync(sceneData.SceneName);
    }
}
