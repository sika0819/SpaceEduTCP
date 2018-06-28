using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICourseModel {
     string id { get; set; }
    /// <summary>
    /// 课程类型
    /// </summary>
     string type { get; set; }
    /// <summary>
    /// 课程名称
    /// </summary>
     string name { get; set; }
    /// <summary>
    /// 课程描述
    /// </summary>
     string desc { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
     string updated { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
     string created { get; set; }
    /// <summary>
    /// 缩略图URL
    /// </summary>
     string thumb { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
     string status { get; set; }
}
