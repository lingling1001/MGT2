using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EditorBuilder
{
    public static void BuildePackageEx()
    {
        string packagePath = @"C:\Users\milk\Desktop\123";

        List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
        AssetBundleBuild info = new AssetBundleBuild();
        info.assetBundleName = "model.unity3d";
        info.assetNames = new string[] { "Assets/_Art/Model" };
        builds.Add(info);
        BuildPipeline.BuildAssetBundles(packagePath, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.Android);
        AssetDatabase.Refresh();

        Debug.Log("Output Path: " + packagePath);
    }
    //public static void BuildePackage()
    //{
    //    string packagePath = @"C:\Users\milk\Desktop\123";

    //    BuildPipeline.BuildAssetBundles(packagePath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    //    AssetDatabase.Refresh();
    //    Debug.Log("Output Path: " + packagePath);
    //}

}
