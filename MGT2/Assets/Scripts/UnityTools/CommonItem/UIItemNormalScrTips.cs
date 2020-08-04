using UnityEngine;
using System.Collections;

public class UIItemNormalScrTips
{

    public static void ShowTips(int type, int itemId, object param, GameObject go)
    {
        EnumScrTipsType itemType = (EnumScrTipsType)type;
        if (itemType == EnumScrTipsType.Item)
        {
            int number = 0;
            if (param != null)
            {
                int.TryParse(param.ToString(), out number);
            }

        }
        else if (itemType == EnumScrTipsType.ShipReference)
        {

        }


    }
}
public enum EnumScrTipsType : int
{
    None,
    Item,
    Ship,
    ShipReference,

}