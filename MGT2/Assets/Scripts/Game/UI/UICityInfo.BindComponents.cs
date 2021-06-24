using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：12/8/2020 9:43:40 PM
	public partial class UICityInfo
	{

		private Button m_Btn_MainCity;
		private HorizontalLayoutGroup m_HGroup_Cities;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Btn_MainCity = autoBindTool.GetBindComponent<Button>(0);
			m_HGroup_Cities = autoBindTool.GetBindComponent<HorizontalLayoutGroup>(1);
		}
	}
