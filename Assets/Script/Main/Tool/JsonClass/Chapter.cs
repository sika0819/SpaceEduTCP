using System;
/// <summary>
/// 章节
/// </summary>
[Serializable]
public class Chapter
{
    /// <summary>
    /// 排序
    /// </summary>
    public int index;
    /// <summary>
    /// 章节ID
    /// </summary>
    public string id;
    /// <summary>
    /// 章节标题
    /// </summary>
    public string title;
    /// <summary>
    /// 该章节的课时
    /// </summary>
    public Lesson[] lessons;
}

/// <summary>
/// 课时
/// </summary>
[Serializable]
public class Lesson
{
    /// <summary>
    /// 顺序
    /// </summary>
    public int index;
    /// <summary>
    /// 课时ID
    /// </summary>
    public string id;
    /// <summary>
    /// 课时标题
    /// </summary>
    public string title;
}