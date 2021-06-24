using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorMenu
{
    

    

    
    /// <summary>
    /// 配置设置界面
    /// </summary>
    [MenuItem("MGTools/Config/刷新配置")]
    public static void RefreshAllConfig()
    {
        ConfigLoadExtension.RefreshAllConfig();
    }
    /// <summary>
    /// 配置设置界面
    /// </summary>
    [MenuItem("MGTools/Config/清空配置")]
    public static void ClearConfig()
    {
        ConfigLoadExtension.ClearSaveChangeData();
    }

    /// <summary>
    /// 配置设置界面
    /// </summary>
    [MenuItem("MGTools/TS/TS001")]
    public static void OpenTS001()
    {
        TestInspector myWindow =
           (TestInspector)EditorWindow.GetWindow(typeof(TestInspector), false, "EditorTestFun", true);//创建窗口

        myWindow.Show();
    }

    //[MenuItem("MGTools/HotKey/CopyItemPath &q")]
    //private static void CopyItemPath()
    //{
    //    EditorCopyComponentPath item = new EditorCopyComponentPath(1);
    //}
    //[MenuItem("MGTools/HotKey/CopyItemPath &w")]
    //private static void CopyAllComponent()
    //{
    //    EditorCopyComponentPath item = new EditorCopyComponentPath();
    //    GameObject obj = Selection.activeGameObject;
    //    item.CopyAllComponent(obj);
    //}

    //[MenuItem("MGTools/HotKey/CopyItempathFull %q")]
    //private static void CopyItemPathEx()
    //{
    //    EditorCopyComponentPath itemEx = new EditorCopyComponentPath(2);
    //}
}
