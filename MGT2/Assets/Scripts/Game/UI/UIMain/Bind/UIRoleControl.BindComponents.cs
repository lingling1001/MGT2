using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：6/8/2021 10:32:18 AM
	public partial class UIRoleControl
	{

		private HorizontalLayoutGroup m_HGroup_Role;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_HGroup_Role = autoBindTool.GetBindComponent<HorizontalLayoutGroup>(0);
		}
	}
