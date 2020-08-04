using UnityEngine;
using UnityEngine.UI;

//自动生成于：2020/6/27 9:33:39
	public partial class UIHeadItem
	{

		private Canvas m_Canvas_Node;
		private Slider m_Slider_HP;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Canvas_Node = autoBindTool.GetBindComponent<Canvas>(0);
			m_Slider_HP = autoBindTool.GetBindComponent<Slider>(1);
		}
	}
