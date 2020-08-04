using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoading : BaseUI
{
    public override void OnInit()
    {
        base.OnInit();
        UIMaskHelper.AddMask(this);
    }


}
