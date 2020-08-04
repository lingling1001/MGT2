using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UISelectRoleItem : PrototypeItemClickBase<UISelectRoleItem, PrototypeRole>
{
    public bool IsSelect { get; private set; }
    public override void OnInit()
    {
        GetBindComponents(gameObject);
        base.OnInit();
        SetEventButton(m_Btn_LogIn);
    }

    public override void SetData(PrototypeRole data)
    {
        base.SetData(data);
        UIHelper.SetText(m_Txt_Name, Data.Name);

    }
    public void SetValue(bool value)
    {
        IsSelect = value;
        UnityObjectExtension.SetActive(m_Img_Select, value);
    }
}
