using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;
using System.IO;
using System.Linq;
using UnityEngine.U2D;
using System.Reflection;
using System.Collections.Generic;

/// <summary>
/// AtlasImage Editor.
/// </summary>
[CustomEditor(typeof(AtlasImage), true)]
[CanEditMultipleObjects]
public class AtlasImageEditor : ImageEditor
{
    //private static bool _openSelectorWindow = false;
    private static SpriteAtlas _lastSpriteAtlas;
    private readonly SpritePreview _preview = new SpritePreview();
    private SerializedProperty _spAtlas;
    private SerializedProperty _spSpriteName;
    private SerializedProperty _spType;
    private SerializedProperty _spPreserveAspect;
    private AnimBool _animShowType;
    private AtlasImage _atlasImage;

    private static GUIContent Txt_SpriteAtlas = new GUIContent("Sprite Atlas");
    private static GUIContent Txt_SpriteAtlasNull = new GUIContent("Select Sprite Atlas");

    protected override void OnEnable()
    {
        if (!target) return;
        _atlasImage = target as AtlasImage;
        base.OnEnable();
        _spAtlas = serializedObject.FindProperty("m_SpriteAtlas");
        _spSpriteName = serializedObject.FindProperty("m_SpriteName");
        _spType = serializedObject.FindProperty("m_Type");
        _spPreserveAspect = serializedObject.FindProperty("m_PreserveAspect");

        _animShowType = new AnimBool(_spAtlas.objectReferenceValue && !string.IsNullOrEmpty(_spSpriteName.stringValue));
        _animShowType.valueChanged.AddListener(new UnityAction(base.Repaint));

        _preview.onApplyBorder = () =>
        {
            PackAtlas(_spAtlas.objectReferenceValue as SpriteAtlas);
            _atlasImage.sprite = (_spAtlas.objectReferenceValue as SpriteAtlas).GetSprite(_spSpriteName.stringValue);
        };

        _lastSpriteAtlas = null;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _preview.onApplyBorder = null;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawAtlasPopupLayout(_spAtlas);

        DrawSpritePopup(_spAtlas.objectReferenceValue as SpriteAtlas, _spSpriteName);


        AppearanceControlsGUI();
        RaycastControlsGUI();

        _animShowType.target = _spAtlas.objectReferenceValue && !string.IsNullOrEmpty(_spSpriteName.stringValue);
        if (EditorGUILayout.BeginFadeGroup(_animShowType.faded))
            this.TypeGUI();
        EditorGUILayout.EndFadeGroup();

        var imageType = (Image.Type)_spType.intValue;
        base.SetShowNativeSize(imageType == Image.Type.Simple || imageType == Image.Type.Filled, false);

        if (EditorGUILayout.BeginFadeGroup(m_ShowNativeSize.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_spPreserveAspect);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFadeGroup();

        base.NativeSizeButtonGUI();
        serializedObject.ApplyModifiedProperties();


        // Draw preview
        var image = target as AtlasImage;
        _preview.sprite = GetOriginalSprite(image.spriteAtlas, image.spriteName);

        _preview.color = image ? image.canvasRenderer.GetColor() : Color.white;


    }

    public override GUIContent GetPreviewTitle()
    {
        return _preview.GetPreviewTitle();
    }

    public override void OnPreviewGUI(Rect rect, GUIStyle background)
    {
        _preview.OnPreviewGUI(rect);
    }

    public override string GetInfoString()
    {
        return _preview.GetInfoString();
    }

    public override void OnPreviewSettings()
    {
        _preview.OnPreviewSettings();
    }

    public void DrawAtlasPopupLayout(SerializedProperty serialized, params GUILayoutOption[] option)
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            NGUIEditorTools.SetLabelWidth(80);
            EditorGUILayout.PrefixLabel(Txt_SpriteAtlas);
            Rect rect = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.popup, option);
            SpriteAtlas atlas = serialized.objectReferenceValue as SpriteAtlas;

