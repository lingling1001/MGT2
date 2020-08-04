using System.Collections;
using System.Collections.Generic;
using UnityEngine;
static public class NGUITools
{
    [System.Diagnostics.DebuggerHidden]
    [System.Diagnostics.DebuggerStepThrough]
    static public bool GetActive(Behaviour mb)
    {
        return mb && mb.enabled && mb.gameObject.activeInHierarchy;
    }

    /// <summary>
    /// Unity4 has changed GameObject.active to GameObject.activeself.
    /// </summary>

    [System.Diagnostics.DebuggerHidden]
    [System.Diagnostics.DebuggerStepThrough]
    static public bool GetActive(GameObject go)
    {
        return go && go.activeInHierarchy;
    }


    /// <summary>
    /// Add a new child game object.
    /// </summary>

    static public GameObject AddChild(GameObject parent) { return AddChild(parent, true); }

    /// <summary>
    /// Add a new child game object.
    /// </summary>

    static public GameObject AddChild(GameObject parent, bool undo)
    {
        GameObject go = new GameObject();
#if UNITY_EDITOR
        if (undo) UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    /// <summary>
    /// Instantiate an object and add it to the specified parent.
    /// </summary>

    static public GameObject AddChild(GameObject parent, GameObject prefab)
    {
        SetLayer(prefab, parent.layer);
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.SetParent(parent.transform);
            t.localPosition = Vector3.zero;
            //   t.localPosition = new Vector3(-200, 0, 0);
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    static public GameObject AddChild(GameObject parent, GameObject prefab, float x)
    {
        SetLayer(prefab, parent.layer);
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            // t.localPosition = Vector3.zero;

            t.localPosition = new Vector3(x, 0, 0);
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    /// <summary>
    /// Recursively set the game object's layer.
    /// </summary>

    static public void SetLayer(GameObject go, int layer)
    {
        go.layer = layer;

        Transform t = go.transform;

        for (int i = 0, imax = t.childCount; i < imax; ++i)
        {
            Transform child = t.GetChild(i);
            SetLayer(child.gameObject, layer);
        }
    }

    static public void SetDirty(UnityEngine.Object obj)
    {
#if UNITY_EDITOR
        if (obj)
        {
            //if (obj is Component) Debug.Log(NGUITools.GetHierarchy((obj as Component).gameObject), obj);
            //else if (obj is GameObject) Debug.Log(NGUITools.GetHierarchy(obj as GameObject), obj);
            //else Debug.Log("Hmm... " + obj.GetType(), obj);
            UnityEditor.EditorUtility.SetDirty(obj);
        }
#endif
    }
}
