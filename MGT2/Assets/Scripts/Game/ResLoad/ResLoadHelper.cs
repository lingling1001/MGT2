using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class ResLoadHelper
{

    public static void LoadAssetAsync<T>(string assetName, Action<T> eventLoadFinish)
    {
        Addressables.LoadAssetAsync<T>(assetName).Completed += (obj) =>
        {
            if (obj.IsDone)
            {
                eventLoadFinish(obj.Result);
            }
        };
    }
    public static void LoadAssetAsync<T>(IResourceLocation location, Action<T> eventLoadFinish)
    {
        Addressables.LoadAssetAsync<T>(location).Completed += (obj) =>
        {
            if (obj.IsDone)
            {
                eventLoadFinish(obj.Result);
            }
        };
    }
    public static void LoadAssetsAsync<T>(IList<IResourceLocation> assets, Action<T> eventLoadFinish)
    {
        Addressables.LoadAssetsAsync<T>(assets, eventLoadFinish);
    }
    public static void LoadAssetInstantiateAsync(string assetName, Action<GameObject> eventLoadFinish, Transform parent = null)
    {
        try
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(assetName, parent);
            handle.Completed += obj =>
            {
                if (obj.IsDone)
                {
                    eventLoadFinish(handle.Result);
                }
            };
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
            Log.Error(" assetName Exception  " + assetName);
        }
    }
    public static void LoadResourceLocationsAsync(string assetName, Action<List<IResourceLocation>> callback)
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
        UnityEngine.Object obj = PreLoadResHelper.Instance.LoadAsset(assetName);
        if (obj == null)
        {
            Log.Error("LoadAsset Error Path " + assetName);
            return null;
        }
        return obj as T;
    }

    public static T Instantiate<T>(T obj) where T : UnityEngine.Object
    {
        return UnityEngine.Object.Instantiate<T>(obj);
    }
    public static T Instantiate<T>(T obj, Transform parent) where T : UnityEngine.Object
    {
        return UnityEngine.Object.Instantiate<T>(obj, parent);
    }

    public static void Destory(GameObject objUI)
    {
        GameObject.Destroy(objUI);
    }
}
