using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DefineText
{
    public const int Attack = 1;
    public const int Guard = 2;
    public const int Infomation = 3;

    public static string GetText(int type)
    {
        PrototypeText data = PrototypeManager<PrototypeText>.Instance.GetPrototype(type);
        if (data != null)
        {
            return data.Chinese;
        }
        return string.Empty;
    }

}



