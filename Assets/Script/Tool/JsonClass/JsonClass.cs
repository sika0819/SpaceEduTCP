using System;
using System.Collections.Generic;

public class Courses
{
    public int page;
    public int per_page;
    public List<Course> courses;
}

/// <summary>
/// 课程
/// </summary>
[Serializable]
public class Course
{
    /// <summary>
    /// 课程ID
    /// </summary>
    public string id;
    /// <summary>
    /// 课程类型
    /// </summary>
    public string type;
    /// <summary>
    /// 课程名称
    /// </summary>
    public string name;
    /// <summary>
    /// 课程描述
    /// </summary>
    public string desc;
    /// <summary>
    /// 修改时间
    /// </summary>
    public string updated;
    /// <summary>
    /// 创建时间
    /// </summary>
    public string created;
    /// <summary>
    /// 缩略图URL
    /// </summary>
    public string thumb;
    /// <summary>
    /// 状态
    /// </summary>
    public string status;
}

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

/// <summary>
/// 会话
/// </summary>
[Serializable]
public class Topic
{
    /// <summary>
    /// 排序
    /// </summary>
    public int index;
    /// <summary>
    /// 会话ID
    /// </summary>
    public string id;
    /// <summary>
    /// 课时ID（无意义）
    /// </summary>
    public string parent;
    /// <summary>
    /// 会话名称（显示到课时名称后）
    /// </summary>
    public string title;
    /// <summary>
    /// 会话简述
    /// </summary>
    public string desc;
    /// <summary>
    /// 资源ID
    /// </summary>
    public string asset;
    /// <summary>
    /// 热点
    /// </summary>
    public Hot[] hots;
}

/// <summary>
/// 资源（图片、视频）本身
/// </summary>
[Serializable]
public class Asset
{
    /// <summary>
    /// 资源ID
    /// </summary>
    public string id;
    /// <summary>
    /// 资源类型
    /// </summary>
    public string type;
    /// <summary>
    /// 资源缩略图(无意义)
    /// </summary>
    public string thumb;
    /// <summary>
    /// 资源URL
    /// </summary>
    public string url;
    /// <summary>
    /// 资源拥有者（无意义）
    /// </summary>
    public string owner;
    /// <summary>
    /// 资源创建时间（无意义）
    /// </summary>
    public string created;
}

/// <summary>
/// 热点
/// </summary>
[Serializable]
public class Hot
{
    /// <summary>
    /// 图片无意义、视频为时间
    /// </summary>
    public int index;
    /// <summary>
    /// x坐标
    /// </summary>
    public double x;
    /// <summary>
    /// y坐标
    /// </summary>
    public double y;
    /// <summary>
    /// 热点描述
    /// </summary>
    public string desc;
}