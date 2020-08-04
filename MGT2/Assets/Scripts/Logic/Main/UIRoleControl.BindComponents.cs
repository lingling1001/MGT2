using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2020/7/21 18:20:55
	public partial class UIRoleControl
	{

		private RectTransform m_Trans_Skill;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Trans_Skill = autoBindTool.GetBindComponent<RectTransform>(0);
		}
	}
