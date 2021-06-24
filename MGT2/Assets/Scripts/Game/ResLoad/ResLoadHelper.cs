using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class ResLoadHelper : MonoSingleton<ResLoadHelper>
{

    public static AsyncOperationHandle<Object> LoadAssetAsync(string assetName)
    {
        return Addressables.LoadAssetAsync<Object>(assetName);
    }

    public static void LoadAssetAsync<T>(string assetName, System.Action<T> eventLoadFinish)
    {
        try
        {
            Addressables.LoadAssetAsync<T>(assetName).Completed += (obj) =>
             {
                 if (obj.IsDone)
                 {
                     eventLoadFinish(obj.Result);
                 }
             };
        }
        catch (System.Exception e)
        {
            Log.Error(e.ToString());
        }
    }

    public static void LoadAssetAsync<T>(IResourceLocation location, System.Action<T> eventLoadFinish)
    {
        Addressables.LoadAssetAsync<T>(location).Completed += (obj) =>
        {
            if (obj.IsDone)
            {
                eventLoadFinish(obj.Result);
            }
        };
    }
    public static void LoadAssetsAsync<T>(IList<IResourceLocation> assets, System.Action<T> eventLoadFinish)
    {
        Addressables.LoadAssetsAsync<T>(assets, eventLoadFinish);
    }



    public static void LoadResourceLocationsAsync(string assetName, System.Action<List<IResourceLocation>> callback)
    {
        Addressables.LoadResourceLocationsAsync(assetName).Completed += (obj) =>
        {
            if (obj.IsDone)
            {
                List<IResourceLocation> list = new List<IResourceLocation>();
                if (obj.Result != null)
                {
                    list.AddRange(obj.Result);
                }
                callback(list);
            }
        };
    }

    public static T LoadAsset<T>(string assetName) where T : UnityEngine.Object
    {
        UnityEngine.Object obj = PreResLoadHelper.Instance.LoadAsset(assetName);
        if (obj == null)
        {
            Log.Error("LoadAsset Error Path " + assetName);
            return null;
        }
        return obj as T;
    }

    public static void DestroyObject(GameObject obj)
    {
        if (obj == null || obj.Equals(null))
        {
            return;
        }
        GameObject.Destroy(obj);
    }
}

