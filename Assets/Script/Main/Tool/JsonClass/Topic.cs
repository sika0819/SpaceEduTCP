using System;
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