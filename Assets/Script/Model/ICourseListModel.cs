using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICourseListModel  {
    int page { get; set; }
    int per_page { get; set; }
    List<CourseModel> courses { get; set; }
}
