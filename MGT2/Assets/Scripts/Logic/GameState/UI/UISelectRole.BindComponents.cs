using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2020/7/9 19:46:16
	public partial class UISelectRole
	{

		private GridLayoutGroup m_GGroup_RoleParent;
		private Button m_Btn_LogIn;
		private Button m_Btn_Close;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_GGroup_RoleParent = autoBindTool.GetBindComponent<GridLayoutGroup>(0);
			m_Btn_LogIn = autoBindTool.GetBindComponent<Button>(1);
			m_Btn_Close = autoBindTool.GetBindComponent<Button>(2);
		}
	}
