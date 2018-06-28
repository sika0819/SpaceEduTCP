using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public interface ISockectDataModel  {
    string UserName { get; set; }
    string Message { get; set; }
    IPEndPoint LocalEndPoint { get; set; }
    Vector3 FocusPoint { get; set; }
}
