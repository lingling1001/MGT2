using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryGAction
{
    public static T Create<T>() where T : GoapAction, new()
    {
        return new T();
    }
}
