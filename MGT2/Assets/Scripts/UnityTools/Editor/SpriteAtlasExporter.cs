using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteAtlasExporter
{
    public static string[] Export(SpriteAtlas atlas, string dirPath)
    {
        string platformName = "Standalone";
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            platformName = "Android";
        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
        {
            platformName = "iPhone";
        }

        TextureImporterPlatformSettings tips = atlas.GetPlatformSettings(platformName);
        TextureImporterPlatformSettings cachedTips = new TextureImporterPlatformSettings();
        tips.CopyTo(cachedTips);

        tips.overridden = true;
        tips.format = TextureImporterFormat.RGBA32;
        //tips.maxTextureSize = 1024;
        atlas.SetPlatformSettings(tips);

        List<string> texturePathList = new List<string>();

        SpriteAtlasUtility.PackAtlases(new SpriteAtlas[] { atlas }, EditorUserBuildSettings.activeBuildTarget);
        MethodInfo getPreviewTextureMI = typeof(SpriteAtlasExtensions).GetMethod("GetPreviewTextures", BindingFlags.Static | BindingFlags.NonPublic);
        Texture2D[] atlasTextures = (Texture2D[])getPreviewTextureMI.Invoke(null, new System.Object[] { atlas });
        if (atlasTextures != null && atlasTextures.Length > 0)
        {
            for (int i = 0; i < atlasTextures.Length; i++)
            {
                Texture2D packTexture = atlasTextures[i];
                byte[] rawBytes = packTexture.GetRawTextureData();

                Texture2D nTexture = new Texture2D(packTexture.width, packTexture.height, packTexture.format, false, false);
                nTexture.LoadRawTextureData(rawBytes);
                nTexture.Apply();
                string textPath = string.Format("{0}/{1}_{2}.png", dirPath, atlas.name, i);
                File.WriteAllBytes(textPath, nTexture.EncodeToPNG());

                texturePathList.Add(textPath);
            }
        }

        atlas.SetPlatformSettings(cachedTips);

        return texturePathList.ToArray();
    }

    [MenuItem("Tools/Atlas/PackedAuto")]
    private static void PackedAuto()
    {
        if (Selection.objects == null)
            return;
        if (Selection.assetGUIDs.Length != 1)
        {
            return;
        }
        string uipath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);


        SpriteAtlasPackingSettings setting = new SpriteAtlasPackingSettings();
        setting.enableRotation = false;
        setting.enableTightPacking = false;

        //SpriteAtlasTextureSettings texSetting = new SpriteAtlasTextureSettings();
        TextureImporterPlatformSettings texPlatSetting = new TextureImporterPlatformSettings();
        texPlatSetting.maxTextureSize = 2048;
        texPlatSetting.crunchedCompression = true;
        texPlatSetting.compressionQuality = 100;
        string dest = string.Empty;
        DirectoryInfo info = Directory.CreateDirectory(uipath);
        string[] guids = AssetDatabase.FindAssets("t:Sprite", new string[] { uipath });
        if (guids.Length > 0)
        {
            List<Sprite> listSpite = new List<Sprite>();
            foreach (string guid in guids)
            {
                Sprite s = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid)) as Sprite;
                listSpite.Add(s);
            }
            dest = string.Format("Assets/_Res/Atlas/{0}.spriteatlas", info.Name.ToLower());
            SpriteAtlas atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(dest);
            if (atlas)
            {
                SpriteAtlasExtensions.Remove(atlas, atlas.GetPackables());
            }
            else
            {
                atlas = new SpriteAtlas();
                atlas.SetPackingSettings(setting);
                atlas.SetPlatformSettings(texPlatSetting);
                AssetDatabase.CreateAsset(atlas, dest);
            }
            SpriteAtlasExtensions.Add(atlas, listSpite.ToArray());
            EditorUtility.SetDirty(atlas);
        }

        EditorUtility.DisplayDialog("提示", "创建图集成功"+ dest, "确认");

        AssetDatabase.SaveAssets();
    }

    [MenuItem("Tools/Atlas/SpriteAtlas Exporter")]
    private static void ExportAltas()
    {
        List<SpriteAtlas> atlasList = new List<SpriteAtlas>();
        if (Selection.objects != null && Selection.objects.Length > 0)
        {
            foreach (var obj in Selection.objects)
            {
                if (obj.GetType() == typeof(SpriteAtlas))
                {
                    atlasList.Add(obj as SpriteAtlas);
                }
            }
        }

        if (atlasList.Count == 0)
        {
            EditorUtility.DisplayDialog("Tips", "Please Selected SpriteAtlas", "OK");
            return;
        }

        string dirPath = EditorUtility.OpenFolderPanel("Save Dir", "D:/", "");
        if (string.IsNullOrEmpty(dirPath))
        {
            EditorUtility.DisplayDialog("Tips", "Please Selected a folder", "OK");
            return;
        }

        foreach (var atlas in atlasList)
        {
            Export(atlas, dirPath);
        }

        // ExplorerUtil.OpenExplorerFolder(dirPath);
    }




}