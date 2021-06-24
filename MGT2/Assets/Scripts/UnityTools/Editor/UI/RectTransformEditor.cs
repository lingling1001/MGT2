using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(RectTransform))]
public class RectTransformEditor : DecoratorEditor
{
    public override string editorTypeName => "UnityEditor.RectTransformEditor";
    public override void OnInspectorGUI()
    {
        if (EditorInstance == null)
        {
            return;
        }
        base.OnInspectorGUI();
        DrawCopyComponent();
    }
    public enum OperateType
    {
        Copy,
        Auto,
    }
    private OperateType type = OperateType.Copy;
    private System.Collections.Generic.List<System.Type> listCompons = new System.Collections.Generic.List<System.Type>();

    private void DrawCopyComponent()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("CP"))
        {
            CopyName(serializedObject.targetObject.name);
        }
        //if (GUILayout.Button("LAB"))
        //{
        //    CopyNameComponent("UILabel");
        //}
        if (GUILayout.Button("CP"))
        {
            SetListDatas();
            Transform trans = serializedObject.targetObject as Transform;
            listCompons.Insert(0, trans.gameObject.GetType());
            type = OperateType.Copy;
        }
        if (GUILayout.Button("AU"))
        {
            SetListDatas();
            type = OperateType.Auto;
        }
        EditorGUILayout.EndHorizontal();
        for (int cnt = 0; cnt < listCompons.Count; cnt++)
        {
            if (GUILayout.Button(listCompons[cnt].Name))
            {
                if (type == OperateType.Copy)
                {
                    CopyNameComponent(listCompons[cnt].Name);
                }
                else if (type == OperateType.Auto)
                {
                    AutoSetFiled(listCompons[cnt]);
                }
                listCompons.Clear();
            }
        }
    }
    private void SetListDatas()
    {
        listCompons.Clear();
        Transform trans = serializedObject.targetObject as Transform;
        Component[] coms = trans.GetComponents<Component>();
        for (int cnt = 0; cnt < coms.Length; cnt++)
        {
            listCompons.Add(coms[cnt].GetType());
        }
    }
    private void CopyNameComponent(string param1)
    {
        CopyName(string.Format("    [SerializeField] private {0} {1};", param1, serializedObject.targetObject.name));
    }
    private void CopyName(string param1)
    {
        TextEditor text = new TextEditor();
        text.text = param1;
        text.OnFocus();
        text.Copy();
        listCompons.Clear();
    }

    private void AutoSetFiled(System.Type type)
    {
        try
        {
            if (serializedObject == null)
            {
                return;
            }
            Transform trans = serializedObject.targetObject as Transform;
            if (trans == null)
            {
                return;
            }
            Component thisComponent = trans.GetComponent(type);
            //获取所有字段
            List<FieldInfo> listFis = new List<FieldInfo>();
            listFis.AddRange(type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance));
            listFis.AddRange(type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance));
            System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>();
            for (int cnt = 0; cnt < listFis.Count; cnt++)
            {
                list.Clear();
                list.Add(trans.gameObject);
                NGUITools.FindHideChildGameObject(list, trans, listFis[cnt].Name);
                for (int i = 0; i < list.Count; i++)
                {
                    if (listFis[cnt].Name == list[i].name)
                    {
                        if (listFis[cnt].FieldType.Name == "GameObject")
                        {
                            listFis[cnt].SetValue(thisComponent, list[i]);
                        }
                        else
                        {
                            Component com = list[i].GetComponent(listFis[cnt].FieldType);
                            if (com == null)
                            {
                                continue;
                            }
                            if (listFis[cnt].FieldType == com.GetType())
                            {
                                listFis[cnt].SetValue(thisComponent, com);
                            }
                            else
                            {
                                Debug.LogError(listFis[cnt].FieldType.Name + "   " + com.GetType().Name);
                            }
                        }
                        continue;
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
}
