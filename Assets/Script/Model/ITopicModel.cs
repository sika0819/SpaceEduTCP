public interface ITopicModel  {
    /// <summary>
    /// 排序
    /// </summary>
    int index { get; set; }
    /// <summary>
    /// 会话ID
    /// </summary>
    string id { get; set; }
    /// <summary>
    /// 课时ID（无意义）
    /// </summary>
     string parent { get; set; }
    /// <summary>
    /// 会话名称（显示到课时名称后）
    /// </summary>
     string title { get; set; }
    /// <summary>
    /// 会话简述
    /// </summary>
     string desc { get; set; }
    /// <summary>
    /// 资源ID
    /// </summary>
     string asset { get; set; }
    /// <summary>
    /// 热点
    /// </summary>
    HotModel[] hots { get; set; }
}
