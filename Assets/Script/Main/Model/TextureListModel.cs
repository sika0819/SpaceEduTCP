using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureListModel : ITextureListModel
{
    public List<Texture> textureList { get; set; }
    public TextureListModel() {
        textureList = new List<Texture>();
    }

    public void Add(Texture texture2D)
    {
        textureList.Add(texture2D);
    }
}
