using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public class TestInspector : EditorWindow
{

    void OnGUI()
    {
        if (GUILayout.Button("1111111"))
        {
            EditorBuilder.BuildePackageEx();
        }
        
    }

}
