using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public partial class UIHeadInfoItem : ClickItemBase<UIHeadInfoItem>
{
    public AssemblyCache AssemblyCache { get { return _assemblyCache; } }
    private AssemblyCache _assemblyCache;
    private RectTransform _rectTrans;
    private void Awake()
    {
        _rectTrans = GetComponent<RectTransform>();
        GetBindComponents(gameObject);
        SetButton(m_Btn_Event);
    }
    public void SetData(EntityAssembly data)
    {
        _assemblyCache = data.GetData<AssemblyCache>();
        if (_assemblyCache.AssyRoleInfo == null)
        {
            return;
        }
        RefreshItemRole.Refresh(m_Scr_RoleItem, _assemblyCache.AssyRoleInfo);

    }

    public void RefreshPosition()
    {
        if (_assemblyCache == null)
        {
            return;
        }
        _rectTrans.anchoredPosition = UIHelper.WorldToUI(_assemblyCache.AssyPosition.Position);
    }
}
