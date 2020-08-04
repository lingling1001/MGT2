using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public partial class UIMainButtonItem : PrototypeItemClickBase<UIMainButtonItem, PrototypeMainUI>
{
    public override void OnInit()
    {
        base.OnInit();
        GetBindComponents(gameObject);
        SetEventButton(m_Btn_Event);
    }
    public override void SetData(PrototypeMainUI data)
    {
        base.SetData(data);
        this.m_Txt_Name.text = data.Name;
    }

}
