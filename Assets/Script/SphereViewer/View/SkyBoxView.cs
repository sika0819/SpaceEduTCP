using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxView : View {
    public Material InsideSphereMaterial;
    public void SetSkyBox(Texture skyboxTexture)
    {
        InsideSphereMaterial.mainTexture = skyboxTexture;
    }
}
