using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：12/8/2020 9:31:59 PM
	public partial class UICityInfoItem
	{

		private Image m_Img_BG;
		private TextMeshProUGUI m_Txt_Name;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Img_BG = autoBindTool.GetBindComponent<Image>(0);
			m_Txt_Name = autoBindTool.GetBindComponent<TextMeshProUGUI>(1);
		}
	}
