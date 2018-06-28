using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICharpterModel {
    /// <summary>
    /// 排序
    /// </summary>
    int index { get; set; }
    /// <summary>
    /// 章节ID
    /// </summary>
    string id { get; set; }
    /// <summary>
    /// 章节标题
    /// </summary>
    string title { get; set; }
    /// <summary>
    /// 该章节的课时
    /// </summary>
    LessonModel[] lessons { get; set; }
    void convertType(Chapter chapter);
}
