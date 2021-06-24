using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//自动生成于：6/8/2021 10:31:12 AM
public partial class UIRoleControlItem
	{

		private UIItemNormalScr m_Scr_RoleItem;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Scr_RoleItem = autoBindTool.GetBindComponent<UIItemNormalScr>(0);
		}

   
}
