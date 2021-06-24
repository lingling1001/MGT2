using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIRoleControlItem : MonoBehaviour
{
    private AssemblyRoleControl _data;
    private void Awake()
    {
        GetBindComponents(gameObject);
    }

    public void SetData(AssemblyRoleControl data)
    {
        _data = data;
        if (data == null)
        {
            return;
        }
        AssemblyRoleInfo role = data.Owner.GetData<AssemblyRoleInfo>();
        if (role == null)
        {
            return;
        }
        RefreshItemRole.Refresh(m_Scr_RoleItem, role);

    }
}
