using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDepthHelper
{
    public static void Set2Top(BaseUI go)
    {
        if (go != null)
        {
            Set2Top(go.Trans);
        }
    }
    /// <summary>
    ///  将UI置顶
    /// </summary>
    /// <param name="go">界面</param>
    /// <param name="step">增加的层级数量</param>
    public static void Set2Top(Transform trans)
    {
        if (trans == null)
        {
            return;
        }
        trans.SetAsLastSibling();
        //int count = trans.parent.childCount;
        //for (int cnt = 0; cnt < count; cnt++)
        //{
        //    Debug.LogError(trans.parent.GetChild(cnt).gameObject.activeSelf + " " + trans.parent.GetChild(cnt).name);
        //}
        //Debug.LogError("ToTop " + trans.name + "   Parent " + trans.parent.name);
        //Debug.LogError(trans.parent.childCount);
    }


}
