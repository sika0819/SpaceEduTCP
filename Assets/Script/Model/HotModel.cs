using System;

[Serializable]
public class HotModel: IHotModel
{
    /// <summary>
    /// 图片无意义、视频为时间
    /// </summary>
    public int index { get; set; }
    /// <summary>
    /// x坐标
    /// </summary>
    public double x { get; set; }
    /// <summary>
    /// y坐标
    /// </summary>
    public double y { get; set; }
    /// <summary>
    /// 热点描述
    /// </summary>
    public string desc { get; set; }
}
