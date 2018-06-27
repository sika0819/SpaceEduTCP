using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoginModel  {
    string token { get; set; }
    long expires { get; set; }
    UserInfo user { get; set; }
}
