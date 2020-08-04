using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2020/7/9 20:10:08
	public partial class UISelectRoleItem
	{

		private Button m_Btn_LogIn;
		private TextMeshProUGUI m_Txt_Name;
		private Image m_Img_Select;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Btn_LogIn = autoBindTool.GetBindComponent<Button>(0);
			m_Txt_Name = autoBindTool.GetBindComponent<TextMeshProUGUI>(1);
			m_Img_Select = autoBindTool.GetBindComponent<Image>(2);
		}
	}
