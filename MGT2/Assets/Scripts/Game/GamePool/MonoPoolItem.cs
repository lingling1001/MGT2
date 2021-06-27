using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonoPool
{
    string PoolKey { get; set; }
    void EnterPool();
}
public class MonoPoolItem : MonoBehaviour, IMonoPool
{
    public string PoolKey { get; set; }
    public virtual void EnterPool()
    {
        ItemPoolMgr.AddPool(PoolKey, gameObject);
    }


}

