using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public class TestInspector : EditorWindow
{
    public int Type;
    void OnGUI()
    {
        Type = EditorGUILayout.IntField(Type);
        if (GUILayout.Button("1111111"))
        {
            Debug.Log(CreateRoleHelper.GetRandomName((EnumGender)Type));
        }

    }

}
