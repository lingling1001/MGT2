using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：6/2/2021 2:02:25 PM
	public partial class UIEnterGame
	{

		private Button m_Btn_Enter;
		private Button m_Btn_Load;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Btn_Enter = autoBindTool.GetBindComponent<Button>(0);
			m_Btn_Load = autoBindTool.GetBindComponent<Button>(1);
		}
	}
