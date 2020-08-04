using UnityEngine;
using UnityEngine.UI;

//自动生成于：2020/7/1 19:05:12
	public partial class UIMinMap
	{

		private RectTransform m_Rect_Content;
		private Image m_Img_Mask;
		private RawImage m_RImg_Map;
		private Image m_Img_View;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Rect_Content = autoBindTool.GetBindComponent<RectTransform>(0);
			m_Img_Mask = autoBindTool.GetBindComponent<Image>(1);
			m_RImg_Map = autoBindTool.GetBindComponent<RawImage>(2);
			m_Img_View = autoBindTool.GetBindComponent<Image>(3);
		}
	}
