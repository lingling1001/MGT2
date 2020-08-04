using System.Collections.Generic;

/// <summary>
/// 引导特效接口
/// </summary>
public interface IGuideEffect
{
    void StartEffect();
    void ComplateEffect();
    int GuidUIId {get; set;}
    int GuideEffectId { get; set;}
    List<string> GuidUIParam {get; set;}
}
