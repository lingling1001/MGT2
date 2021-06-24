using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：6/17/2021 5:06:10 PM
	public partial class UIHeadInfoItem
	{

		private UIItemNormalScr m_Scr_RoleItem;
		private Button m_Btn_Event;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Scr_RoleItem = autoBindTool.GetBindComponent<UIItemNormalScr>(0);
			m_Btn_Event = autoBindTool.GetBindComponent<Button>(1);
		}
	}
