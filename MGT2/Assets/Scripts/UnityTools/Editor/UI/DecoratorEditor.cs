using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// A base class for creating editors that decorate Unity's built-in editor types.
/// </summary>
public abstract class DecoratorEditor : Editor
{
    // empty array for invoking methods using reflection
    private static readonly object[] EMPTY_ARRAY = new object[0];

    #region Editor Fields

    /// <summary>
    /// Type object for the internally used (decorated) editor.
    /// </summary>
    private System.Type decoratedEditorType;

    /// <summary>
    /// Type object for the object that is edited by this editor.
    /// </summary>
    private System.Type editedObjectType;

    private Editor editorInstance;

    #endregion

    private static Dictionary<string, MethodInfo> decoratedMethods = new Dictionary<string, MethodInfo>();

    private static Assembly editorAssembly = Assembly.GetAssembly(typeof(Editor));

    public virtual string editorTypeName { get; private set; }
    protected Editor EditorInstance
    {
        get
        {
            if (editorInstance == null)
            {
                Debug.LogError("Could not create editor !");
            }
            return editorInstance;
        }
    }
    public virtual void OnEnable()
    {
        decoratedEditorType = editorAssembly.GetType(editorTypeName);
        editorInstance = Editor.CreateEditor(targets, decoratedEditorType);
    }

    void OnDisable()
    {
        if (editorInstance != null)
        {
            DestroyImmediate(editorInstance);
            editorInstance = null;
        }
    }

    /// <summary>
    /// Delegates a method call with the given name to the decorated editor instance.
    /// </summary>
    protected void CallInspectorMethod(string methodName)
    {
        MethodInfo method = null;
        // Add MethodInfo to cache
        if (!decoratedMethods.ContainsKey(methodName))
        {
            var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

            method = decoratedEditorType.GetMethod(methodName, flags);

            if (method != null)
            {
                decoratedMethods[methodName] = method;
            }
            else
            {
                Debug.LogError(string.Format("Could not find method {0}", method));
            }
        }
        else
        {
            method = decoratedMethods[methodName];
        }

        if (method != null)
        {
            method.Invoke(EditorInstance, EMPTY_ARRAY);
        }
    }

    public void OnSceneGUI()
    {
        CallInspectorMethod("OnSceneGUI");
    }

    protected override void OnHeaderGUI()
    {
        CallInspectorMethod("OnHeaderGUI");
    }

    public override void OnInspectorGUI()
    {

        EditorInstance.OnInspectorGUI();
    }

    public override void DrawPreview(Rect previewArea)
    {
        EditorInstance.DrawPreview(previewArea);
    }

    public override string GetInfoString()
    {
        return EditorInstance.GetInfoString();
    }

    public override GUIContent GetPreviewTitle()
    {
        return EditorInstance.GetPreviewTitle();
    }

    public override bool HasPreviewGUI()
    {
        return EditorInstance.HasPreviewGUI();
    }

    public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
    {
        EditorInstance.OnInteractivePreviewGUI(r, background);
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        EditorInstance.OnPreviewGUI(r, background);
    }

    public override void OnPreviewSettings()
    {
        EditorInstance.OnPreviewSettings();
    }

    public override void ReloadPreviewInstances()
    {
        EditorInstance.ReloadPreviewInstances();
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        return EditorInstance.RenderStaticPreview(assetPath, subAssets, width, height);
    }

    public override bool RequiresConstantRepaint()
    {
        return EditorInstance.RequiresConstantRepaint();
    }

    public override bool UseDefaultMargins()
    {
        return EditorInstance.UseDefaultMargins();
    }
}
