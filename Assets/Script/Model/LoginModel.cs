using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LoginModel : ILoginModel {
    public string token { get; set; }
    public long expires { get; set; }
    public UserInfo user { get; set; }

    
}
