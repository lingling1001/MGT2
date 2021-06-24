using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：6/23/2021 2:03:00 PM
public partial class UIMain
	{

		private TextMeshProUGUI m_Txt_CurTime;
		private Button m_Btn_Pause;
		private AtlasImage m_AImg_Pause;
		private Button m_Btn_Speed;
		private AtlasImage m_Img_Speed;
		private TextMeshProUGUI m_Txt_Speed;
		private Button m_Btn_Main;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Txt_CurTime = autoBindTool.GetBindComponent<TextMeshProUGUI>(0);
			m_Btn_Pause = autoBindTool.GetBindComponent<Button>(1);
			m_AImg_Pause = autoBindTool.GetBindComponent<AtlasImage>(2);
			m_Btn_Speed = autoBindTool.GetBindComponent<Button>(3);
			m_Img_Speed = autoBindTool.GetBindComponent<AtlasImage>(4);
			m_Txt_Speed = autoBindTool.GetBindComponent<TextMeshProUGUI>(5);
			m_Btn_Main = autoBindTool.GetBindComponent<Button>(6);
		}
	}
