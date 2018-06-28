using System;
/// <summary>
/// 会话
/// </summary>
[Serializable]
public class TopicModel:ITopicModel
{
    /// <summary>
    /// 排序
    /// </summary>
    public int index { get; set; }
    /// <summary>
    /// 会话ID
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// 课时ID（无意义）
    /// </summary>
    public string parent { get; set; }
    /// <summary>
    /// 会话名称（显示到课时名称后）
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 会话简述
    /// </summary>
    public string desc { get; set; }
    /// <summary>
    /// 资源ID
    /// </summary>
    public string asset { get; set; }
    /// <summary>
    /// 热点
    /// </summary>
    public HotModel[] hots { get; set; }
}
