using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIHeadItem : MonoBehaviour
{
    public int EntityId { get; private set; }
    private void Awake()
    {
        GetBindComponents(gameObject);
    }
    public void Initial(int entityId)
    {
        m_Canvas_Node.worldCamera = CameraManager.Instance.MainCamera;
        EntityId = entityId;
    }
    //private GameObject obj;
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
        Vector3 lookPos = CameraManager.Instance.MainCamera.transform.position;
        lookPos.Set(pos.x, lookPos.y, lookPos.z);
        transform.LookAt(lookPos);
        //if (obj == null)
        //{
        //    obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //}
        //obj.transform.position = lookPos;
    }

}
