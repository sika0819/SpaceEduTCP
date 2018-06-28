using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CourseModel:ICourseModel
{
    /// <summary>
    /// 课程ID
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// 课程类型
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 课程名称
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 课程描述
    /// </summary>
    public string desc { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    public string updated { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public string created { get; set; }
    /// <summary>
    /// 缩略图URL
    /// </summary>
    public string thumb { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public string status { get; set; }
}
