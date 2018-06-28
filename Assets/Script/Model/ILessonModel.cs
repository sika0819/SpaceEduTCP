using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILessonModel  {
    /// <summary>
    /// 顺序
    /// </summary>
    int index { get; set; }
    /// <summary>
    /// 课时ID
    /// </summary>
    string id { get; set; }
    /// <summary>
    /// 课时标题
    /// </summary>
    string title { get; set; }
    void ConvertType(Lesson lesson);
}
