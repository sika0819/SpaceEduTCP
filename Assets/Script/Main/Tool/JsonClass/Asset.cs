using System;
using System.Collections.Generic;
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

