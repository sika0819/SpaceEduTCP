using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharpterModel:ICharpterModel {
    /// <summary>
    /// 排序
    /// </summary>
    public int index { get; set; }
    /// <summary>
    /// 章节ID
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// 章节标题
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 该章节的课时
    /// </summary>
    public LessonModel[] lessons { get; set; }

    public void convertType(Chapter chapter)
    {
        index = chapter.index;
        title = chapter.title;
        id = chapter.id;
        lessons = new LessonModel[chapter.lessons.Length];
        for (int i = 0; i < chapter.lessons.Length; i++) {
            lessons[i].ConvertType(chapter.lessons[i]);
        }
    }
}
