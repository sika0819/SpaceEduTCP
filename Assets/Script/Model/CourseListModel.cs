using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CourseListModel : ICourseListModel
{
    public int page { get; set; }
    public int per_page { get; set; }
    public List<CourseModel> courses { get; set; }
}
