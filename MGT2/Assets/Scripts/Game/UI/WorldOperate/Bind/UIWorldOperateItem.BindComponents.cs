using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//自动生成于：6/25/2021 2:51:18 PM
public partial class UIWorldOperateItem
	{

		private Button m_Btn_This;
		private TextMeshProUGUI m_Txt_Text;
		private AtlasImage m_AImg_Icon;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Btn_This = autoBindTool.GetBindComponent<Button>(0);
			m_Txt_Text = autoBindTool.GetBindComponent<TextMeshProUGUI>(1);
			m_AImg_Icon = autoBindTool.GetBindComponent<AtlasImage>(2);
		}

 
}
