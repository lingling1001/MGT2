using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2020/7/25 8:14:01
	public partial class UIMain
	{

		private GridLayoutGroup m_GGroup_TopRight;
		private Button m_Btn_Close;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_GGroup_TopRight = autoBindTool.GetBindComponent<GridLayoutGroup>(0);
			m_Btn_Close = autoBindTool.GetBindComponent<Button>(1);
		}
	}
