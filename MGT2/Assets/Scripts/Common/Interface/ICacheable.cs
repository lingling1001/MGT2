using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICacheable
{
    string GetKey();
    void ResetData();
}
