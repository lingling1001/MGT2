using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2020/7/21 19:42:21
	public partial class UIRoleSkillItem
	{

		private Image m_Img_Icon;
		private Image m_Img_CD;
		private Image m_Img_CDMask;
		private Image m_Img_Lock;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Img_Icon = autoBindTool.GetBindComponent<Image>(0);
			m_Img_CD = autoBindTool.GetBindComponent<Image>(1);
			m_Img_CDMask = autoBindTool.GetBindComponent<Image>(2);
			m_Img_Lock = autoBindTool.GetBindComponent<Image>(3);
		}
	}
