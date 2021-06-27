using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：6/25/2021 11:00:40 AM
	public partial class UIWorldOperateRole
	{

		private UIItemNormalScr m_Scr_RoleItem;
		private Button m_Btn_Event;
		private GridLayoutGroup m_GGroup_Node;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Scr_RoleItem = autoBindTool.GetBindComponent<UIItemNormalScr>(0);
			m_Btn_Event = autoBindTool.GetBindComponent<Button>(1);
			m_GGroup_Node = autoBindTool.GetBindComponent<GridLayoutGroup>(2);
		}
	}