            if (GUI.Button(rect, atlas ? new GUIContent(atlas.name) : Txt_SpriteAtlasNull, EditorStyles.popup))
            {
                ComponentSelector.Show<SpriteAtlas>(obj =>
                {
                    if (serialized == null)
                    {
                        return;
                    }
                    serialized.objectReferenceValue = obj;
                    serialized.serializedObject.ApplyModifiedProperties();
                });
            }
            if (atlas != null)
            {
                if (GUILayout.Button("Focus", GUILayout.Width(50)))
                {
                    Selection.activeObject = atlas;
                }
            }
        }
    }





    public void DrawSpritePopup(SpriteAtlas atlas, SerializedProperty serializeSprite)
    {
        GUIContent content = new GUIContent(serializeSprite.displayName, serializeSprite.tooltip);

        var controlID = GUIUtility.GetControlID(FocusType.Passive);
        string strName = serializeSprite.stringValue;

        using (new EditorGUI.DisabledGroupScope(!atlas))
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.PrefixLabel(content);
            if (GUILayout.Button(string.IsNullOrEmpty(strName) ? "-" : strName, "minipopup") && atlas)
            {
                if (_lastSpriteAtlas != atlas)
                {
                    _lastSpriteAtlas = atlas;
                    PackAtlas(atlas);
                }
                SpriteSelector.Show(atlas, (obj) =>
                {
                    if (obj == null)
                        return;
                    string strRealName = obj.Replace("(Clone)", "");
                    serializeSprite.stringValue = strRealName;
                    serializeSprite.serializedObject.ApplyModifiedProperties();

                });
            }

            if (_atlasImage.sprite != null)
            {
                if (GUILayout.Button("Focus", GUILayout.Width(50)))
                {
                    Selection.activeObject = _atlasImage.sprite;
                }
            }
        }

    }






    private static void PackAtlas(SpriteAtlas atlas)
    {

        UnityEditor.U2D.SpriteAtlasUtility.PackAtlases(new SpriteAtlas[] { atlas },
            EditorUserBuildSettings.activeBuildTarget);

    }

    public static Sprite GetOriginalSprite(SpriteAtlas atlas, string name)
    {
        if (!atlas || string.IsNullOrEmpty(name))
        {
            return null;
        }
        SerializedProperty spPackedSprites = new SerializedObject(atlas).FindProperty("m_PackedSprites");
        int count = spPackedSprites.arraySize;
        for (int cnt = 0; cnt < count; cnt++)
        {
            Object obj = spPackedSprites.GetArrayElementAtIndex(cnt).objectReferenceValue;
            Sprite sprite = obj as Sprite;
            if (sprite == null)
            {
                continue;
            }
            if (sprite.name == name)
            {
                return sprite;
            }
        }
        return null;
    }


    //%%%% v Context menu for editor v %%%%
    [MenuItem("CONTEXT/Image/Convert To AtlasImage", true)]
    static bool _ConvertToAtlasImage(MenuCommand command)
    {
        return CanConvertTo<AtlasImage>(command.context);
    }

    [MenuItem("CONTEXT/Image/Convert To AtlasImage", false)]
    static void ConvertToAtlasImage(MenuCommand command)
    {
        ConvertTo<AtlasImage>(command.context);
    }

    [MenuItem("CONTEXT/Image/Convert To Image", true)]
    static bool _ConvertToImage(MenuCommand command)
    {
        return CanConvertTo<Image>(command.context);
    }

    [MenuItem("CONTEXT/Image/Convert To Image", false)]
    static void ConvertToImage(MenuCommand command)
    {
        ConvertTo<Image>(command.context);
    }

    /// <summary>
    /// Verify whether it can be converted to the specified component.
    /// </summary>
    protected static bool CanConvertTo<T>(Object context)
        where T : MonoBehaviour
    {
        return context && context.GetType() != typeof(T);
    }

    /// <summary>
    /// Convert to the specified component.
    /// </summary>
    protected static void ConvertTo<T>(Object context) where T : MonoBehaviour
    {
        var target = context as MonoBehaviour;
        var so = new SerializedObject(target);
        so.Update();

        bool oldEnable = target.enabled;
        target.enabled = false;

        // Find MonoScript of the specified component.
        foreach (var script in Resources.FindObjectsOfTypeAll<MonoScript>())
        {
            if (script.GetClass() != typeof(T))
                continue;

            // Set 'm_Script' to convert.
            so.FindProperty("m_Script").objectReferenceValue = script;
            so.ApplyModifiedProperties();
            break;
        }

        (so.targetObject as MonoBehaviour).enabled = oldEnable;
    }
}
