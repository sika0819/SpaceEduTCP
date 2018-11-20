using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseRequest  {
    public string thumb;
    public string textureUrl;

    public CourseRequest(string textureUrl, string thumb)
    {
        this.textureUrl = textureUrl;
        this.thumb = thumb;
    }
}
