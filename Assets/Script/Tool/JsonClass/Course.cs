using System;
using System.Collections.Generic;
[Serializable]
public class CourseList
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
    public Topic[] topics;
}