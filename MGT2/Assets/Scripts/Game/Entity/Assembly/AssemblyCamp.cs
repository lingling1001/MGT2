using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyCamp : AssemblyBase
{
    public int Id { get; private set; }
    public void SetValue(int param)
    {
        Id = param;
    }
}
