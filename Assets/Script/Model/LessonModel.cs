using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LessonModel : ILessonModel {
    /// <summary>
    /// 顺序
    /// </summary>
    public int index { get; set; }
    /// <summary>
    /// 课时ID
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// 课时标题
    /// </summary>
    public string title { get; set; }

    public void ConvertType(Lesson lesson)
    {
        index = lesson.index;
        id = lesson.id;
        title = lesson.title;
    }
}
