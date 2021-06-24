//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2015 Tasharen Entertainment
//----------------------------------------------

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D;

/// <summary>
/// Editor component used to display a list of sprites.
/// </summary>

public class SpriteSelector : ScriptableWizard
{
    static public SpriteSelector instance;

    void OnEnable() { instance = this; }
    void OnDisable()
    {
        NGUISettings.partialSprite = string.Empty;
        instance = null;
    }

    public delegate void Callback(string sprite);

    SerializedObject mObject;
    SerializedProperty mProperty;

    Sprite mSprite;
    Vector2 mPos = Vector2.zero;
    Callback mCallback;
    float mClickTime = 0f;

    /// <summary>
    /// Draw the custom wizard.
    /// </summary>

    void OnGUI()
    {
        NGUIEditorTools.SetLabelWidth(80f);

        if (NGUISettings.atlas == null)
        {
            GUILayout.Label("No Atlas selected.", "LODLevelNotifyText");
        }
        else
        {
            SpriteAtlas atlas = NGUISettings.atlas;
            bool close = false;
            GUILayout.Label(atlas.name + " Sprites", "LODLevelNotifyText");
            NGUIEditorTools.DrawSeparator();

            GUILayout.BeginHorizontal();
            GUILayout.Space(84f);

            string before = NGUISettings.partialSprite;
            string after = EditorGUILayout.TextField("", before, "SearchTextField");
            if (before != after) NGUISettings.partialSprite = after;

            if (GUILayout.Button("", "SearchCancelButton", GUILayout.Width(18f)))
            {
                NGUISettings.partialSprite = "";
                GUIUtility.keyboardControl = 0;
            }
            GUILayout.Space(84f);
            GUILayout.EndHorizontal();

            //Texture2D tex = atlas.texture as Texture2D;

            //if (tex == null)
            //{
            //	GUILayout.Label("The atlas doesn't have a texture to work with");
            //	return;
            //}

            BetterList<string> sprites = GetListOfSprites(atlas, NGUISettings.partialSprite);

            float size = 80f;
            float padded = size + 10f;
            int columns = Mathf.FloorToInt(Screen.width / padded);
            if (columns < 1) columns = 1;

            int offset = 0;
            Rect rect = new Rect(10f, 0, size, size);

            GUILayout.Space(10f);
            mPos = GUILayout.BeginScrollView(mPos);
            int rows = 1;

            while (offset < sprites.size)
            {
                GUILayout.BeginHorizontal();
                {
                    int col = 0;
                    rect.x = 10f;

                    for (; offset < sprites.size; ++offset)
                    {
                        Sprite sprite = NGUITools.GetSprite(atlas, sprites[offset]);
                        if (sprite == null) continue;

                        // Button comes first
                        if (GUI.Button(rect, ""))
                        {
                            if (Event.current.button == 0)
                            {
                                float delta = Time.realtimeSinceStartup - mClickTime;
                                mClickTime = Time.realtimeSinceStartup;

                                if (NGUISettings.selectedSprite != sprite.name)
                                {
                                    if (mSprite != null)
                                    {
                                        NGUIEditorTools.RegisterUndo("Atlas Selection", mSprite);
                                        //mSprite.MakePixelPerfect();
                                        EditorUtility.SetDirty(mSprite);
                                    }

                                    NGUISettings.selectedSprite = sprite.name;
                                    NGUIEditorTools.RepaintSprites();
                                    if (mCallback != null) mCallback(sprite.name);
                                }
                                else if (delta < 0.5f) close = true;
                            }
                            else
                            {
                                //NGUIContextMenu.AddItem("Edit", false, EditSprite, sprite);
                                //NGUIContextMenu.AddItem("Delete", false, DeleteSprite, sprite);
                                NGUIContextMenu.AddItem("CopyNameSprite", false, CopyNameSpriteSprite, sprite);

                                NGUIContextMenu.Show();
                            }
                        }

                        if (Event.current.type == EventType.Repaint)
                        {
                            // On top of the button we have a checkboard grid
                            NGUIEditorTools.DrawTiledTexture(rect, NGUIEditorTools.backdropTexture);

                            DrawSprite(rect, sprite);

                            // Draw the selection
                            if (NGUISettings.selectedSprite == sprite.name)
                            {
                                NGUIEditorTools.DrawOutline(rect, new Color(0.4f, 1f, 0f, 1f));
                            }
                        }

                        GUI.backgroundColor = new Color(1f, 1f, 1f, 0.5f);
                        GUI.contentColor = new Color(1f, 1f, 1f, 0.7f);
                        GUI.Label(new Rect(rect.x, rect.y + rect.height, rect.width, 32f), sprite.name, "ProgressBarBack");
                        GUI.contentColor = Color.white;
                        GUI.backgroundColor = Color.white;

                        if (++col >= columns)
                        {
                            ++offset;
                            break;
                        }
                        rect.x += padded;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(padded);
                rect.y += padded + 26;
                ++rows;
            }
            GUILayout.Space(rows * 26);
            GUILayout.EndScrollView();

            if (close) Close();
        }
    }

    /// <summary>
    /// Edit the sprite (context menu selection)
    /// </summary>

    void EditSprite(object obj)
    {
        //if (this == null) return;
        //UISpriteData sd = obj as UISpriteData;
        //NGUIEditorTools.SelectSprite(sd.name);
        //Close();
    }

    /// <summary>
    /// Delete the sprite (context menu selection)
    /// </summary>

    void DeleteSprite(object obj)
    {
        //if (this == null) return;
        //UISpriteData sd = obj as UISpriteData;

        //List<UIAtlasMaker.SpriteEntry> sprites = new List<UIAtlasMaker.SpriteEntry>();
        //UIAtlasMaker.ExtractSprites(NGUISettings.atlas, sprites);

        //for (int i = sprites.Count; i > 0;)
        //{
        //    UIAtlasMaker.SpriteEntry ent = sprites[--i];
        //    if (ent.name == sd.name)
        //        sprites.RemoveAt(i);
        //}
        //UIAtlasMaker.UpdateAtlas(NGUISettings.atlas, sprites);
        //NGUIEditorTools.RepaintSprites();
    }
    void CopyNameSpriteSprite(object obj)
    {
        if (this == null || obj == null) return;
        Sprite sd = obj as Sprite;
        TextEditor text = new TextEditor();
        text.text = sd.name;
        text.OnFocus();
        text.Copy();
    }



    BetterList<string> GetListOfSprites(SpriteAtlas spriteAtlas, string filter)
    {
        BetterList<string> strs = new BetterList<string>();
        SerializedProperty spPackedSprites = new SerializedObject(spriteAtlas).FindProperty("m_PackedSprites");
        int count = spPackedSprites.arraySize;
        for (int cnt = 0; cnt < count; cnt++)
        {
            string strName = spPackedSprites.GetArrayElementAtIndex(cnt).objectReferenceValue.name;
            if (!string.IsNullOrEmpty(filter))
            {
                if (!strName.Contains(filter))
                {
                    continue;
                }
            }
            strs.Add(strName);
        }
        return strs;
    }

    public static void DrawSprite(Rect rect, Sprite sprite)
    {
        if (sprite == null)
        {
            return;
        }

        var tex = sprite.texture;
        var textureRect = sprite.textureRect;

        GUI.DrawTextureWithTexCoords(
            rect,
            tex,
            new Rect(
                textureRect.x / tex.width,
                textureRect.y / tex.height,
                textureRect.width / tex.width,
                textureRect.height / tex.height));
    }




    /// <summary>
    /// Property-based selection result.
    /// </summary>

    void OnSpriteSelection(string sp)
    {
        if (mObject != null && mProperty != null)
        {
            mObject.Update();
            mProperty.stringValue = sp;
            mObject.ApplyModifiedProperties();
        }
    }

    /// <summary>
    /// Show the sprite selection wizard.
    /// </summary>

    //static public void ShowSelected()
    //{
    //    if (NGUISettings.atlas != null)
    //    {
    //        Show(delegate (string sel) { NGUIEditorTools.SelectSprite(sel); });
    //    }
    //}

    /// <summary>
    /// Show the sprite selection wizard.
    /// </summary>

    static public void Show(SerializedObject ob, SerializedProperty pro, SpriteAtlas atlas)
    {
        if (instance != null)
        {
            instance.Close();
            instance = null;
        }

        if (ob != null && pro != null && atlas != null)
        {
            SpriteSelector comp = ScriptableWizard.DisplayWizard<SpriteSelector>("Select a Sprite");
            NGUISettings.SetAtlas(atlas);
            NGUISettings.selectedSprite = pro.hasMultipleDifferentValues ? null : pro.stringValue;
            comp.mSprite = null;
            comp.mObject = ob;
            comp.mProperty = pro;
            comp.mCallback = comp.OnSpriteSelection;
        }
    }
    static public void Show(SpriteAtlas atlas, Callback callback)
    {
        NGUISettings.SetAtlas(atlas);
        Show(callback);
    }
    /// <summary>
    /// Show the selection wizard.
    /// </summary>

    static public void Show(Callback callback)
    {
        if (instance != null)
        {
            instance.Close();
            instance = null;
        }

        SpriteSelector comp = ScriptableWizard.DisplayWizard<SpriteSelector>("Select a Sprite");
        comp.mSprite = null;
        comp.mCallback = callback;
    }
}
