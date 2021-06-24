using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：6/21/2021 10:25:19 AM
	public partial class WorldOperateItem
	{

		private Button m_Btn_This;
		private TextMeshProUGUI m_Txt_Text;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Btn_This = autoBindTool.GetBindComponent<Button>(0);
			m_Txt_Text = autoBindTool.GetBindComponent<TextMeshProUGUI>(1);
		}
	}
