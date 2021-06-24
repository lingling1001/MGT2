using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshItemRole
{
    public static void Refresh(UIItemNormalScr item, AssemblyRoleInfo roleInfo)
    {
        if (item == null || roleInfo == null)
        {
            return;
        }
        UIItemNormalScr.UIItemMemberInfo itemName = item.GetMemberInfo(UIItemNormalScr.EItemInfoType.Name);
        if (itemName != null)
        {
            item.SetScrMemberInfo(itemName, true, roleInfo.Name);
        }

        UIItemNormalScr.UIItemMemberInfo itemIcon = item.GetMemberInfo(UIItemNormalScr.EItemInfoType.Icon);
        if (itemIcon != null)
        {
            item.SetScrMemberInfo(itemIcon, true, roleInfo.GetHeadIcon());
        }


    }


}
