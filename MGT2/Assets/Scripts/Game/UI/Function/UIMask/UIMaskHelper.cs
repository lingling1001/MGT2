using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaskHelper
{
    public static void AddMask(BaseUI ui)
    {
        var prefab = ResLoadHelper.LoadAsset<GameObject>(UIHelper.GetUIPath(EnumUIType.MaskImage));
        GameObject obj = ResLoadHelper.Instantiate<GameObject>(prefab);
        if (ui.Trans == null)
        {
            ResLoadHelper.Destory(obj);
        }
        else
        {
            obj.transform.SetParent(ui.Trans, false);
            obj.transform.SetAsFirstSibling();
        }
    }




}
