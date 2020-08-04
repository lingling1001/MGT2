using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2020/6/11 21:26:32
	public partial class UIMainButtonItem
	{

		private Button m_Btn_Event;
		private TextMeshProUGUI m_Txt_Name;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Btn_Event = autoBindTool.GetBindComponent<Button>(0);
			m_Txt_Name = autoBindTool.GetBindComponent<TextMeshProUGUI>(1);
		}
	}
