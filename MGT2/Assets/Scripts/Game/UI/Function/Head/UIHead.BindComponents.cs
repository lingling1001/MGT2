using UnityEngine;
using UnityEngine.UI;

//自动生成于：2020/6/10 13:57:57
	public partial class UIHead
	{

		private RectTransform m_Trans_Parents;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Trans_Parents = autoBindTool.GetBindComponent<RectTransform>(0);
		}
	}
