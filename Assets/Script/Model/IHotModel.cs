
public interface IHotModel
{
    /// <summary>
    /// 图片无意义、视频为时间
    /// </summary>
    int index { get; set; }
    /// <summary>
    /// x坐标
    /// </summary>
    double x { get; set; }
    /// <summary>
    /// y坐标
    /// </summary>
    double y { get; set; }
    /// <summary>
    /// 热点描述
    /// </summary>
    string desc { get; set; }
}
