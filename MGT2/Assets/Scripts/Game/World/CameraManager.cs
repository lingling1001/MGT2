using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFrameWork;
using Cinemachine;

public class CameraManager : ManagerBase
{
    public GameObject ObjNode;
    public Camera MainCamera { get { return _mainCamera; } }
    private Camera _mainCamera;
    private CinemachineVirtualCamera _cinemachine;
    public CinemachineVirtualCamera Cinemachine { get { return _cinemachine; } }

    private Transform _objCameraFollow;
    public Transform ObjCameraFollow { get { return _objCameraFollow; } }

    public Vector3 FollowPosition { get { return Cinemachine.Follow.position; } }
    public void ResetCameraFollow()
    {
        if (Cinemachine.Follow != ObjCameraFollow)
        {
            SetFollowPosition(Cinemachine.Follow.position);
            SetFollowTarget(ObjCameraFollow);
        }
    }

    public void SetFollowPosition(Vector3 pos)
    {
        _objCameraFollow.position = pos;
    }


    public void SetFollowTarget(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }
        SetFollowTarget(obj.transform);
    }
    public void SetFollowTarget(Transform trans)
    {
        if (Cinemachine == null)
        {
            return;
        }
        Cinemachine.Follow = trans;
    }

    public override void On_Init()
    {
        if (ObjNode == null)
        {
            ObjNode = GameObject.Find("MainCameraNode");
            _objCameraFollow = ObjNode.transform.Find("FollowTarget");
            _cinemachine = ObjNode.GetComponentInChildren<CinemachineVirtualCamera>();
            _mainCamera = ObjNode.GetComponentInChildren<Camera>();
            ObjNode.transform.localPosition = Vector3.zero;
        }
    }
    public override void On_Release()
    {
        SetFollowTarget(ObjNode);
        ObjNode = null;
    }

}
