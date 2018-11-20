using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITextureListModel {
    List<Texture> textureList { get; set; }
    void Add(Texture texture2D);
}
